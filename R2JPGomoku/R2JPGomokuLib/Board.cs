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
            this.playCharacter = playCharacter;
            
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
        }

        public List<List<string>> Rows() {
            return board;
        }

        public List<List<string>> Columns() {
            var columns = new List<List<string>>();
            for (int i = 0; i < board.Count; i++) {
                var c = new List<string>();
                for (int j = 0; j < board.Count; j++) {
                    c.Add(board[j][i]);
                }
                columns.Add(c);
            }
            return columns;
        }

        public List<List<string>> LeftDiagonals() {
            var diagonals = new List<List<string>>();
            var length = board.Count;

            diagonals.Add(GetDiagonal(board, 0, 0, 1, 1));
            diagonals.Add(GetDiagonal(board, 0, length-1, -1, 1));

            for (int i = 1; i < length; i++) {
                diagonals.Add(GetDiagonal(board, i, 0, 1, 1));
                diagonals.Add(GetDiagonal(board, 0, i, 1, 1));
                diagonals.Add(GetDiagonal(board, 0, i, -1, 1));
                diagonals.Add(GetDiagonal(board, i, length-1, -1, 1));
            }
            return diagonals;
        }

        private List<string> GetDiagonal(List<List<string>> board, int x, int y, int stepX, int stepY) {
            var result = new List<string>();
            var i = x;
            var j = y;
            var min = 0;
            var max = board.Count-1;

            while (min <= i && min <= j && i <= max && j <= max) {
                result.Add(board[j][i]);
                i = i + stepX;
                j = j + stepY;
            }
            return result;
        }

        public Move NextMove() {
            var rowsAsStrings = Rows().Select(r => String.Join(string.Empty, r)+"R").ToList();
            var colsAsStrings = Columns().Select(r => String.Join(string.Empty, r) + "C").ToList();
            var diagonalsAsStrings = LeftDiagonals().Select(r => String.Join(string.Empty, r) + "D").ToList();
            Move x = null;

            var patterns = new List<string>() {
                "mmmm-",
                "m-mmm", 
                "mm-mm", 
                "mmm-m", 
                "-mmmm", 
                "p-ppp",  
                "pp-pp", 
                "ppp-p", 
                "-pppp", 
                "pppp-",
                "-mmm",
                "m-mm",
                "mm-m",
                "mmm-", 
                "ppp-", 
                "-ppp", 
                "pp-p",
                "p-pp",
                "mm-", 
                "-mm",
                "m-m",
                "pp-", 
                "-pp", 
                "-m",
                "m-", 
                "p-", 
                "-p" };
            foreach (var pattern in patterns) {
                for (int i = 0; i < rowsAsStrings.Count(); i++)
                {
                    x = xOfFoundNextMove(rowsAsStrings[i], pattern, i);
                    if (x != null)
                    {
                        return x;
                    }
                    x = xOfFoundNextMove(colsAsStrings[i], pattern, i);
                    if (x != null)
                    {
                        return x;
                    }
                }
            }
               

            return new Move() { X =  Rows().First().Count / 2,  Y = Rows().Count / 2 } ;
        }

        private Move xOfFoundNextMove(string row, string pattern, int i)
        {
            if (row.Contains(pattern))
            {
                var offset = pattern.IndexOf("-");

                if (row.Contains("R"))
                {
                    return new Move { X = row.IndexOf(pattern) + offset, Y = i };
                }
                else
                {
                    return new Move { X = i, Y = row.IndexOf(pattern) + offset };

                }

            }
            return null;
        }

    }
}
