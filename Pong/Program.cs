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
			Console.WriteLine("Wilkomen!");
			Console.WriteLine("Aby rozpocząć klepnij SPACE");
			Console.WriteLine("Żeby ruszyć paletką wciśnij klawisze UP i DOWN");
			Console.WriteLine("WAŻNE: Paletka w ruchu wpływa na kąt odbicia piłki!");

			var game = new Pong();
			game.Run(10);
		}
	}
}
