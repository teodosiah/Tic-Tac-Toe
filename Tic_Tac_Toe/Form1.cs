using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {
        Algorithm game = new Algorithm();

        public Player player = new Player();
        public Player AI_player = new Player();    

        public List<Button> btnList = new List<Button>();

        string STATE = "";
        
        public Form1()
        {
            InitializeComponent();
            
            btnList = Enumerable.Range(1, 9).Select(i => (Button)this.Controls["button" + i.ToString()]).ToList();
            foreach(var btn in btnList)
            {
                btn.Enabled = false;
            }
        }
        public void setPlayer(char choosen_player)
        {
            player.Symbol = choosen_player;
            if(player.Symbol == 'O')
            {
                player.Color_Value = Color.Blue;
                
                AI_player.Symbol = 'X';
                AI_player.Color_Value = Color.Red;
            }
            else
            {
                player.Color_Value = Color.Red;

                AI_player.Symbol = 'O';
                AI_player.Color_Value = Color.Blue;
            }
            label3.Text = AI_player.Symbol.ToString();
            label3.ForeColor = AI_player.Color_Value;

            label4.Text = player.Symbol.ToString();
            label4.ForeColor= player.Color_Value;
        }
       
        private void button_Click(object sender, EventArgs e)
        {            
            int index;
            if ((sender as Button).Text == "" )
            {
                (sender as Button).ForeColor = player.Color_Value;
                (sender as Button).Text = player.Symbol.ToString();
                index = Int32.Parse((sender as Button).Name[6].ToString()) - 1;

                game.MakeMove(index, game.depth, double.NegativeInfinity, double.PositiveInfinity);
               
                btnList[game.Choice].Text = AI_player.Symbol.ToString();
                btnList[game.Choice].ForeColor = AI_player.Color_Value;

            }

            STATE = (game.showGameSate(game.Board, game.turn).ToString());
            if(STATE != "")
            {
               MessageBox.Show(STATE);
               Reset();
                return;
            }
           //System.Threading.Thread.Sleep(1000);

        }
        private void Reset()
        {
            foreach(var btn in btnList)
            {
                if(btn.Text == "")
                {
                    btn.Enabled = false;
                }
            }
            label3.Text = "";
            label4.Text = "";
        }
        

        private void Play_Click(object sender, EventArgs e)
        {
            if((sender as Button).Text == "Play")
            {
                Form2 playerForm = new Form2(this);
                playerForm.ShowDialog();
                foreach(var btn in btnList)
                {
                    btn.Enabled = true;
                }
                game = new Algorithm(player, AI_player);
                (sender as Button).Text = "New Game";
            }
            else
            {                
                foreach(var btn in btnList)
                {
                    btn.Text = "";
                    btn.Enabled = false;
                }
                label3.Text = "";
                label4.Text = "";
                game = new Algorithm();
                (sender as Button).Text = "Play";
            }
            
        }
    }
    public class Player
    {
        public char Symbol { get; set; }
        public Color Color_Value { get; set; }
    }
}
