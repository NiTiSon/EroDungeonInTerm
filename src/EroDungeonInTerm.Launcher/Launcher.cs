using EroDungeonInTerm.Application;
using EroDungeonInTerm.Rendering;

namespace EroDungeonInTerm;

public static class Launcher
{
	public static void Main()
	{
		Game? game = null;
		try
		{
			game = new Game();
			game.Run();
		}
		finally
		{
			game?.Dispose();
		}
	}
}