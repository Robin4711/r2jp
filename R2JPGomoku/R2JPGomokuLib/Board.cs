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
        private readonly string option;

        public class Move {
            public int X { get; set; }
            public int Y { get; set; }
            public int Value { get; set; }

            public Move() { }
            public Move(int x, int y, int value) {
                X = x;
                Y = y;
                Value = value;
            }
        }

        public class Template {
            public Template(string pattern, int value) {
                Pattern = pattern;
                Value = value;
            }

            public string Pattern { get; }
            public int Value { get; }
        }

        public Board(List<List<string>> board, string playCharacter, string option = "") {
            this.playCharacter = playCharacter;
            this.option = option;

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

        public Move NextMoveByCells(IList<Template> injectedPatterns = null) {
            var rowsAsStrings = RowsAsCellLists().Select(r => r.ToString()).ToList();
            var colsAsStrings = ColumnsAsCellLists().Select(c => c.ToString()).ToList();
            var diagonalsAsStrings = Diagonals().Select(d => d.ToString()).ToList();

            var sequences = Diagonals().Concat(ColumnsAsCellLists()).Concat(RowsAsCellLists());

            var patterns = injectedPatterns ?? new List<Template>() {
                new Template("-mmmm", 10000),
                new Template("m-mmm", 10000),
                new Template("mm-mm", 10000),
                new Template("mmm-m", 10000),
                new Template("mmmm-", 10000),
                
                new Template("-pppp", 5000),
                new Template("p-ppp", 5000),
                new Template("pp-pp", 5000),
                new Template("ppp-p", 5000),
                new Template("pppp-", 5000),

                new Template("=-mmm=", 2500),
                new Template("=mmm-=", 2500),
                new Template("=mm-m=", 2500),
                new Template("=m-mm=", 2500),
                
                new Template("=-=mmm", 1000),
                new Template("mmm=-=", 1000),

                new Template("=-ppp=", 1000),
                new Template("=ppp-=", 1000),
                new Template("=pp-p=", 1000),
                new Template("=p-pp=", 1000),


                new Template("mm=m-", 500),
                new Template("-m=mm", 500),
                
                new Template("m-=mm", 500),
                new Template("mm=-m", 500),
                
                new Template("p=p-p=p", 250),
                
                new Template("pp=p-", 100),
                new Template("-p=pp", 100),
                
                new Template("p-=pp", 100),
                new Template("pp=-p", 100),
                
                new Template("p=p-p=p", 100),
                
                new Template("-=m=m=m", 100),
                new Template("m=m=m=-", 100),
                
                new Template("m-m=m", 100),
                new Template("m=m-m", 100),
                
                new Template("=-=mm=", 100),
                new Template("=mm=-=", 100),
                new Template("=-=mm", 100),
                new Template("mm=-=", 100),
                new Template("=-mm=", 100),
                new Template("=mm-=", 100),
                
                new Template("-=m=m=m", 100),
                new Template("m=m=m=-", 100),
                
                new Template("m-m=m", 100),
                new Template("m=m-m", 100),
                
                new Template("==-pp=", 50),
                new Template("=pp-==", 50),
                new Template("=-=pp", 50),
                new Template("pp=-=", 50),
                new Template("=-pp=", 50),
                new Template("=pp-=", 50),
                new Template("==-p=", 50),
                new Template("=-p==", 50),
                
                new Template("=m=m=-=", 25),
                new Template("=-=m=m=", 25),
                
                new Template("=m=m-=", 25),
                new Template("=-m=m=", 25),
                
                new Template("=-=m=", 25),
                new Template("=m=-=", 25),
                new Template("==-m=", 25),
                new Template("=-m==", 25),
                
                new Template("mm-", 25),
                new Template("-mm", 25),
                new Template("m-m", 25),

                new Template("-pp", 25),
                new Template("pp-", 25),
                new Template("p-p", 25),
                
                new Template("-m", 10),
                new Template("m-", 10),
                
                new Template("-p", 10),
                new Template("p-", 10),

            };

            var possibleMoves = new List<Move>();

            foreach (var pattern in patterns) {
                var searchPattern = pattern.Pattern.Replace('=', '-');
                var rs = sequences;
                var debug = rs.Select(r => r.ToStringExt());
                var matches = sequences.Where(r => r.ToStringExt().Contains(searchPattern)).ToList();
                if (matches.Count() > 0) {
                    var r = matches.First();
                    var s = r.ToStringExt();
                    var start = s.IndexOf(searchPattern);
                    var offset = pattern.Pattern.IndexOf("-");
                    var c = r[start + offset];

                    possibleMoves.Add(new Move(c.X, c.Y, pattern.Value));
                }
            }

            var centerX = ColumnsAsCellLists().Count / 2;
            var centerY = RowsAsCellLists().Count / 2;
            var centerCell = cells.Single(c => c.X.Equals(centerX) && c.Y.Equals(centerY));

            if (centerCell.Value.Equals("-")) {
                possibleMoves.Add(new Move(centerX, centerY, 1));
            }

            if (option.Contains("w")) {
                var move = possibleMoves
                    .GroupBy(m => new { X = m.X, Y = m.Y })
                    .Select(g => new { X = g.Key.X, Y = g.Key.Y, Value = g.Sum(o => o.Value) })
                    .OrderByDescending(m => m.Value)
                    .First();
                return new Move(move.X, move.Y, 1000);
            }

            return possibleMoves.OrderByDescending(m => m.Value).First();

        }



    }
}
