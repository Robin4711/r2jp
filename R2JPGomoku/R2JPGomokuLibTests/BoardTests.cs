using Microsoft.VisualStudio.TestTools.UnitTesting;
using R2JPGomokuLib;
using System;
using System.Collections.Generic;
using System.Text;
using static R2JPGomokuLib.Board;

namespace R2JPGomokuLib.Tests {
    [TestClass()]
    public class BoardTests {
        [TestMethod()]
        public void FirstTest_FirstMove() {
            var listBoard = new List<List<string>>() {
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null }
            };

            var board = new Board(listBoard);

            var expected = new Move() { X = 3, Y = 3 };

            PerformTest(board, expected);
        }

        [TestMethod()]
        public void CanWeWin_FourInRow_Left()
        {
            var listBoard = new List<List<string>>() {
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { "x", "x", "x", "x", null, null },
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null }
            };

            var board = new Board(listBoard);

            var expected = new Move() { X = 4, Y = 2 };

            PerformTest(board, expected);
        }

        [TestMethod()]
        public void CanWeWin_FourInRow_Right()
        {
            var listBoard = new List<List<string>>() {
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, "x", "x", "x", "x" },
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null }
            };

            var board = new Board(listBoard);

            var expected = new Move() { X = 1, Y = 2 };

            PerformTest(board, expected);
        }

        private void PerformTest(Board board, Move expected) {
            var actual = board.NextMove();
            Assert.AreEqual(expected.X, actual.X, $"Expected: X {expected.X} Actual: {actual.X}");
            Assert.AreEqual(expected.Y, actual.Y, $"Expected: Y {expected.Y} Actual: {actual.Y}");
        }
    }
}