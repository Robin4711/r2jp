using Microsoft.VisualStudio.TestTools.UnitTesting;
using R2JPGomokuLibTests;
using System.Collections.Generic;
using System.Linq;
using static R2JPGomokuLib.Board;

namespace R2JPGomokuLib.Tests
{
    [TestClass()]
    public class BoardTests
    {
        [TestMethod()]
        public void FirstTest_FirstMove_20x20() {
            var listBoard = new List<List<string>>() {
                //-----------------{    1,    2,    3,    4,    5,    6,    7,    8,    9,   10,   11,   12,   13,   14,   15,   16,   17,   18,   19,   20 }, 
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },

                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },

                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },

                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null }
            };

            var board = new Board(listBoard, "x");

            var expected = new Move() { X = 10, Y = 10 };

            PerformTest(board, expected);
        }

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

            var expected = new Move() { X = 2, Y = 2 };

            PerformTest(board, expected);
        }


        [TestMethod()]
        [Ignore]
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

            var expected = new Move() { X = 5, Y = 1 };

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
        public void Offensive_SetThreeWithAGap() {
            var listBoard = new List<List<string>>() {
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, "x", "x", null, null , null },
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null }
            };

            var board = new Board(listBoard, "x");

            var expected = new Move() { X = 5, Y = 2 };

            PerformTest(board, expected);
        }


        [TestMethod()]
        public void Offensive_SetThreeWithAGap_FromTwoWithGap() {
            var listBoard = new List<List<string>>() {
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, "x", null, "x", null , null },
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null }
            };

            var board = new Board(listBoard, "x");

            var expected = new Move() { X = 5, Y = 2 };

            PerformTest(board, expected);
        }

        [TestMethod()]
        public void Offensive_SetTwoWithAGap_FromOne() {
            var listBoard = new List<List<string>>() {
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, "x", null, null, null , null },
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null }
            };

            var board = new Board(listBoard, "x");

            var expected = new Move() { X = 4, Y = 4 };

            PerformTest(board, expected);
        }

        [TestMethod()]
        public void Defensive_DoNotBlockWhenThereIsNoRoom_ThreeInRow () {
            var listBoard = new List<List<string>>() {
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, "x", "x", "x", "o", null , null },
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null }
            };

            var board = new Board(listBoard, "o");

            var notExpected = new Move() { X = 0, Y = 2 };

            PerformNotExpectedTest(board, notExpected);
        }

        [TestMethod()]
        public void Defensive_Block_TwoTwoInARow() {
            var listBoard = new List<List<string>>() {
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, null, "x", null, null, null },
                new List<string>() { null, null, "o", null, "x", null , null },
                new List<string>() { null, "o", null, "o", null, "o", null },
                new List<string>() { null, null, "o", null, "x", null, null },
                new List<string>() { null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null }
            };

            var board = new Board(listBoard, "x", "w");

            var expected = new Move() { X = 2, Y = 3 };

            PerformTest(board, expected);
        }

        [TestMethod()]
        public void Offensive_Set_ThreeInRowWithNoGap() {
            var listBoard = new List<List<string>>() {
                new List<string>() { null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, "o", null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null , null },
                new List<string>() { null, null, null, "o", "o", null, null, null, null },
                new List<string>() { null, null, "o", null, "o", null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null }
            };

            var board = new Board(listBoard, "o", "w");

            var expected = new Move() { X = 4, Y = 3 };

            PerformTest(board, expected);
        }

        [TestMethod()]
        public void Offensive_Set_ThreeWithGapFromTwoWithLargeGap() {
            var listBoard = new List<List<string>>() {
                new List<string>() { null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null , null },
                new List<string>() { null, null, "o", null, null, "o", null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null }
            };

            var board = new Board(listBoard, "o", "w");

            var expected = new Move() { X = 3, Y = 4 };

            PerformTest(board, expected);
        }

        [TestMethod()]
        public void Defensive_Block_ThreeInARowDiagonal() {
            var listBoard = new List<List<string>>() {
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null,  "x", null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null,  "o", null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null,  "o", null, "o" , null, null, null },
                new List<string>() { null, null, null, "x" ,  "x",  "o",  "x",  "x",  "o",  "x",  "x",  "o", null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null,  "o", null, "o" , null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null,  "x", null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null },
                new List<string>() { null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null }
            };

            var board = new Board(listBoard, "x", "w");

            var expectedAny = new List<Move> { new Move() { X = 9, Y = 8 }, new Move { X = 13, Y = 4 } };

            PerformTest(board, expectedAny);
        }

        private void PerformTest(Board board, Move expected, IList<Template> patterns = null)
        {
            var actual = board.NextMoveByCells(patterns);
            Assert.IsTrue(expected.X.Equals(actual.X) && expected.Y.Equals(actual.Y), $"Expected X: {expected.X}, Y: {expected.Y}. Actual X: {actual.X}, Y: {actual.Y}");
        }

        private void PerformTest(Board board, List<Move> expectedAny, IList<Template> patterns = null) {
            var actual = board.NextMoveByCells(patterns);
            var match = expectedAny.Any(m => m.X.Equals(actual.X) && m.Y.Equals(actual.Y));
            Assert.IsTrue(match, $"Expected {expectedAny.ToStringExt()} but was X: {actual.X}, Y: {actual.Y}");
        }

        private void PerformNotExpectedTest(Board board, Move notExpected) {
            var actual = board.NextMoveByCells();
            Assert.IsFalse(notExpected.X.Equals(actual.X) && notExpected.Y.Equals(actual.Y), $"Did not expect X: {notExpected.X}, Y: {notExpected.Y}");
        }

    }
}