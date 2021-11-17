using Newtonsoft.Json;
using R2JPGomokuLib;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using static R2JPGomokuLib.GameController;

namespace R2JPGomokuWin {
    public class GameWriterWin : IGameWriter {
        private readonly Board board;

        public GameWriterWin(Board board) {
            this.board = board;
        }

        public void WriteError(string error) {
            //throw new NotImplementedException();
        }

        public void WriteGame(string game) {
            var gameResponse = JsonConvert.DeserializeObject<GameResponse>(game);
            board.DrawBoard(gameResponse);
        }

        public void WriteGamePretty(string game) {
            //throw new NotImplementedException();
        }

        public void WriteWinner(string winner) {
            //throw new NotImplementedException();
        }
    }
}
