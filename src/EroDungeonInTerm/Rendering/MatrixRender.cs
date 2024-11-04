using System;

namespace EroDungeonInTerm.Rendering;

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

	public void Flush()
	{
		for (uint row = 0; row < matrix.Length; row++)
		{
			Console.WriteLine(matrix[row]);
		}
	}

	public override void Place(uint row, uint column, char ch)
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