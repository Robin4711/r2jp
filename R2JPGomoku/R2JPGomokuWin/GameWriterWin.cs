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
        private readonly TextBox message;

        public GameWriterWin(Board board, TextBox message) {
            this.board = board;
            this.message = message;
        }

        public void WriteError(string error) {
            message.Text = error;
        }

        public void WriteGame(string game) {
            GameResponse gameResponse = null;
            try {
                gameResponse = JsonConvert.DeserializeObject<GameResponse>(game);
            }
            catch (Exception e) {
                WriteError(game);
                return;
            }
            board.DrawBoard(gameResponse);
        }

        public void WriteGamePretty(string game) {
            //throw new NotImplementedException();
        }

        public void WriteWinner(string winner) {
            message.Text = winner;
        }
    }
}
