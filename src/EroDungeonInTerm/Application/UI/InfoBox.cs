using System;
using System.Collections.Immutable;
using System.Security.Cryptography;
using EroDungeonInTerm.Application.UI;

namespace EroDungeonInTerm.Rendering;

/// <summary>
/// UI element with title, rows and sections.
/// </summary>
public partial class InfoBox : UIElement
{
	public readonly Section MainSection;
	public readonly ImmutableArray<Section> Sections;
	public readonly Padding Padding;

	public TextRow Title => MainSection.Title;

	public InfoBox() : this(new Section(), ImmutableArray<Section>.Empty) { }

	public InfoBox(TextRow title, params TextRow[] rows)
		: this(new Section(title, rows.ToImmutableArray()), ImmutableArray<Section>.Empty) { }

	public InfoBox(TextRow title, Padding padding, params TextRow[] rows)
		: this(new Section(title, rows.ToImmutableArray()), padding, ImmutableArray<Section>.Empty) { }

	public InfoBox(TextRow title, TextRow[] rows, params Section[] sections)
		: this(new Section(title, rows.ToImmutableArray()), sections.ToImmutableArray()) { }

	public InfoBox(TextRow title, Padding padding, TextRow[] rows, params Section[] sections)
		: this(new Section(title, rows.ToImmutableArray()), padding, sections.ToImmutableArray()) { }


	public InfoBox(Section mainSection, ImmutableArray<Section> sections)
	{
		MainSection = mainSection;
		Padding = Padding.Zero;
		Sections = sections;
	}

	public InfoBox(Section mainSection, Padding padding, ImmutableArray<Section> sections)
	{
		MainSection = mainSection;
		Padding = padding;
		Sections = sections;
	}

	public uint LongestSectionRowLength
	{
		get
		{
			int count = Sections.Length;
			int maxLength = 0;
			for (int i = 0; i < count; i++)
			{
				maxLength = int.Max(maxLength, (int)Sections[i].LongestRowLength);
			}
			maxLength = int.Max(maxLength, (int)MainSection.LongestRowLength);

			return (uint)maxLength;
		}
	}

	public override void Draw(char[][] renderBox)
	{
		GetSize(out uint width, out uint height);

		int verticalPointer = 0;

		// Title
		renderBox[verticalPointer][0] = '┌';
		DrawAlign(renderBox[verticalPointer], Title, '─', 1, 1);
		renderBox[verticalPointer][width - 1] = '┐';
		verticalPointer++;

		// Main section rows
		for (int i = 0; i < MainSection.Rows.Length; i++)
		{
			renderBox[verticalPointer][0] = '│';
			renderBox[verticalPointer][width - 1] = '│';
			DrawRow(renderBox[verticalPointer], MainSection.Rows[i], Padding.Left + 1, Padding.Right + 1);
			verticalPointer++;
		}

		// Sections
		for (int i = 0; i < Sections.Length; i++)
		{
			renderBox[verticalPointer][0] = '├';
			DrawAlign(renderBox[verticalPointer], Sections[i].Title, '─', 1, 1);
			renderBox[verticalPointer][width - 1] = '┤';
			verticalPointer++;

			for (int j = 0; j < Sections[i].Rows.Length; j++)
			{
				renderBox[verticalPointer][0] = '│';
				renderBox[verticalPointer][width - 1] = '│';
				DrawRow(renderBox[verticalPointer], Sections[i].Rows[j], Padding.Left + 1, Padding.Right + 1);
				verticalPointer++;
			}
		}

		// Footer
		renderBox[verticalPointer][0] = '└';
		for (int i = 1; i < width -1; i++)
		{
			renderBox[verticalPointer][i] = '─';
		}
		renderBox[verticalPointer][width - 1] = '┘';

		for (int i = 0; i < height; i++)
		{
			for (int j = 0; j < width; j++)
			{
				if (renderBox[i][j] == '\0')
				{
#if DEBUG
					renderBox[i][j] = '░'; // For debugging purposes
#else
					renderBox[i][j] = ' ';
#endif
				}
			}
		}
	}

	private void DrawRow(char[] place, TextRow text, uint paddingLeft, uint paddingRight)
	{
		DrawAlign(place, text, ' ', paddingLeft, paddingRight);
	}

	private void DrawAlign(char[] place, TextRow text, char filler, uint paddingLeft, uint paddingRight)
	{
		switch (text.Align)
		{
			case TextAlign.Left:
				Copy(place, paddingLeft, text);
				Array.Fill(place, filler, (int)(paddingLeft + text.Text.Length), (int)(place.Length - paddingLeft - text.Text.Length - paddingRight));
				break;
			case TextAlign.Right:
				Copy(place, (uint)(place.Length - paddingRight - text.Text.Length), text);
				Array.Fill(place, filler, (int)(paddingLeft), (int)(place.Length - paddingLeft - paddingRight - text.Text.Length));
				break;
			case TextAlign.Center:
				uint leftMost = paddingLeft;
				uint rightMost = (uint)(place.Length - paddingRight);
				uint available = rightMost - leftMost;
				uint padding = (uint)(available - text.Text.Length) / 2;
				Array.Fill(place, filler, (int)leftMost, (int)available);
				Copy(place, leftMost + padding, text);
				break;
		}
	}

	private void Copy(char[] dst, uint dstIndex, string src)
	{
		int srcIndex = 0;
		for (uint i = dstIndex; i < dst.Length; i++)
		{
			if (srcIndex >= src.Length) break;

			dst[i] = src[srcIndex++];
		}
	}

	public override void GetSize(out uint width, out uint height)
	{
		width = 0;
		height = 0;

		// Borders
		width += 2;
		height += 2;

		// Each section title
		height += (uint)Sections.Length;
		for (int i = 0; i < Sections.Length; i++)
		{
			// Add rows
			height += (uint)Sections[i].Rows.Length;
		}

		// Main section title are inlined into border

		// Main section rows
		height += (uint)MainSection.Rows.Length;

		// Width by longest row value
		width += LongestSectionRowLength;

		// Padding
		width += Padding.Left + Padding.Right;
		height += Padding.Top + Padding.Bottom;

		return;
	}
}