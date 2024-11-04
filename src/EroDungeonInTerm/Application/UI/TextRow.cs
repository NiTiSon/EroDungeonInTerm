namespace EroDungeonInTerm.Application.UI;

public readonly struct TextRow
{
	public readonly string Text;
	public readonly HorizontalAlignment Align;

	public static readonly TextRow Empty = new(string.Empty, HorizontalAlignment.Left);

	public TextRow() : this(string.Empty) { }

	public TextRow(string text) : this(text, HorizontalAlignment.Left) { }

	public TextRow(string text, HorizontalAlignment align)
	{
		Text = text;
		Align = align;
	}

	public static implicit operator string(TextRow row) => row.Text;

	public static implicit operator TextRow(string text) => new(text);
}