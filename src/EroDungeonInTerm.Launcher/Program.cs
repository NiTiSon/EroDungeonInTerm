using EroDungeonInTerm.Rendering;

namespace EroDungeonInTerm;

public static class Launcher
{
	public static void Main()
	{
		//Console.WriteLine("=== BATTLE ===");
		//Console.WriteLine("Allies:");
		//Console.WriteLine("""
		//	┌#1─────────────┐ ┌#2─────────────┐
		//	│     Marta     │ │      Asta     │
		//	│ def: 34       │ │ def: 0        │
		//	│ health: 20/21 │ │ health: 0/22  │
		//	│ lust: 0%      │ │ lust: 100%    │
		//	└───────────────┘ └───────────────┘ 

		//	""");

		
		ConsoleKeyInfo key;
		uint tick = 0;
		do
		{
			tick++;
			InfoBox info = new("#" + tick, new Padding(1, 0, 1, 0), ["Katerine".Align(TextAlign.Center)],
			[
			new InfoBox.Section("Stats".Align(TextAlign.Center),
				"health: 20/20",
				"lust: 30%"
			),
			new InfoBox.Section("Equipment".Align(TextAlign.Center),
				"Head: Iron Bucket",
				"Torso: Iron Breastplate",
				"Legs: None",
				"Feet: None"
			),
			]);

			info.GetSize(out uint w, out uint h);
			char[][] buffer = CreateMatrix(w, h);
			info.Draw(buffer);
			for (int i = 0; i < buffer.Length; i++)
			{
				Console.Write(new string(buffer[i]));
				Console.WriteLine();
			}

			Console.ForegroundColor = ConsoleColor.Black;
			Console.BackgroundColor = ConsoleColor.Black;
			key = Console.ReadKey();
			Console.CursorLeft = 0;
			Console.CursorTop = 0;
			Console.ResetColor();
		}
		while (key.Key != ConsoleKey.Escape);
	}

	private static char[][] CreateMatrix(uint w, uint h)
	{
		char[][] result = new char[h][];
		for (int i = 0; i < h; i++)
		{
			result[i] = new char[w];
		}

		return result;
	}
}