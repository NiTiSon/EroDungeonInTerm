using EroDungeonInTerm.Application.UI;
using EroDungeonInTerm.Rendering;

namespace EroDungeonInTerm.Application;

public class Scene
{
	private readonly Game game;
	private readonly uint rows, columns;

	private UIElement? root;

	public UIElement? Root
	{
		get => root;
		set => root = value;
	}

	public Scene(Game game, uint rows, uint columns)
	{
		this.game = game;
		this.rows = rows;
		this.columns = columns;
	}

	public void OnInput(InputEvent input)
	{

	}

	public void Draw(Render render)
	{
		root?.Draw(render);
	}
}