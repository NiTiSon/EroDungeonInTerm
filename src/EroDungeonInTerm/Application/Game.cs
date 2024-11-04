using System;
using System.Collections.Concurrent;
using System.Threading;

namespace EroDungeonInTerm.Application;

public class Game : IDisposable
{
	private readonly ConcurrentQueue<InputEvent> inputEvents;
	private readonly Thread inputThread;
	
	private GameState state;

	private Scene? currentScene;

	public GameState State => state;


	public Game()
	{
		inputThread = new(InputThreadRun);
		inputEvents = new();
	}

	public void Run()
	{
		Console.CursorVisible = false;

		state = GameState.Running;
		inputThread.Start();
		do
		{
			// Prevent using `Console.Clear()` to better performance.
			Console.CursorLeft = 0;
			Console.CursorTop = 0;

			while (inputEvents.TryDequeue(out InputEvent input))
			{
				if (input.Info.Key == ConsoleKey.Escape)
				{
					state = GameState.Closed;
				}

				currentScene?.OnInput(input);
			}
		}
		while (state == GameState.Running);
	}

	private void InputThreadRun()
	{
		ConsoleKeyInfo info;
		while (state == GameState.Running)
		{
			info = Console.ReadKey(intercept: true);

			inputEvents.Enqueue(new InputEvent(info));
			if (info.Key == ConsoleKey.Escape)
			{
				return;
			}
		}
	}

	public void Dispose()
	{
		Console.CursorVisible = true;
		Console.ResetColor();
	}

	private static char[][] CreateMatrix(uint columns, uint rows)
	{
		
	}
}
