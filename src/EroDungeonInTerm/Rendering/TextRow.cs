namespace EroDungeonInTerm.Rendering;

public readonly struct TextRow
{
	public readonly string Text;
	public readonly TextAlign Align;

	public static readonly TextRow Empty = new(string.Empty, TextAlign.Left);

	public TextRow() : this(string.Empty) { }

	public TextRow(string text) : this(text, TextAlign.Left) { }

	public TextRow(string text, TextAlign align)
	{
		Text = text;
		Align = align;
	}

	public static implicit operator string(TextRow row) => row.Text;

	public static implicit operator TextRow(string text) => new(text);
}