using System;

namespace EroDungeonInTerm.Gameplay.Exploring;

public partial class Map
{
	private readonly Block?[] map;
	private readonly uint rows, columns;

	public uint Rows => rows;
	public uint Columns => columns;

	public Map(Block?[] map, uint rows, uint columns)
	{
		if (rows * columns != map.Length)
		{
			throw new ArgumentException("Map creation failed: the sizes are wrong.");
		}

		this.map = map;
		this.rows = rows;
		this.columns = columns;
	}

	public Block? this[uint row, uint column]
	{
		get
		{
			ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(row, rows);
			ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(column, columns);

			return map[row * this.columns + column];
		}
	}
}