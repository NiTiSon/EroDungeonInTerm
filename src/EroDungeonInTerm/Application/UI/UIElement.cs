using EroDungeonInTerm.Rendering;

namespace EroDungeonInTerm.Application.UI;

public abstract class UIElement : IDrawable
{
	private UIElement? parent;

	public UIElement? Parent
	{
		get => parent;
		internal set => parent = value;
	}

	public abstract void GetSize(out uint width, out uint height);

	public abstract void Draw(Render render);
}
