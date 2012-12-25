using System;

namespace project2006
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
#if DEBUG
            using (Game1 game = new Game1())
            {
                game.Run();
            }
#elif !DEBUG
            using (GameMain game = new GameMain())
            {
                game.Run();
            }
#endif
        }
    }
}

