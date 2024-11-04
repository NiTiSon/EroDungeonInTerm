namespace EroDungeonInTerm.Gameplay;

public struct BaseStats
{
	public uint MaxHealth;
	public uint Defense;
	public uint Attack;
	public uint Speed;

	public BaseStats()
	{
		MaxHealth = 200;
		Defense = 10;
		Attack = 40;
		Speed = Rules.SpeedRequiredToMove;
	}

	public BaseStats(uint maxHealth, uint defense, uint attack, uint speed)
	{
		MaxHealth = maxHealth;
		Defense = defense;
		Attack = attack;
		Speed = speed;
	}
}