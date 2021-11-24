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
            string nul = null;

            var listBoard = new List<List<string>>() {
                new List<string>() { nul, "x", "o", "x" },
                new List<string>() { nul, "x", "o", "x" },
                new List<string>() { nul, "x", "o", "x" },
                new List<string>() { nul, "x", "o", "x" },
            };

            var board = new Board(listBoard, "x");

            var diagonals = board.Diagonals();
            var actual = diagonals.Select(d => d.OrderBy(c => c.X).ToList().ToStringExt()).ToList();

            var expected = new List<string> {
                "-",
                "-m",
                "-mp",
                "-mpm",
                "mpm",
                "pm",
                "m",
            };

            Assert.AreEqual(expected.Count * 2, actual.Count);
            foreach (var exp in expected) {
                Assert.AreEqual(actual.Where(a => a.Equals(exp)).Count(), 2);
            }
        }
    }
}