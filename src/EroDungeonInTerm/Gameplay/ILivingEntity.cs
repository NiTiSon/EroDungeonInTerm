namespace EroDungeonInTerm.Gameplay;

public interface ILivingEntity : IEntity
{
	/// <summary>
	/// The entity takes a hit.
	/// </summary>
	/// <param name="hit">Hit info.</param>
	/// <returns>Dealed damage.</returns>
	uint TakeHit(in Hit hit);

	/// <summary>
	/// The entity takes a instant heal.
	/// </summary>
	/// <param name="amount">Healing amount.</param>
	/// <returns>Amount of restored health.</returns>
	uint TakeHeal(in Heal heal);
}