using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace R2JPGomokuLib {
    public class GameController {
        public class NewGameRequest {
            public string player_1 { get; set; }
            public string player_2 { get; set; }
        }


        public async Task NewGame(string gameId, string player1, string player2) {
            var data = new NewGameRequest { player_1 = player1, player_2 = player2 };

            await Post(HttpMethod.Post, $"new_game/{gameId}", data);

        }

        public void PlayGame(string gameId) {
            while (true) {
                var response = Post(HttpMethod.Get, $"view_game/{gameId}");

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
