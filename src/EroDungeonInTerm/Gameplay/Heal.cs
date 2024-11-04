namespace EroDungeonInTerm.Gameplay;

public readonly struct Heal
{
	public readonly uint Amount;
	public readonly IEntity Dealer;

	public Heal(IEntity dealer, uint amount)
	{
		Amount = amount;
		Dealer = dealer;
	}
}