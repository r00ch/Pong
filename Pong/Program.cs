using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace Pong
{
	class Program
	{
		static void Main()
		{
			var game = new Pong();
			game.Run(10);
		}
	}
}
