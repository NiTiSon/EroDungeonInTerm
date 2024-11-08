﻿using EroDungeonInTerm.Rendering;
using System.Collections.Generic;

namespace EroDungeonInTerm.Application.UI;

public class VBox : UIElement
{
	private readonly List<UIElement> elements;
	private readonly HorizontalAlignment alignment;

	public VBox()
	{
		elements = new(4);
	}

	public VBox(IEnumerable<UIElement> elements)
	{
		this.elements = new(elements);
		this.elements.ForEach(el => el.Parent = this);
	}

	public void Add(UIElement element)
	{
		elements.Add(element);
		element.Parent = this;
	}

	public bool Remove(UIElement element)
	{
		if (elements.Remove(element))
		{
			element.Parent = null;
			return true;
		}

		return false;
	}

	public override void Draw(Render render)
	{
		GetSize(out uint width, out uint height);
		uint verticalPointer = 0;
		for (int i = 0; i < elements.Count; i++)
		{
			UIElement element = elements[i];
			element.GetSize(out uint elementWidth, out uint elementHeight);

			element.Draw(render.Slice(verticalPointer, 0, elementHeight, elementWidth));

			verticalPointer += elementHeight;
		}
	}

	public override void GetSize(out uint width, out uint height)
	{
		width = 0;
		height = 0;
		for (int i = 0; i < elements.Count; i++)
		{
			UIElement element = elements[i];
			element.GetSize(out uint elementWidth, out uint elementHeight);
			width = uint.Max(width, elementWidth);
			height += elementHeight;
		}
	}
}