using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static R2JPGomokuLib.GameController;

namespace R2JPGomokuWin {
    public class Board {
        public class Cell {
            public Cell(int x, int y, Button button) {
                X = x;
                Y = y;
                Button = button;
            }

            public int X { get; set; }
            public int Y { get; set; }
            public Button Button { get; set; }
        }

        private readonly Panel panel;
        private List<List<Cell>> cells;

        public Board(Panel panel) {
            this.panel = panel;
        }

        public void DrawBoard(GameResponse response) {
            var size = response.board.Count;
            var offset = 45;
            if (cells == null || !cells.Count.Equals(size)) {
                panel.Controls.Clear();
                cells = new List<List<Cell>>();

                for (int y = 0; y < size; y++) {
                    var row = new List<Cell>();
                    for (int x = 0; x < size; x++) {
                        var b = new Button();
                        b.Click += CellClicked;
                        b.Width = offset;
                        b.Height = offset;
                        b.Left = x * offset;
                        b.Top = y * offset;
                        panel.Controls.Add(b);
                        row.Add(new Cell(x, y, b));
                    }
                    cells.Add(row);
                }

                for (int y = 0; y < size; y++) {
                    for (int x = 0; x < size; x++) {
                        cells[y][x].Button.Text = response.board[y][x] ?? "-";
                    }
                }
            }
        }

        private void CellClicked(object sender, EventArgs e) {
            var buttons = cells.SelectMany(r => r.Select(c => c.Button));
            var b = buttons.Single(b => b.Equals(sender));
            b.Text = "X";
        }

    }
}
