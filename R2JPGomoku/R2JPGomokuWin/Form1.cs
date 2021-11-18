using Newtonsoft.Json;
using R2JPGomokuLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static R2JPGomokuLib.GameController;

namespace R2JPGomokuWin {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private GameController GetGameController(string gameId, string player, Panel panel) {
            var board = new Board(panel, null, gameId, player);
            var gameWriter = new GameWriterWin(board, textBox1);
            board.GameWriter = gameWriter;
            var gameController = new GameController(gameWriter);
            return gameController;
        }

        private async void button1_Click(object sender, EventArgs e) {
            var command = comboBox1.Text.Split(" ");
            string gameId ;
            string player1;
            string player2;
            string player;
            string marker; 
            switch (command[0]) {
                case "new_game":
                    gameId = command[1];
                    player1 = command[2];
                    player2 = command[3];
                    var controller = GetGameController(gameId, player1, panel1);
                    var response1 = controller.EndGame(gameId);
                    var response2 = controller.NewGame(gameId, player1, player2);
                    break;
                case "end_game":
                    gameId = command[1];
                    await GetGameController(gameId, "foobar", panel1).EndGame(gameId);
                    break;
                case "manual":
                    gameId = command[1];
                    player = command[2];
                    await GetGameController(gameId, player, panel1).PlayGame(gameId, player, false);
                    break;
                case "robot":
                    gameId = command[1];
                    player = command[2];
                    await GetGameController(gameId, player, panel1).PlayGame(gameId, player, true);
                    break;
                default:
                    break;
            }
        }



    }
}
