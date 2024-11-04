using EroDungeonInTerm.Application.UI;
using System;
using System.Collections.Immutable;

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

	public override void Draw(Render render)
	{
		GetSize(out uint width, out uint height);

		uint verticalPointer = 0;

		// Title
		render.Place(verticalPointer, 0, '┌');
		DrawAlign(render, verticalPointer, width, Title, '─', 1, 1);
		render.Place(verticalPointer, width - 1, '┐');
		verticalPointer++;

		// Main section rows
		for (int i = 0; i < MainSection.Rows.Length; i++)
		{
			render.Place(verticalPointer, 0, '│');
			render.Place(verticalPointer, width - 1, '│');
			DrawRow(render, verticalPointer, width, MainSection.Rows[i], Padding.Left + 1, Padding.Right + 1);
			verticalPointer++;
		}

		// Sections
		for (int i = 0; i < Sections.Length; i++)
		{
			render.Place(verticalPointer, 0, '├');
			DrawAlign(render, verticalPointer, width, Sections[i].Title, '─', 1, 1);
			render.Place(verticalPointer, width - 1, '┤');
			verticalPointer++;

			for (int j = 0; j < Sections[i].Rows.Length; j++)
			{
				render.Place(verticalPointer, 0, '│');
				render.Place(verticalPointer, width - 1, '│');
				DrawRow(render, verticalPointer, width, Sections[i].Rows[j], Padding.Left + 1, Padding.Right + 1);
				verticalPointer++;
			}
		}

		// Footer
		render.Place(verticalPointer, 0, '└');
		for (uint i = 1; i < width -1; i++)
		{
			render.Place(verticalPointer, i, '─');
		}
		render.Place(verticalPointer, width - 1, '┘');
	}

	private void DrawRow(Render render, uint row, uint rowLength, TextRow text, uint paddingLeft, uint paddingRight)
	{
		DrawAlign(render, row, rowLength, text, ' ', paddingLeft, paddingRight);
	}

	private void DrawAlign(Render render, uint row, uint rowLength, TextRow text, char filler, uint paddingLeft, uint paddingRight)
	{
		switch (text.Align)
		{
			case HorizontalAlignment.Left:
				Copy(render, row, paddingLeft, text);
				render.FillLine(row, (uint)(paddingLeft + text.Text.Length), (uint)(rowLength - paddingLeft - text.Text.Length - paddingRight), filler);
				break;
			case HorizontalAlignment.Right:
				Copy(render, row, (uint)(rowLength - paddingRight - text.Text.Length), text);
				render.FillLine(row, paddingLeft, (uint)(rowLength - paddingLeft - paddingRight - text.Text.Length), filler);
				break;
			case HorizontalAlignment.Center:
				uint leftMost = paddingLeft;
				uint rightMost = (uint)(rowLength - paddingRight);
				uint available = rightMost - leftMost;
				uint padding = (uint)(available - text.Text.Length) / 2;

				render.FillLine(row, leftMost, available, filler);
				Copy(render, row, leftMost + padding, text);
				break;
		}
	}

	private void Copy(Render render, uint row, uint dstIndex, string src)
	{
		int srcIndex = 0;
		for (uint i = dstIndex; i < src.Length; i++)
		{
			render.Place(row, i, src[srcIndex++]);
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