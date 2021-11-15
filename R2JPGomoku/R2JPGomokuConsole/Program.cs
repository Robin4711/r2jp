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
                    await new GameController().PlayGame(args[1], args[2], args[3]);
                    break;
                case "view":
                    new GameController().ViewGame(args[1]);
                    break;
                case "pretty":
                    new GameController().ViewPrettyGame(args[1]);
                    break;
                default:
                    break;
            }
        }
    }
}
