using System.IO;

namespace EroDungeonInTerm.Gameplay;

internal interface ISavable<out T>
{
	void Save(BinaryWriter save);

	static abstract T Load(BinaryReader save);
}