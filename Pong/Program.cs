﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
	class Program
	{
		static void Main()
		{
			var game = new Pong();
			game.Run(60);
		}
	}
}
