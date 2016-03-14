using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Example
{
	class MyApplication
	{
		[STAThread]
		public static void Main()
		{
			VerticeColour leftVertice = VerticeColour.Blue;
			VerticeColour bottomVertice = VerticeColour.Green;
			VerticeColour rightVertice = VerticeColour.Ivory;

			using (var game = new GameWindow())
			{
				game.KeyUp += (sender, e) =>
				{
					if (e.Key == Key.Left) leftVertice = ChangeVerticeColour(leftVertice);
					if (e.Key == Key.Down) bottomVertice = ChangeVerticeColour(bottomVertice);
					if (e.Key == Key.Right) rightVertice = ChangeVerticeColour(rightVertice);
				};

				game.Load += (sender, e) =>
				{
					game.VSync = VSyncMode.On;
				};

				game.Resize += (sender, e) =>
				{
					GL.Viewport(0, 0, game.Width, game.Height);
				};

				game.UpdateFrame += (sender, e) =>
				{
					if (game.Keyboard[Key.Escape])
					{
						game.Exit();
					}
				};

				game.RenderFrame += (sender, e) =>
				{
					GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

					GL.MatrixMode(MatrixMode.Projection);
					GL.LoadIdentity();
					GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);

					GL.Begin(PrimitiveType.Triangles);

					GL.Color3(SetVerticeColour(leftVertice));
					GL.Vertex2(-1.0f, 1.0f);
					GL.Color3(SetVerticeColour(bottomVertice));
					GL.Vertex2(0.0f, -1.0f);
					GL.Color3(SetVerticeColour(rightVertice));
					GL.Vertex2(1.0f, 1.0f);

					GL.End();

					game.SwapBuffers();
				};

				game.Run(60.0); //60 fps
			}
		}
	
		static Color SetVerticeColour(VerticeColour vc)
		{
			switch (vc)
			{
				case VerticeColour.Blue:
					return Color.MidnightBlue;
				case VerticeColour.Green:
					return Color.SpringGreen;
				case VerticeColour.Ivory:
					return Color.Ivory;
				default:
					return Color.White;
			}
		}
		static VerticeColour ChangeVerticeColour(VerticeColour vc)
		{
			switch (vc)
			{
				case VerticeColour.Blue:
					return VerticeColour.Green;
				case VerticeColour.Green:
					return VerticeColour.Ivory;
				case VerticeColour.Ivory:
					return VerticeColour.Blue;
				default:
					break;
			}
			return VerticeColour.Blue;
		}
	}
	enum VerticeColour
	{
		Blue,
		Green,
		Ivory
	}
}