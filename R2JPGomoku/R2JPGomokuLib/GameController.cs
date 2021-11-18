using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace R2JPGomokuLib {
    public class GameController {
        private readonly IGameWriter gameWriter;
        private bool canMakeMove = false;

        public class NewGameRequest {
            public string player_1 { get; set; }
            public string player_2 { get; set; }
        }

        public class GameResponse {
            public string id { get; set; }
            public List<List<string>> board { get; set; }
            public string next_move { get; set; }
            public string winner { get; set; }
            public string player_1 { get; set; }
            public string player_2 { get; set; }
        }

        public class MoveRequest {
            public string player { get; set; }
            public int x { get; set; }
            public int y { get; set; }
        }

        public GameController(IGameWriter gameWriter) {
            this.gameWriter = gameWriter;
        }

        public async Task ViewPrettyGame(string gameId) {
            var response = await Call(HttpMethod.Get, $"view_game/{gameId}/pretty");
            gameWriter.WriteGamePretty(response);
        }

        public async Task NewGame(string gameId, string player1, string player2) {
            var data = new NewGameRequest { player_1 = player1, player_2 = player2 };
            var response = await Call(HttpMethod.Post, $"new_game/{gameId}", data);
            await ViewGame(response: response);
        }

        public async Task ViewGame(string gameId = null, string response = null) {
            if (response != null) {
                gameWriter.WriteGame(response);
            }
            else if (gameId != null) {
                response = await Call(HttpMethod.Get, $"view_game/{gameId}");
                gameWriter.WriteGame(response);
            }
        }

        public async Task EndGame(string gameId) {
            await Call(HttpMethod.Post, $"end_game/{gameId}");
        }

        public async Task MakeMove(string gameId, string player, int x, int y) {
            var data = new MoveRequest() { player = player, x = x, y = y };
            var response = await Call(HttpMethod.Put, $"play_game/{gameId}", data);
            canMakeMove = false;
            gameWriter.WriteGame(response);
            var pretty = await Call(HttpMethod.Get, $"view_game/{gameId}/pretty");
            gameWriter.WriteGamePretty(pretty);
        }

        public async Task PlayGame(string gameId, string player, bool auto) {

            while (true) {
                var response = await Call(HttpMethod.Get, $"view_game/{gameId}");

                if (response.Equals("ERROR!")) {
                    gameWriter.WriteError(response);
                    return;
                }

                await ViewGame(response: response);

                Thread.Sleep(500);

                GameResponse game = null;
                try {
                    game = JsonConvert.DeserializeObject<GameResponse>(response);
                }
                catch (Exception e) {
                    gameWriter.WriteError($"Could not parse: {response}");
                    return;
                }

                if (game.winner != null) {
                    gameWriter.WriteWinner(game.winner);
                    return;
                }

                if (game.next_move.Equals(player)) {
                    canMakeMove = true;
                    if (auto) {
                        var marker = player.Equals(game.player_1) ? "x" : "o";
                        var board = new Board(game.board, marker);
                        var move = board.NextMoveByCells();
                        await MakeMove(gameId, player, move.X, move.Y);
                    }
                }

                Thread.Sleep(500);
            }
        }

        private async Task<string> Call(HttpMethod method, string path, object data = null) {
            HttpRequestMessage request = new HttpRequestMessage(method, $"http://gomoku.fly.dev/{path}");

            string json = JsonConvert.SerializeObject(data);

            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");


            var debug = JsonConvert.SerializeObject(request);
            File.WriteAllText(@"C:\Temp\debug.txt", debug);
            HttpClient http = new HttpClient();
            HttpResponseMessage response;
            try {
                response = await http.SendAsync(request);
            }
            catch (Exception) {

                throw new ApplicationException("fooBAR");
            }


            var result = "";
            if (response.IsSuccessStatusCode) {
                result = response.Content.ReadAsStringAsync().Result;
            }
            else {
                result = "ERROR!";
            }

            http.Dispose();

            return result;
        }
    }
}
