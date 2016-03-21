using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Pong
{
	public static class Dummy
	{
		public static void Create()
		{
			GL.LoadIdentity();
			GL.Begin(BeginMode.Quads);

			GL.Color3(Color.Red);
			GL.Vertex2(-25,-25);

			GL.Color3(Color.Yellow);
			GL.Vertex2( 25,-25);

			GL.Color3(Color.Green);
			GL.Vertex2( 25, 25);

			GL.Color3(Color.Blue);
			GL.Vertex2(-25, 25);

			GL.End();
		}
	}
}
