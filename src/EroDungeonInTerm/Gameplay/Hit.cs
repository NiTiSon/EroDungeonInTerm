using System.Runtime.InteropServices;

namespace EroDungeonInTerm.Gameplay;

public readonly struct Hit
{
	public readonly uint Damage;
	public readonly bool IsCritical;
	public readonly IEntity Dealer;

	public Hit(IEntity dealer, uint damage, [Optional] bool isCritical)
	{
		Damage = damage;
		IsCritical = isCritical;
		Dealer = dealer;
	}
}