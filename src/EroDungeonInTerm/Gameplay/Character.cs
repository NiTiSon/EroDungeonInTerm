using EroDungeonInTerm.Application.UI;
using EroDungeonInTerm.Rendering;
using System.IO;
using System.Net.NetworkInformation;

namespace EroDungeonInTerm.Gameplay;

public class Character : ISavable<Character>, ILivingEntity
{
	private Race race;
	private Sex sex;
	private string name;

	/// <summary>
	/// Basic character's stats (without any buffs and equipment bonuses).
	/// </summary>
	private BaseStats stats;
	private uint health;

	public Character(Race race, Sex sex, string name, in BaseStats stats)
	{
		this.race = race;
		this.sex = sex;
		this.name = name;
		this.stats = stats;
		this.health = stats.MaxHealth;
	}

	public uint TakeHit(in Hit hit)
	{
		uint damage = hit.Damage;
		if (damage < stats.Defense)
		{
			damage = 1;
		}
		else
		{
			damage -= stats.Defense;
		}

		damage = uint.Min(health, damage); // Preventing underflowing
		health -= damage;
		return damage;
	}

	public uint TakeHeal(in Heal heal)
	{
		uint amount = heal.Amount;
		uint lostHealth = stats.MaxHealth - health;

		amount = uint.Min(lostHealth, amount);
		health += amount;
		return amount;
	}

	public InfoBox GetInfo()
	{
		return new InfoBox(
			name,
			[
				$"{race} {(sex == Sex.Asexual ? ' ' : sex == Sex.Male ? '♂' : '♀')}"
			],
			[
				new InfoBox.Section("Stats".Align(HorizontalAlignment.Center),
				$"hp: {health}/{stats.MaxHealth}",
				$"def: {stats.Defense}",
				$"atk: {stats.Attack}",
				$"crt: {stats.CriticalChance * 100:0.##}% +{stats.CriticalDamage * 100:0.##}%",
				$"spd: {stats.Speed}"
				),
			]
		);
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