using R2JPGomokuLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace R2JPGomokuConsole {
    public class GameWriterConsole : IGameWriter {
        public void WriteError(string error) {
            Console.WriteLine($"Error: {error}");
        }

        public void WriteGamePretty(string game) {
            Console.Write(game);
        }

        public void WriteWinner(string winner) {
            Console.WriteLine($"Winner: {winner}");
        }

        void IGameWriter.WriteGame(string game) {
            //Console.Write(game);
            //Console.Write(game);
        }
    }
}
