using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Pong
{
	class Paddle
	{
		public Vector2 Dimensions { get; set; }
		public Vector3 Location { get; set; }
		private PaddleType type;

		public Paddle(int width, int height, PaddleType type)
		{
			Dimensions = new Vector2(width, height);
			this.type = type;
			Location = (type == PaddleType.Player)
				? new Vector3(-480, 0, 0)
				: new Vector3( 480, 0, 0);
		}

		public void Render()
		{

			GL.LoadIdentity();
			GL.Translate(Location);
			GL.Begin(BeginMode.Quads);

			GL.Vertex2(-Dimensions.X/2, -Dimensions.Y/2);
			GL.Vertex2(Dimensions.X/2, -Dimensions.Y/2);
			GL.Vertex2(Dimensions.X / 2, Dimensions.Y / 2);
			GL.Vertex2(-Dimensions.X / 2, Dimensions.Y / 2);

			GL.End();
		}
	}
}
