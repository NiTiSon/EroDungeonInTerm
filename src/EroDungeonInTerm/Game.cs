using System;

namespace EroDungeonInTerm;

public class Game : IDisposable
{
	public Game()
	{

	}
	
	public void Run()
	{
		Console.CursorVisible = false;
	}

	public void Dispose()
	{
		Console.CursorVisible = true;
		Console.ResetColor();
	}
}
