using System;

namespace EroDungeonInTerm.Application;

public readonly record struct InputEvent(ConsoleKeyInfo Info, bool Virtual = false);