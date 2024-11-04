using EroDungeonInTerm.Application.UI;
using EroDungeonInTerm.Rendering;

namespace EroDungeonInTerm.Application;

public sealed class Scene
{
	private readonly Game game;

	private UIElement? root;

	public UIElement? Root
	{
		get => root;
		set => root = value;
	}

	public Scene(Game game)
	{
		this.game = game;
	}

	public void OnInput(InputEvent input)
	{

	}

	public void Draw(Render render)
	{
		root?.Draw(render);
	}
}