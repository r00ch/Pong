using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Pong
{
	class Pong : GameWindow
	{
		private uint gameWindowWidth = 1000;
		private uint gameWindowHeight = 500;
		private bool gotResized = false;

		Paddle playerPaddle = new Paddle(20, 100, PaddleType.Player);
		Paddle cpuPaddle = new Paddle(20, 100, PaddleType.CPU);

		bool pressedUp = false;
		bool pressedDown = false;

		private void SetWindowSize()
		{
			this.Width = (int)gameWindowWidth;
			this.Height = (int)gameWindowHeight;
		}
		protected override void OnLoad(EventArgs e)
		{
			GL.ClearColor(Color.Black);
			SetWindowSize();
		}
		protected override void OnResize(EventArgs e)
		{
			GL.Viewport(0, 0, this.Width, this.Height);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Ortho(-this.Width / 2, this.Width/ 2, -this.Height/ 2, this.Height/ 2, -1, 1);
			GL.MatrixMode(MatrixMode.Modelview);
			gotResized = true;
		}
		protected override void OnKeyDown(KeyboardKeyEventArgs e)
		{
			if (e.Key == Key.Up) pressedUp = true;
			if (e.Key == Key.Down) pressedDown = true;
		}
		protected override void OnKeyUp(KeyboardKeyEventArgs e)
		{
			if (e.Key == Key.Up) pressedUp = false;
			if (e.Key == Key.Down) pressedDown = false;
		}
		private void MovePlayerPaddle()
		{
			if (pressedUp && playerPaddle.Location.Y < this.Height/2 - playerPaddle.Dimensions.Y/2)
				playerPaddle.Location = new Vector3(-480, playerPaddle.Location.Y + 5, 0);
			else if (pressedDown && playerPaddle.Location.Y > -this.Height / 2 + playerPaddle.Dimensions.Y / 2)
				playerPaddle.Location = new Vector3(-480, playerPaddle.Location.Y - 5, 0);
		}
		protected override void OnRenderFrame(FrameEventArgs e)
		{
			if (gotResized) 
			{
				SetWindowSize();
				gotResized = false;
			}
			MovePlayerPaddle();

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			Dummy.Create();
			playerPaddle.Render();
			cpuPaddle.Render();

			this.SwapBuffers();
		}
	}
}
