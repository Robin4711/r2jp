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

        private async void button1_Click(object sender, EventArgs e) {
            var command = comboBox1.Text.Split(" ");
            var gameWriter = new GameWriterWin(new Board(panel1));
            var gameController = new GameController(gameWriter);
            switch (command[0]) {
                case "new_game":
                    var response = await gameController.NewGame(command[1], command[2], command[3]);
                    break;
                case "end_game":
                    gameController.EndGame(command[1]);
                    break;
                case "manual":
                    break;
                case "robot":
                    gameController.PlayGame(command[1], command[2], command[3]);
                    break;
                default:
                    break;
            }
        }



    }
}
