namespace EroDungeonInTerm.Application.UI;

public readonly struct Padding
{
	public readonly uint Left;
	public readonly uint Top;
	public readonly uint Right;
	public readonly uint Bottom;

	public static readonly Padding Zero = default;

	public Padding(uint all)
	{
		Left = all;
		Top = all;
		Right = all;
		Bottom = all;
	}

	public Padding(uint left, uint top, uint right, uint bottom)
	{
		Left = left;
		Top = top;
		Right = right;
		Bottom = bottom;
	}
}