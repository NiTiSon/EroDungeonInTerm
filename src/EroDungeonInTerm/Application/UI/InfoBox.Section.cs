using System.Collections.Immutable;
using EroDungeonInTerm.Application.UI;

namespace EroDungeonInTerm.Rendering;

public partial class InfoBox
{
	/// <summary>
	/// Section of <see cref="InfoBox"/>.
	/// </summary>
	public readonly struct Section
	{
		public readonly TextRow Title;
		public readonly ImmutableArray<TextRow> Rows;

		public Section() : this(TextRow.Empty, ImmutableArray<TextRow>.Empty) { }

		public Section(TextRow title, params TextRow[] rows) : this(title, rows.ToImmutableArray()) { }

		public Section(TextRow title, ImmutableArray<TextRow> rows)
		{
			Title = title;
			Rows = rows;
		}

		public uint LongestRowLength
		{
			get
			{
				int count = Rows.Length;
				int maxLength = 0;

				for (int i = 0; i < count; i++)
				{
					maxLength = int.Max(maxLength, Rows[i].Text.Length);
				}
				maxLength = int.Max(maxLength, Title.Text.Length);

				return (uint)maxLength;
			}
		}
	}
}