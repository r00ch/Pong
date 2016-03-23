using OpenTK;

namespace Pong
{
	interface IRenderable
	{
		Vector2 Dimensions { get; set; }
		Vector3 Location { get; set; }
		void Render();
	}
}
