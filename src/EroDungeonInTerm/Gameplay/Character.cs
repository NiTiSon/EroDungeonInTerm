namespace EroDungeonInTerm.Gameplay;

public class Character
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
}