using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;

namespace Pong
{
	class Ball : GameObject
	{
		public uint Mass { get; set; }
		public float Speed { get; set; }
		public Vector3 MovementDirection { get; set; }

		public Ball(uint width, uint height, Vector3 location)
		{
			this.Dimensions = new Vector2(width, height);
			this.Location = location;
		}
		public void GenerateBallMovement()
		{//przy kącie 27 - 0.3f to jest max Y - losujemy liczbe z przesziału -0.3 do 0.3
			if (this.Speed == 0) this.Speed = 10;

			Random rnd = new Random();
			float rndYValue = rnd.Next(0, 60);
			float yValue = (30 - rndYValue) / 100;
			this.MovementDirection = new Vector3(-1-yValue, yValue,0);
		}
		public void Move()
		{
			this.Location += MovementDirection * Speed;
		}
		public void CalculateCollision(uint gameWindowWidth, uint gameWindowHeight)
		{
			if (this.Location.Y + this.Dimensions.Y + 10 >= gameWindowHeight / 2 || 
				this.Location.Y - this.Dimensions.Y - 10 <= -gameWindowHeight / 2)
				this.MovementDirection *= new Vector3(1, -1, 1);
			if (this.Location.X >= gameWindowWidth / 2 ||
				this.Location.X <= -gameWindowWidth / 2)
			{
				this.MovementDirection = new Vector3(0, 0, 0);
				this.Location = new Vector3(0, 0, 0);
			}
		}
		public void CalculateCollision(Paddle paddle)
		{
			if (this.Location.Y - this.Dimensions.Y / 2 <= paddle.Location.Y + paddle.Dimensions.Y / 2 &&
				this.Location.Y + this.Dimensions.Y / 2 >= paddle.Location.Y - paddle.Dimensions.Y / 2 )
			{
				if (this.Location.X - this.Dimensions.X / 2 <= paddle.Location.X + paddle.Dimensions.X / 2 &&
					this.Location.X + this.Dimensions.X / 2 >= paddle.Location.X - paddle.Dimensions.X / 2)
				{
					this.MovementDirection *= new Vector3(-1, 1, 1);
					if (paddle.Type == PaddleType.Player && paddle.Moving != MoveType.None)
					{
						if (paddle.Moving == MoveType.Up) this.MovementDirection = new Vector3(this.MovementDirection.X, this.MovementDirection.Y + 0.2f,0);
						else this.MovementDirection = new Vector3(this.MovementDirection.X, this.MovementDirection.Y - 0.2f, 0);
					}
				}	
			}
		}
	}
}
