using R2JPGomokuLib;
using System;
using System.Threading.Tasks;

namespace R2JPGomokuConsole {
    class Program {
        static async Task Main(string[] args) {
            var gameWriter = new GameWriterConsole();
            var gameController = new GameController(gameWriter);
            switch (args[0]) {
                case "new_game":
                    await gameController.NewGame(args[1], args[2], args[3]);
                    break;
                case "play":
                    await gameController.PlayGame(args[1], args[2], true);
                    break;
                case "view":
                    await gameController.ViewGame();
                    break;
                case "pretty":
                    await gameController.ViewPrettyGame(args[1]);
                    break;
                default:
                    break;
            }
        }
    }
}
