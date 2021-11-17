using System;
using System.Collections.Generic;
using System.Text;

namespace R2JPGomokuLib {
    public interface IGameWriter {
        void WriteGame(string game);
        void WriteGamePretty(string game);
        void WriteWinner(string winner);
        void WriteError(string error);
    }
}
