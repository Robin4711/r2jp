using R2JPGomokuLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace R2JPGomokuWin {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            var command = comboBox1.Text.Split(" ");
            switch (command[0]) {
                case "new_game":
                    var response = new GameController().NewGame(command[0], command[1], command[2]);
                    break;
                case "end_game":
                    new GameController().EndGame(command[0]);
                    break;
                case "manual":
                    break;
                case "robot":
                    new GameController().PlayGame(command[0], command[1], command[2]);
                    break;
                default:
                    break;
            }
        }
    }
}
