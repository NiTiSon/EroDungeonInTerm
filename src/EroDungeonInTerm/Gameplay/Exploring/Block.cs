using System;

namespace EroDungeonInTerm.Gameplay.Exploring;

public class Block
{
	public char Texture { get; }
	public ConsoleColor Foreground { get; }

	public Block(char texture, ConsoleColor foreground)
	{
		Texture = texture;
		Foreground = foreground;
	}
}