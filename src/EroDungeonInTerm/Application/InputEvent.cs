using System;

namespace EroDungeonInTerm.Application;

public readonly record struct InputEvent(ConsoleKeyInfo Info, bool Virtual = false)
{
	public override string ToString()
	{
		return $"{{{Info.Modifiers}, {Info.Key}, Virtual = {Virtual}}}";
	}
}