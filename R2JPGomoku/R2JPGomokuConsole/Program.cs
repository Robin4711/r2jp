using R2JPGomokuLib;
using System;
using System.Threading.Tasks;

namespace R2JPGomokuConsole {
    class Program {
        static async Task Main(string[] args) {
            switch (args[0]) {
                case "new_game":
                    await new GameController().NewGame(args[1], args[2], args[3]);
                    break;
                case "play":
                    new GameController().PlayGame(args[1], args[2]);
                    break;
                default:
                    break;
            }
        }
    }
}
