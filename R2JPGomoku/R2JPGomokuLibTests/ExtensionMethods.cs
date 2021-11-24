using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static R2JPGomokuLib.Board;

namespace R2JPGomokuLibTests {
    public static class ExtensionMethods {
        public static string ToStringExt(this IList<Move> self) {
            return String.Join(", ", self.Select(m => $"(X:{m.X}, Y:{m.Y})"));
        }
    }
}
