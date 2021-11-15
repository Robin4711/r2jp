using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace R2JPGomokuLib {
    public class Cell {
        public int X { get; set; }
        public int Y { get; set; }
        public string Value { get; set; }

        public Cell(int x, int y, string value) {
            X = x;
            Y = y;
            Value = value;
        }
    }

    public class Board {
        private readonly List<List<string>> board;
        private readonly IList<Cell> cells = new List<Cell>();
        private readonly string playCharacter;

        public class Move {
            public int X { get; set; }
            public int Y { get; set; }
            public Move() { }
            public Move(int x, int y) {
                X = x;
                Y = y;
            }
        }

        public Board(List<List<string>> board, string playCharacter) {
            this.playCharacter = playCharacter;

            for (int y = 0; y < board.Count; y++) {
                var row = board[y];
                for (int x = 0; x < row.Count; x++) {
                    if (row[x] == null) {
                        row[x] = "-";
                        cells.Add(new Cell(x, y, "-"));
                    }
                    else if (row[x] == playCharacter) {
                        row[x] = "m";
                        cells.Add(new Cell(x, y, "m"));
                    }
                    else {
                        row[x] = "p";
                        cells.Add(new Cell(x, y, "p"));
                    }
                }
            }

            this.board = board;
        }

        public List<List<string>> Rows() {
            return board;
        }

        public List<List<Cell>> RowsAsCellLists() {
            return cells.GroupBy(c => c.Y).Select(g => g.ToList()).ToList();
        }

        public List<List<Cell>> ColumnsAsCellLists() {
            return cells.GroupBy(c => c.X).Select(g => g.ToList()).ToList();
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

        public List<List<Cell>> Diagonals() {
            var diagonals = new List<List<string>>();
            var diagonalsAsCellLists = new List<List<Cell>>();
            var length = cells.Count;

            var b = RowsAsCellLists();

            diagonals.Add(GetDiagonal(board, 0, 0, 1, 1));
            diagonals.Add(GetDiagonal(board, length - 1, 0, -1, 1));

            diagonalsAsCellLists.Add(GetDiagonalAsCellList(b, 0, 0, 1, 1));
            diagonalsAsCellLists.Add(GetDiagonalAsCellList(b, length - 1, 0, -1, 1));

            for (int i = 1; i < length; i++) {
                diagonals.Add(GetDiagonal(board, i, 0, 1, 1));
                diagonals.Add(GetDiagonal(board, 0, i, 1, 1));
                diagonals.Add(GetDiagonal(board, length - 1 - i, 0, -1, 1));
                diagonals.Add(GetDiagonal(board, length - 1, i, -1, 1));
                diagonalsAsCellLists.Add(GetDiagonalAsCellList(b, i, 0, 1, 1));
                diagonalsAsCellLists.Add(GetDiagonalAsCellList(b, 0, i, 1, 1));
                diagonalsAsCellLists.Add(GetDiagonalAsCellList(b, length - 1 - i, 0, -1, 1));
                diagonalsAsCellLists.Add(GetDiagonalAsCellList(b, length - 1, i, -1, 1));
            }
            return diagonalsAsCellLists;
        }

        private List<Cell> GetDiagonalAsCellList(List<List<Cell>> board, int x, int y, int stepX, int stepY) {
            var result = new List<Cell>();
            var i = x;
            var j = y;
            var min = 0;
            var max = board.Count - 1;

            while (min <= i && min <= j && i <= max && j <= max) {
                result.Add(board[j][i]);
                i += stepX;
                j += stepY;
            }
            return result;
        }

        private List<string> GetDiagonal(List<List<string>> board, int x, int y, int stepX, int stepY) {
            var result = new List<string>();
            var i = x;
            var j = y;
            var min = 0;
            var max = board.Count - 1;

            while (min <= i && min <= j && i <= max && j <= max) {
                result.Add(board[j][i]);
                i = i + stepX;
                j = j + stepY;
            }
            return result;
        }

        public Move NextMove() {
            var rowsAsStrings = Rows().Select(r => String.Join(string.Empty, r) + "R").ToList();
            var colsAsStrings = Columns().Select(r => String.Join(string.Empty, r) + "C").ToList();
            var diagonalsAsStrings = Diagonals().Select(r => String.Join(string.Empty, r) + "D").ToList();
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
                for (int i = 0; i < rowsAsStrings.Count(); i++) {
                    x = xOfFoundNextMove(rowsAsStrings[i], pattern, i);
                    if (x != null) {
                        return x;
                    }
                    x = xOfFoundNextMove(colsAsStrings[i], pattern, i);
                    if (x != null) {
                        return x;
                    }
                }
            }


            return new Move() { X = Rows().First().Count / 2, Y = Rows().Count / 2 };
        }

        public Move NextMoveByCells() {
            var rowsAsStrings = RowsAsCellLists().Select(r => r.ToString()).ToList();
            var colsAsStrings = ColumnsAsCellLists().Select(c => c.ToString()).ToList();
            var diagonalsAsStrings = Diagonals().Select(d => d.ToString()).ToList();
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
            Cell c = null;
            foreach (var pattern in patterns) {
                var matches = RowsAsCellLists().Where(r => r.ToString().Contains(pattern));
                if (matches.Count() > 0) {
                    c = matches.First().Single(c => c.Equals("-"));
                    return new Move(c.X, c.Y);
                }
            }

            return new Move() { X = Rows().First().Count / 2, Y = Rows().Count / 2 };
        }

        private Move xOfFoundNextMove(string row, string pattern, int i) {
            if (row.Contains(pattern)) {
                var offset = pattern.IndexOf("-");

                if (row.Contains("R")) {
                    return new Move { X = row.IndexOf(pattern) + offset, Y = i };
                }
                else {
                    return new Move { X = i, Y = row.IndexOf(pattern) + offset };

                }

            }
            return null;
        }

    }
}
