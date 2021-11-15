using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace R2JPGomokuLib {
    public static class ExtensionMethods {
        public static string ToString(this IList<Cell> self) {
            return String.Join(String.Empty, self.Select(c => c.Value));
        }
    }
}
