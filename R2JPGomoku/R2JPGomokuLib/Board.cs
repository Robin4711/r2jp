using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace R2JPGomokuLib {
    public class Board {
        private readonly List<List<string>> board;

        public class Move {
            public int X { get; set; }
            public int Y { get; set; }
        }

        public Board(List<List<string>> board) {
            this.board = board;
        }

        public List<List<string>> Rows() {
            return board;
        }

        public Move NextMove() {
            return new Move() { X =  Rows().First().Count / 2,  Y = Rows().Count / 2 } ;
        }

    }
}
