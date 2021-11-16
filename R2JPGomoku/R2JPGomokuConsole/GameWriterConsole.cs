using R2JPGomokuLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace R2JPGomokuConsole {
    public class GameWriterConsole : IGameWriter {
        void IGameWriter.WriteGame(string game) {
            Console.Write(game);
        }
    }
}
