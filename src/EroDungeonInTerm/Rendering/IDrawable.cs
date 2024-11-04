namespace EroDungeonInTerm.Rendering;

public interface IDrawable
{
	void GetSize(out uint width, out uint height);

	void Draw(Render render);
}