using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using static R2JPGomokuLib.GameController;

namespace R2JPGomokuWin {
    public class Board {
        private readonly Panel panel;
        private List<List<Button>> cells;

        public Board(Panel panel) {
            this.panel = panel;
        }

        public void DrawBoard(GameResponse response) {
            var size = response.board.Count;
            var offset = 45;
            if (cells == null || !cells.Count.Equals(size)) {
                panel.Controls.Clear();
                cells = new List<List<Button>>();

                for (int y = 0; y < size; y++) {
                    var row = new List<Button>();
                    for (int x = 0; x < size; x++) {
                        var b = new Button();
                        b.Click += CellClicked;
                        b.Width = offset;
                        b.Height = offset;
                        b.Left = x * offset;
                        b.Top = y * offset;
                        panel.Controls.Add(b);
                        row.Add(b);
                    }
                    cells.Add(row);
                }

                for (int y = 0; y < size; y++) {
                    var row = new List<Button>();
                    for (int x = 0; x < size; x++) {
                        cells[y][x].Text = response.board[y][x] ?? "-";
                    }
                    cells.Add(row);
                }
            }
        }

        private void CellClicked(object sender, EventArgs e) {

        }

    }
}
