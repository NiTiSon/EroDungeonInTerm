using EroDungeonInTerm.Rendering;

namespace EroDungeonInTerm.Application.UI;

public abstract class UIElement : IDrawable
{
	public abstract void Draw(char[][] renderBox);

	public abstract void GetSize(out uint width, out uint height);
}
