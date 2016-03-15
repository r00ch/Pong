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

			bool isLeftMouseDown = false;
			bool isRightMouseDown = false;
			Vector2 mousePosition = new Vector2();

			Triangle triangle;

			triangle.a = new Vector2(-1.0f, 1.0f);
			triangle.b = new Vector2(0.0f, -0.73f);
			triangle.c = new Vector2(1.0f, 1.0f);

			float rotateAngle = 0;

			using (var game = new GameWindow(600, 600))
			{
				game.KeyDown += (sender, e) =>
				{
					if (e.Key == Key.Left) leftVertice = ChangeVerticeColour(leftVertice);
					if (e.Key == Key.Down) bottomVertice = ChangeVerticeColour(bottomVertice);
					if (e.Key == Key.Right) rightVertice = ChangeVerticeColour(rightVertice);
				};

				game.MouseMove += (sender, e) =>
				{
					Vector2 newMousePosition = new Vector2(Mouse.GetCursorState().X, Mouse.GetCursorState().Y);
					if (isLeftMouseDown)
					{
						triangle.a.X = triangle.a.X - (mousePosition.X - newMousePosition.X) / 180;
						triangle.a.Y = triangle.a.Y + (mousePosition.Y - newMousePosition.Y) / 180;

						triangle.b.X = triangle.b.X - (mousePosition.X - newMousePosition.X) / 180;
						triangle.b.Y = triangle.b.Y + (mousePosition.Y - newMousePosition.Y) / 180;

						triangle.c.X = triangle.c.X - (mousePosition.X - newMousePosition.X) / 180;
						triangle.c.Y = triangle.c.Y + (mousePosition.Y - newMousePosition.Y) / 180;

						mousePosition = newMousePosition;
					}
					if (isRightMouseDown)
					{
						rotateAngle = rotateAngle+(mousePosition.X - newMousePosition.X)/5;
						mousePosition = newMousePosition;
					}
				};

				game.MouseDown += (sender, e) =>
				{
					Console.WriteLine("Mouse button up: " + e.Button + " at: " + e.Position);
					if (e.Button == MouseButton.Left) isLeftMouseDown = true;
					if (e.Button == MouseButton.Right) isRightMouseDown = true;

					mousePosition = new Vector2(Mouse.GetCursorState().X, Mouse.GetCursorState().Y);
				};

				game.MouseUp += (sender, e) =>
				{
					if (e.Button == MouseButton.Left) isLeftMouseDown = false;
					if (e.Button == MouseButton.Right) isRightMouseDown = false;
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
					GL.Ortho(-2.0, 2.0, -2.0, 2.0, 0.0, 4.0);

					GL.Rotate(rotateAngle, Vector3.UnitZ);

					GL.Begin(PrimitiveType.Triangles);

					GL.Color3(SetVerticeColour(leftVertice));
					GL.Vertex2(triangle.a);
					GL.Color3(SetVerticeColour(bottomVertice));
					GL.Vertex2(triangle.b);
					GL.Color3(SetVerticeColour(rightVertice));
					GL.Vertex2(triangle.c);
					
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
	struct Triangle
	{
		public Vector2 a;
		public Vector2 b;
		public Vector2 c;
	}
}