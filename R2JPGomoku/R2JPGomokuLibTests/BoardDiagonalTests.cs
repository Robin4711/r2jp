using Microsoft.VisualStudio.TestTools.UnitTesting;
using R2JPGomokuLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace R2JPGomokuLib.Tests {
    [TestClass()]
    public class BoardDiagonalTests {
        [TestMethod()]
        public void DiagonalsTest() {
            var listBoard = new List<List<string>>() {
                new List<string>() { "1A", "2A", "3A", "4A" },
                new List<string>() { "1B", "2B", "3B", "4B" },
                new List<string>() { "1C", "2C", "3C", "4C" },
                new List<string>() { "1D", "2D", "3D", "4D" }
            };

            var board = new Board(listBoard, "x");

            var diagonals = board.Diagonals();
            var actual = diagonals.Select(d => d.OrderBy(c => c.X).ThenBy(c => c.Y).ToList().ToStringExt()).ToList();

            var expected = new List<string> {
                "1D",
                "1C, 2D", 
                "1B, 2C, 3D",
                "1A, 2B, 3C, 4D",
                "2A, 3B, 4C",
                "3A, 4B",
                "4A",
                "1A",
                "1B, 2A",
                "1C, 2B, 3A",
                "1D, 2C, 3B, 4A",
                "2D, 3C, 4B",
                "3D, 4C",
                "4D"
            };

            Assert.AreEqual(expected.Count, actual.Count);
            foreach (var exp in expected) {
                Assert.IsTrue(actual.Contains(exp));
            }
        }
    }
}