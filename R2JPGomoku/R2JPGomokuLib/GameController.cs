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

        public void ViewGame(string gameId) {
            var response = Call(HttpMethod.Get, $"view_game/{gameId}").Result;
            gameWriter.WriteGame(response);
        }

        public void ViewPrettyGame(string gameId) {
            var response = Call(HttpMethod.Get, $"view_game/{gameId}/pretty").Result;
            gameWriter.WriteGamePretty(response);
        }


        public async Task<string> NewGame(string gameId, string player1, string player2) {
            var data = new NewGameRequest { player_1 = player1, player_2 = player2 };
            string response;
            try {
                response = await Call(HttpMethod.Post, $"new_game/{gameId}", data);
                gameWriter.WriteGame(response);
            }
            catch (Exception) {
                throw;
            }
            return response;
        }

        public async Task EndGame(string gameId) {
            await Call(HttpMethod.Post, $"end_game/{gameId}");
        }

        public async Task PlayGame(string gameId, string myPlayer, string marker) {
            while (true) {
                var response = Call(HttpMethod.Get, $"view_game/{gameId}").Result;

                if (response.Equals("ERROR!")) {
                    gameWriter.WriteError(response);
                    return;
                }

                var game = JsonConvert.DeserializeObject<GameResponse>(response);

                if (game.winner != null) {
                    gameWriter.WriteWinner(game.winner);
                    return;
                }

                if (game.next_move.Equals(myPlayer)) {
                    var board = new Board(game.board, marker);
                    var move = board.NextMoveByCells();
                    var data = new MoveRequest() { player = myPlayer, x = move.X, y = move.Y };
                    //string json = JsonConvert.SerializeObject(data);
                    var res = Call(HttpMethod.Put, $"play_game/{gameId}", data).Result;
                    gameWriter.WriteGame(res);
                    var pretty = Call(HttpMethod.Get, $"view_game/{gameId}/pretty").Result;
                    gameWriter.WriteGamePretty(pretty);
                }

                Thread.Sleep(1000);
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
