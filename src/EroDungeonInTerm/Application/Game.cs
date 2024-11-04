using EroDungeonInTerm.Application.UI;
using EroDungeonInTerm.Gameplay;
using EroDungeonInTerm.Rendering;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace EroDungeonInTerm.Application;

public class Game : IDisposable
{
	public const double RenderFrequency = 1d / 8d; // 8 Frames per second

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
		
		currentScene = new(this);
		Character anatoly = new(Race.Human, Sex.Male, "Anatoly", new BaseStats());
		Character ceresa = new(Race.Human, Sex.Female, "Ceresa", new BaseStats() { Attack = 19, MaxHealth = 221, CriticalChance = 0.88f });

		long lastTimestamp = 0;
		do
		{
			while (inputEvents.TryDequeue(out InputEvent input))
			{
				if (input.Info.Key == ConsoleKey.Escape)
				{
					state = GameState.Closed;
				}
				else if (input.Info.Key == ConsoleKey.D)
				{
					ceresa.Hit(20);
				}

				currentScene?.OnInput(input);
			}

			currentScene!.Root = new HBox([anatoly.GetInfo(), ceresa.GetInfo()]);

			if (Stopwatch.GetElapsedTime(lastTimestamp).TotalSeconds >= RenderFrequency)
			{
				currentScene?.Draw(render);

				Console.CursorLeft = 0;
				Console.CursorTop = 0;
				render.Flush();
				Console.CursorLeft = 0;
				Console.CursorTop = 0;

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
