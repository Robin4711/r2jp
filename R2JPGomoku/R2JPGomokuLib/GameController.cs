﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace R2JPGomokuLib {
    public class GameController {
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


        public async Task NewGame(string gameId, string player1, string player2) {
            var data = new NewGameRequest { player_1 = player1, player_2 = player2 };

            await Post(HttpMethod.Post, $"new_game/{gameId}", data);

        }

        public void PlayGame(string gameId, string myPlayer) {
            while (true) {
                var response = Post(HttpMethod.Get, $"view_game/{gameId}").Result;

                if (response.Equals("ERROR!")) {
                    return;
                }

                var game = JsonConvert.DeserializeObject<GameResponse>(response);

                if (game.winner != null) {
                    Console.WriteLine($"Winner: {game.winner}");
                    return;
                }

                if (game.next_move.Equals(myPlayer)) {
                    Console.WriteLine("Make move");
                }

                Thread.Sleep(1000);
            }
        }

        private async Task<string> Post(HttpMethod method, string path, object data = null) {
            HttpRequestMessage request = new HttpRequestMessage(method, $"http://gomoku.fly.dev/{path}");

            string json = JsonConvert.SerializeObject(data);

            request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            HttpClient http = new HttpClient();
            HttpResponseMessage response = await http.SendAsync(request);

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
