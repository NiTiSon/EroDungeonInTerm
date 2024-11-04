namespace EroDungeonInTerm.Application.UI;

public static class TextRowExtensions
{
	public static TextRow Align(this string text, HorizontalAlignment align)
	{
		return new TextRow(text, align);
	}

	public static TextRow WithAlign(this TextRow text, HorizontalAlignment align)
	{
		return new TextRow(text.Text, align);
	}
}
