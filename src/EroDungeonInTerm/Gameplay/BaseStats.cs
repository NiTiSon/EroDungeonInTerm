namespace EroDungeonInTerm.Gameplay;

public struct BaseStats
{
	public uint MaxHealth;
	public uint Defense;
	public uint Attack;
	public uint Speed;
	public float CriticalChance;
	public float CriticalDamage;

	public BaseStats()
	{
		MaxHealth = 200;
		Defense = 10;
		Attack = 40;
		Speed = Rules.SpeedRequiredToMove;
		CriticalChance = 0.15f; // +15% CRT 
		CriticalDamage = 0.60f; // +60% CRT DMG
	}

	public BaseStats(uint maxHealth, uint defense, uint attack, uint speed, float critChance, float critDamage)
	{
		MaxHealth = maxHealth;
		Defense = defense;
		Attack = attack;
		Speed = speed;
		CriticalChance = critChance;
		CriticalDamage = critDamage;
	}
}