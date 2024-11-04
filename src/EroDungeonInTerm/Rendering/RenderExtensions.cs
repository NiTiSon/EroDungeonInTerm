namespace EroDungeonInTerm.Rendering;

public static class RenderExtensions
{
	public static void FillLine(this Render render, uint row, uint column, uint width, char ch)
	{
		for (uint i = 0; i < width; i++)
		{
			render.Place(row, column + i, ch);
		}
	}
}