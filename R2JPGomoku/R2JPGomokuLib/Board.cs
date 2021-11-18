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


        public List<List<Cell>> RowsAsCellLists() {
            return cells.GroupBy(c => c.Y).Select(g => g.ToList()).ToList();
        }

        public List<List<Cell>> ColumnsAsCellLists() {
            return cells.GroupBy(c => c.X).Select(g => g.ToList()).ToList();
        }


        public List<List<Cell>> Diagonals() {
            var diagonals = new List<List<string>>();
            var diagonalsAsCellLists = new List<List<Cell>>();
            var length = cells.Count;

            var b = RowsAsCellLists();

            diagonalsAsCellLists.Add(GetDiagonalAsCellList(b, 0, 0, 1, 1));
            diagonalsAsCellLists.Add(GetDiagonalAsCellList(b, length - 1, 0, -1, 1));

            for (int i = 1; i < length; i++) {
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

        public Move NextMoveByCells(IList<string> injectedPatterns = null) {
            var rowsAsStrings = RowsAsCellLists().Select(r => r.ToString()).ToList();
            var colsAsStrings = ColumnsAsCellLists().Select(c => c.ToString()).ToList();
            var diagonalsAsStrings = Diagonals().Select(d => d.ToString()).ToList();

            var sequences = Diagonals().Concat(ColumnsAsCellLists()).Concat(RowsAsCellLists());

            var patterns = injectedPatterns ?? new List<string>() {
                "-mmmm",
                "m-mmm",
                "mm-mm",
                "mmm-m",
                "mmmm-",

                "-pppp",
                "p-ppp",
                "pp-pp",
                "ppp-p",
                "pppp-",

                "=-=mmm",
                "mmm=-=",
                "=-mmm=",
                "=mmm-=",

                "=-ppp=",
                "=ppp-=",

                "=mm-m=",
                "=m-mm=",

                "mm=m-",
                "-m=mm",


                "m-=mm",
                "mm=-m",

                "p=p-p=p",

                "=pp-p=",
                "=p-pp=",

                "pp=p-",
                "-p=pp",


                "p-=pp",
                "pp=-p",

                "p=p-p=p",

                "-=m=m=m",
                "m=m=m=-",

                "m-m=m",
                "m=m-m",

                "=-=mm=",
                "=mm=-=",
                "=-=mm",
                "mm=-=",
                "=-mm=",
                "=mm-=",


                "-=m=m=m",
                "m=m=m=-",

                "m-m=m",
                "m=m-m",

                "==-pp=",
                "=pp-==",
                "=-=pp",
                "pp=-=",
                "=-pp=",
                "=pp-=",
                "==-p=",
                "=-p==",

                "=m=m=-=",
                "=-=m=m=",

                "=m=m-=",
                "=-m=m=",

                "=-=m=",
                "=m=-=",
                "==-m=",
                "=-m==",
                
                "-mm",
                "mm-",
                "m-m",

                "-pp",
                "pp-",
                "p-p",

                "-m",
                "m-",

                "-p",
                "p-",

            };

            foreach (var pattern in patterns) {
                var searchPattern = pattern.Replace('=', '-');
                var rs = sequences;
                var debug = rs.Select(r => r.ToStringExt());
                var matches = sequences.Where(r => r.ToStringExt().Contains(searchPattern)).ToList();
                if (matches.Count() > 0) {
                    var r = matches.First();
                    var s = r.ToStringExt();
                    var start = s.IndexOf(searchPattern);
                    var offset = pattern.IndexOf("-");
                    var c = r[start + offset];

                    return new Move(c.X, c.Y);
                }
            }

            return new Move() { X = ColumnsAsCellLists().Count / 2, Y = RowsAsCellLists().Count / 2 };
        }



    }
}
