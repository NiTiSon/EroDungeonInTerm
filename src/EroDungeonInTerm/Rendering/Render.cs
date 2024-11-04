using System.Reflection;

namespace EroDungeonInTerm.Rendering;

public abstract class Render
{
	public abstract Render Slice(uint row, uint column, uint height, uint width);

	public abstract void Place(uint row, uint column, char ch);
}