using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using static R2JPGomokuLib.Board;

namespace R2JPGomokuLib.Tests
{
    [TestClass()]
    public class BoardTests
    {
        [TestMethod()]
        public void FirstTest_FirstMove()
        {
            var listBoard = new List<List<string>>() {
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null }
            };

            var board = new Board(listBoard, "x");

            var expected = new Move() { X = 3, Y = 3 };

            PerformTest(board, expected);
        }

        [TestMethod()]
        public void Test_EmptySpot()
        {
            var listBoard = new List<List<string>>() {
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, "o", null, null },
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null }
            };

            var board = new Board(listBoard, "x");

            var expected = new Move() { X = 4, Y = 2 };

            PerformTest(board, expected);
        }


        [TestMethod()]
        public void Test_AddNextToOurPrevious()
        {
            var listBoard = new List<List<string>>() {
                new List<string>() {null, null, null, null, null, null, null, null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
                new List<string>() {null, null, null, null, null, "x", "o", null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
            };

            var board = new Board(listBoard, "x");

            var expected = new Move() { X = 4, Y = 2 };

            PerformTest(board, expected);
        }
        [TestMethod()]
        public void CanWeWin_FourInRowColumn()
        {
            var listBoard = new List<List<string>>() {
                new List<string>() {null, null, null, null, null, null, null, null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
                new List<string>() {null, null, null, null, null, "x", "o", null, null },
                new List<string>() {null, null, null, null, null, "x", null, null, null },
                new List<string>() {null, null, null, null, null, "x", null, null, null },
                new List<string>() {null, null, null, null, null, "x", null, null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
            };

            var board = new Board(listBoard, "x");

            var expected = new Move() { X = 5, Y = 6 };

            PerformTest(board, expected);
        }

        [TestMethod()]
        public void CanWeWin_ThreeWithCapInRowColumn()
        {
            var listBoard = new List<List<string>>() {
                new List<string>() {null, null, null, null, null, null, null, null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
                new List<string>() {null, null, null, null, null, "x", "o", null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
                new List<string>() {null, null, null, null, null, "x", null, null, null },
                new List<string>() {null, null, null, null, null, "x", null, null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
            };

            var board = new Board(listBoard, "x");

            var expected = new Move() { X = 5, Y = 3 };

            PerformTest(board, expected);
        }

        [TestMethod()]
        public void CanTheyWin_FourInRowColumn()
        {
            var listBoard = new List<List<string>>() {
                new List<string>() {null, null, null, null, null, null, null, null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
                new List<string>() {null, null, null, null, null, "o", "x", null, null },
                new List<string>() {null, null, null, null, null, "o", null, null, null },
                new List<string>() {null, null, null, null, null, "o", null, null, null },
                new List<string>() {null, null, null, null, null, "o", null, null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
            };

            var board = new Board(listBoard, "x");

            var expected = new Move() { X = 5, Y = 1 };

            PerformTest(board, expected);
        }

        [TestMethod()]
        public void CanTheyWin_FourDiagonalInRowColumn()
        {
            var listBoard = new List<List<string>>() {
                new List<string>() {null, null, null, "o", null, null, null, null, null },
                new List<string>() {null, null, null, null, "o", null, null, null, null },
                new List<string>() {null, null, null, null, null, "o", "x", null, null },
                new List<string>() {null, null, null, null, null, null, "o", null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
                new List<string>() {null, null, null, null, null, "o", null, null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
                new List<string>() {null, null, null, null, null, null, null, null, null },
            };

            var board = new Board(listBoard, "x");

            var expected = new Move() { X = 7, Y = 4 };

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

            var board = new Board(listBoard, "x");

            var expected = new Move() { X = 4, Y = 2 };

            PerformTest(board, expected);
        }

        [TestMethod()]
        public void CanTheyWin_FourInRow_Left()
        {
            var listBoard = new List<List<string>>() {
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { "o", "o", "o", "o", null, null },
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null }
            };

            var board = new Board(listBoard, "x");

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

            var board = new Board(listBoard, "x");

            var expected = new Move() { X = 1, Y = 2 };

            PerformTest(board, expected);
        }


        [TestMethod()]
        public void CanTneyWin_FourInRow_Right()
        {
            var listBoard = new List<List<string>>() {
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, "o", "o", "o", "o" },
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null }
            };

            var board = new Board(listBoard, "x");

            var expected = new Move() { X = 1, Y = 2 };

            PerformTest(board, expected);
        }

        [TestMethod()]
        public void Offensive_SetFourWithAGap() {
            var listBoard = new List<List<string>>() {
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, "o", "o", null, null , null },
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null }
            };

            var board = new Board(listBoard, "x");

            var expected = new Move() { X = 5, Y = 2 };

            PerformTest(board, expected);
        }

        private void PerformTest(Board board, Move expected)
        {
            var actual = board.NextMoveByCells();
            Assert.AreEqual(expected.X, actual.X, "X");
            Assert.AreEqual(expected.Y, actual.Y, "Y");
        }
    }
}