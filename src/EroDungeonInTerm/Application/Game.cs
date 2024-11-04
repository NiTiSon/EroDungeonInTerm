using EroDungeonInTerm.Application.UI;
using EroDungeonInTerm.Rendering;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace EroDungeonInTerm.Application;

public class Game : IDisposable
{
	public const double RenderFrequency = 0.5;

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
		Console.WindowWidth = 140;
		Console.WindowHeight = 40;

		MatrixRender render = new(40, 140);
		state = GameState.Running;
		inputThread.Start();
		
		currentScene = new(this)
		{
			Root = new InfoBox("Character: Anatoly", "Health".Align(HorizontalAlignment.Center))
		};

		long lastTimestamp = 0;
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

			if (Stopwatch.GetElapsedTime(lastTimestamp).TotalSeconds >= RenderFrequency)
			{
				currentScene?.Draw(render);

				render.Flush();

				lastTimestamp = Stopwatch.GetTimestamp();
			}
		}
		while (state == GameState.Running);
		Console.Clear();
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
}
