using EroDungeonInTerm.Rendering;
using System.Collections.Generic;

namespace EroDungeonInTerm.Application.UI;

public class HBox : UIElement
{
	private readonly List<UIElement> elements;
	private readonly VerticalAlignment alignment;

	public HBox()
	{
		elements = new(4);
	}

	public HBox(IEnumerable<UIElement> elements)
	{
		this.elements = new(elements);
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
		uint horizontalPointer = 0;
		for (int i = 0; i < elements.Count; i++)
		{
			UIElement element = elements[i];
			element.GetSize(out uint elementWidth, out uint elementHeight);

			element.Draw(render.Slice(0, horizontalPointer, elementHeight, elementWidth));

			horizontalPointer += elementWidth;
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
			height = uint.Max(height, elementHeight);
			width += elementWidth;
		}
	}
}