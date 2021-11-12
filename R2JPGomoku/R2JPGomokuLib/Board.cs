﻿using System;
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
            
            foreach(var row in board)
            {
                for (int i = 0; i < row.Count; i++)
                {
                    if (row[i] == null)
                        row[i] = "-";
                }
            }
            this.board = board;
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
                x = xOfFoundNextMove(rowsAsStrings[i], "xxxx-");
                if (x != -1)
                {
                    y = i;
                    return new Move { X = x, Y = y };
                }
                x = xOfFoundNextMove(rowsAsStrings[i], "-xxxx");
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
