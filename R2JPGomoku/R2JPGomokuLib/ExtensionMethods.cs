using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static R2JPGomokuLib.Board;

namespace R2JPGomokuLib {
    public static class ExtensionMethods {
        public static string ToStringExt(this IList<Cell> self) {
            return String.Join(String.Empty, self.Select(c => c.Value));
        }

        //public static Sequence<T> ToList<T>(this IEnumerable<T> self) {
        //    var result = new Sequence<T>();
        //    foreach (var item in self) {
        //        result.Add(item);
        //    }
        //    return result;
        //}

        public static Sequence<T> ToList<T>(this IEnumerable<T> self, SequenceType type) {
            var result = new Sequence<T>(type);
            foreach (var item in self) {
                result.Add(item);
            }
            return result;
        }
    }
}
