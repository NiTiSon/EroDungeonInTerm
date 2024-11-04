using EroDungeonInTerm.Rendering;
using System.Collections.Generic;

namespace EroDungeonInTerm.Application.UI;

public class VBox : UIElement
{
	private readonly List<UIElement> elements;

	public VBox()
	{
		elements = new(4);
	}

	public VBox(IEnumerable<UIElement> elements)
	{
		this.elements = new(elements);
	}

	public void Add(UIElement element)
	{
		elements.Add(element);
	}

	public bool Remove(UIElement element)
	{
		return elements.Remove(element);
	}

	public override void Draw(Render render)
	{
		throw new System.NotImplementedException();
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