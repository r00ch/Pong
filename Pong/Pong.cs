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
		Ball ball = new Ball(20, 20, new Vector3());

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
			if (e.Key == Key.Up)
			{
				pressedUp = true;
				playerPaddle.Moving = MoveType.Up;
			}
			if (e.Key == Key.Down)
			{
				pressedDown = true;
				playerPaddle.Moving = MoveType.Down;
			}
			if (pressedUp && pressedDown) playerPaddle.Moving = MoveType.None;
		}
		protected override void OnKeyUp(KeyboardKeyEventArgs e)
		{
			if (e.Key == Key.Up)pressedUp = false;
			if (e.Key == Key.Down)pressedDown = false;
			if (e.Key == Key.Space) ball.GenerateBallMovement();
			if (!pressedDown && !pressedUp) playerPaddle.Moving = MoveType.None;
		}
		private void MovePlayerPaddle()
		{
			if (pressedUp && playerPaddle.Location.Y < this.Height / 2 - playerPaddle.Dimensions.Y / 2)
				playerPaddle.Move(new Vector3(0, 5, 0));
			if (pressedDown && playerPaddle.Location.Y > -this.Height / 2 + playerPaddle.Dimensions.Y / 2)
				playerPaddle.Move(new Vector3(0, -5, 0));
		}
		private void MoveCpuPaddle()
		{
			if (cpuPaddle.Location.Y < ball.Location.Y &&
				cpuPaddle.Location.Y < this.Height / 2 - cpuPaddle.Dimensions.Y / 2)
				cpuPaddle.Move(new Vector3(0, 3, 0));
			else if (cpuPaddle.Location.Y > ball.Location.Y &&
				cpuPaddle.Location.Y > -this.Height / 2 + cpuPaddle.Dimensions.Y / 2)
				cpuPaddle.Move(new Vector3(0, -3, 0));
		}
		protected override void OnRenderFrame(FrameEventArgs e)
		{
			if (gotResized) 
			{
				SetWindowSize();
				gotResized = false;
			}

			MovePlayerPaddle();
			MoveCpuPaddle();

			ball.CalculateCollision(gameWindowWidth, gameWindowHeight);
			ball.CalculateCollision(playerPaddle);
			ball.CalculateCollision(cpuPaddle);
			ball.Move();
			
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			playerPaddle.Render();
			cpuPaddle.Render();
			ball.Render();

			this.SwapBuffers();
		}
	}
}
