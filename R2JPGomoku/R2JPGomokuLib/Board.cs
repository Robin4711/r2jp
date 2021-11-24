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
            public string Pattern { get; }
            public SequenceType SequenceType { get; }

            public Move() { }
            public Move(int x, int y, int value, string pattern, SequenceType sequenceType) {
                X = x;
                Y = y;
                Value = value;
                Pattern = pattern;
                SequenceType = sequenceType;
            }

            public override string ToString() {
                return $"({X}, {Y}) {Value} {Pattern}";
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

        public class Sequence<T> : List<T> {
            public Sequence(SequenceType type) : base() {
                Type = type;
            }

            public SequenceType Type { get; set; }
        }

        public enum SequenceType {
            Row,
            Column,
            DiagonalLeft,
            DiagonalRight
        }

        public List<Sequence<Cell>> RowsAsCellLists() {
            return cells.GroupBy(c => c.Y).Select(g => g.ToList(SequenceType.Row)).ToList();
        }

        public List<Sequence<Cell>> ColumnsAsCellLists() {
            return cells.GroupBy(c => c.X).Select(g => g.ToList(SequenceType.Column)).ToList();
        }


        public List<Sequence<Cell>> Diagonals() {
            var diagonalsAsCellLists = new List<Sequence<Cell>>();
            var length = Convert.ToInt32( Math.Sqrt(cells.Count));

            var b = RowsAsCellLists();

            diagonalsAsCellLists.Add(GetDiagonalAsCellList(b, 0, 0, 1, 1, SequenceType.DiagonalLeft));
            diagonalsAsCellLists.Add(GetDiagonalAsCellList(b, length - 1, 0, -1, 1, SequenceType.DiagonalRight));

            for (int i = 1; i < length; i++) {
                diagonalsAsCellLists.Add(GetDiagonalAsCellList(b, i, 0, 1, 1, SequenceType.DiagonalLeft));
                diagonalsAsCellLists.Add(GetDiagonalAsCellList(b, 0, i, 1, 1, SequenceType.DiagonalLeft));
                diagonalsAsCellLists.Add(GetDiagonalAsCellList(b, length - 1 - i, 0, -1, 1, SequenceType.DiagonalRight));
                diagonalsAsCellLists.Add(GetDiagonalAsCellList(b, length - 1, i, -1, 1, SequenceType.DiagonalRight));
            }
            return diagonalsAsCellLists;
        }

        private Sequence<Cell> GetDiagonalAsCellList(List<Sequence<Cell>> board, int x, int y, int stepX, int stepY, SequenceType type) {
            var result = new Sequence<Cell>(type);
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

            var sequences = Diagonals().Concat(ColumnsAsCellLists()).Concat(RowsAsCellLists());
            var debug = String.Join("\r\n", sequences.Select(s => s.ToStringExt()));

            var patterns = injectedPatterns ?? new List<Template>() {
            
                // Level 5 - Offense
                new Template("-mmmm", 100000),
                new Template("m-mmm", 100000),
                new Template("mm-mm", 100000),
                new Template("mmm-m", 100000),
                new Template("mmmm-", 100000),
                
                // Level 5 Defense
                new Template("-pppp", 5000),
                new Template("p-ppp", 5000),
                new Template("pp-pp", 5000),
                new Template("ppp-p", 5000),
                new Template("pppp-", 5000),

                // Level 4 - Offense
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
                new Template("=m-=m=", 100),
                new Template("=m=-m=", 100),

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
                new Template("=p-p=", 50),

                new Template("=m=m=-=", 25),
                new Template("=-=m=m=", 25),
                
                new Template("=m=m-=", 25),
                new Template("=-m=m=", 25),
                
                new Template("=-=m=", 25),
                new Template("=m=-=", 25),
                new Template("==-m=", 25),
                new Template("=-m==", 25),

                new Template("==-p=", 25),
                new Template("=-p==", 25),

                new Template("mm-", 10),
                new Template("-mm", 10),
                new Template("m-m", 10),
                new Template("=m-=m", 10),
                new Template("=m=-m", 10),
                new Template("m-=m=", 10),
                new Template("m=-m=", 10),

                new Template("-pp", 10),
                new Template("pp-", 10),
                new Template("p-p", 10),
                
                new Template("-m", 5),
                new Template("m-", 5),
                
                new Template("-p", 5),
                new Template("p-", 5),

            };

            var possibleMoves = new List<Move>();

            foreach (var pattern in patterns) {
                var searchPattern = pattern.Pattern.Replace('=', '-');
                var matches = sequences.Where(s => s.ToStringExt().Contains(searchPattern)).ToList();
                foreach (var m in matches) {
                    var s = m.ToStringExt();
                    var start = s.IndexOf(searchPattern);
                    var offset = pattern.Pattern.IndexOf("-");
                    var c = m[start + offset];

                    possibleMoves.Add(new Move(c.X, c.Y, pattern.Value, pattern.Pattern, m.Type));
                }
            }

            var centerX = ColumnsAsCellLists().Count / 2;
            var centerY = RowsAsCellLists().Count / 2;
            var centerCell = cells.Single(c => c.X.Equals(centerX) && c.Y.Equals(centerY));

            if (centerCell.Value.Equals("-")) {
                possibleMoves.Add(new Move(centerX, centerY, 1, "-", SequenceType.Row));
            }

            if (option.Contains("w")) {
                var boo = possibleMoves
                    .GroupBy(m => new { x = m.X, y = m.Y }, (m, ms) => new {
                        key = m,
                        count = ms.Count(),
                        moves = ms,
                        value = ms.GroupBy(m => m.SequenceType, (m, ms) => ms.OrderByDescending(m => m.Value).First()).Sum(m => m.Value)
                    });

                var moves = boo.OrderByDescending(f => f.value);
                var move = moves.First();

                return new Move(move.key.x, move.key.y, 1000, "N/A", SequenceType.Row);
            }

            return possibleMoves.OrderByDescending(m => m.Value).First();

        }



    }
}
