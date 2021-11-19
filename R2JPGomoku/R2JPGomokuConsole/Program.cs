using R2JPGomokuLib;
using System;
using System.Threading.Tasks;

namespace R2JPGomokuConsole {
    class Program {
        static async Task Main(string[] args) {
            switch (args[0]) {
                case "new_game":
                    await GetGameController().NewGame(args[1], args[2], args[3]);
                    break;
                case "play":
                    var option = args.Length > 3 ? args[3] : "";
                    await GetGameController(option).PlayGame(args[1], args[2], true);
                    break;
                case "view":
                    await GetGameController().ViewGame();
                    break;
                case "pretty":
                    await GetGameController().ViewPrettyGame(args[1]);
                    break;
                default:
                    break;
            }
        }

        static private GameController GetGameController(string option = "") {
            var gameWriter = new GameWriterConsole();
            var gameController = new GameController(gameWriter, option);
            return gameController;
        }
    }
}
