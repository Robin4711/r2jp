using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace R2JPGomokuLib {
    public class Board {
        private readonly List<List<string>> board;
        private readonly string playCharacter;

        public class Move {
            public int X { get; set; }
            public int Y { get; set; }
        }

        

        public Board(List<List<string>> board, string playCharacter) {
<<<<<<< HEAD
            this.playCharacter = playCharacter;
=======
>>>>>>> make_move
            
            foreach(var row in board)
            {
                for (int i = 0; i < row.Count; i++)
                {
                    if (row[i] == null)
                        row[i] = "-";
                    else if (row[i] == playCharacter)
                        row[i] = "m";
                    else
                        row[i] = "p";
                }
            }
            this.board = board;
            this.playCharacter = playCharacter;
        }

        public List<List<string>> Rows() {
            return board;
        }

        public Move NextMove() {
            var rowsAsStrings = Rows().Select(r => String.Join(string.Empty, r)).ToList();
            var y = 0;
            var x = 0;
            for (int i = 0; i < rowsAsStrings.Count(); i++)
            {
                x = xOfFoundNextMove(rowsAsStrings[i], "mmmm-");
                if (x != -1)
                {
                    y = i;
                    return new Move { X = x, Y = y };
                }
                x = xOfFoundNextMove(rowsAsStrings[i], "-mmmm");
                if (x != -1)
                {
                    y = i;
                    return new Move { X = x, Y = y };
                }

                x = xOfFoundNextMove(rowsAsStrings[i], "-mp");
                if (x != -1)
                {
                    y = i;
                    return new Move { X = x, Y = y };
                }

                x = xOfFoundNextMove(rowsAsStrings[i], "p-");
                if (x != -1)
                {
                    y = i;
                    return new Move { X = x, Y = y };
                }

            }

            return new Move() { X =  Rows().First().Count / 2,  Y = Rows().Count / 2 } ;
        }

        private int xOfFoundNextMove(string row, string pattern)
        {
            if (row.Contains(pattern))
            {
                var offset = pattern.IndexOf("-");
                return row.IndexOf(pattern) + offset;

            }
            return -1;
        }

    }
}
