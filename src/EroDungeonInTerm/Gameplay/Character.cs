using System.IO;

namespace EroDungeonInTerm.Gameplay;

public class Character : ISavable<Character>
{
	private Race race;
	private string name;

	/// <summary>
	/// Basic character's stats (without any buffs and equipment bonuses).
	/// </summary>
	private BaseStats stats;

	public Character(Race race, string name, in BaseStats stats)
	{
		this.race = race;
		this.name = name;
		this.stats = stats;
	}

	public static Character Load(BinaryReader save)
	{
		throw new System.NotImplementedException();
	}

	public void Save(BinaryWriter save)
	{
		save.Write((byte)1); // The save format version used for backward-compatibility

		save.Write((byte)race);
		save.WriteUtf8(name);
	}
}