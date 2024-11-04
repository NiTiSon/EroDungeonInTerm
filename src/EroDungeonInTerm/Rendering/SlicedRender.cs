using System;

namespace EroDungeonInTerm.Rendering;

internal sealed class SlicedRender : Render
{
	private readonly Render render;
	private readonly uint offsetRow, offsetColumn;
	private readonly uint height, width;

	public SlicedRender(Render owner, uint offsetRow, uint offsetColumn, uint height, uint width)
	{
		this.render = owner;
		this.offsetRow = offsetRow;
		this.offsetColumn = offsetColumn;
		this.height = height;
		this.width = width;
	}

	public override void Place(char ch, uint row, uint column)
	{
		ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(row, height);
		ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(column, width);
		
		render.Place(ch, offsetRow + row, offsetColumn + column);
	}

	public override Render Slice(uint row, uint column, uint height, uint width)
	{
		return new SlicedRender(render, offsetRow + row, offsetColumn + column, height, width);
	}
}