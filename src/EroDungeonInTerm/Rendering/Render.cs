using System;
using System.Reflection;

namespace EroDungeonInTerm.Rendering;

public abstract class Render
{
	public abstract Render Slice(uint row, uint column, uint height, uint width);

	public abstract void Place(char ch, uint row, uint column);
}

public sealed class MatrixRender : Render
{
	private char[][] matrix;

	public uint RowsCount => (uint)matrix.Length;

	public uint ColumnsCount => (uint)matrix[0].Length;

	public MatrixRender(uint height, uint width)
	{
		matrix = new char[height][];
		for (int i = 0; i < height; i++)
		{
			matrix[i] = new char[width];
		}
	}

	public override void Place(char ch, uint row, uint column)
	{
		ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(row, RowsCount);
		ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(column, ColumnsCount);
		matrix[row][column] = ch;
	}

	public override Render Slice(uint row, uint column, uint height, uint width)
	{
		return new SlicedRender(this, row, column, height, width);
	}
}