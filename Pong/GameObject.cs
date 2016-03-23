using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Pong
{
	class GameObject : IRenderable
	{
		public Vector2 Dimensions { get; set; }
		public Vector3 Location { get; set; }

		public void Move(Vector3 movementVector)
		{
			this.Location += movementVector;
		}
		public void Render()
		{
			GL.LoadIdentity();
			GL.Translate(Location);
			GL.Begin(BeginMode.Quads);

			GL.Vertex2(-Dimensions.X / 2, -Dimensions.Y / 2);
			GL.Vertex2(Dimensions.X / 2, -Dimensions.Y / 2);
			GL.Vertex2(Dimensions.X / 2, Dimensions.Y / 2);
			GL.Vertex2(-Dimensions.X / 2, Dimensions.Y / 2);

			GL.End();
		}
	}
}
