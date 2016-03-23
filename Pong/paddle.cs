using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Pong 
{
	class Paddle : GameObject
	{
		public PaddleType Type { set; get; }

		public Paddle(int width, int height, PaddleType type)
		{
			Dimensions = new Vector2(width, height);
			this.Type = type;
			Location = (type == PaddleType.Player)
				? new Vector3(-480, 0, 0)
				: new Vector3( 480, 0, 0);
		}
	}
}
