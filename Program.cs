using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Grafica_PDesktop
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(900, 600))
            {
                game.Run(60.0); // 60 FPS
            }


        }
    }
}
