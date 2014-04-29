using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RainDodger
{
    public partial class GameScreen : Form
    {
        int currentPos = -1;
        int raindropCount = -1;

        public GameScreen()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Configs:
            // Raindrop count
            // Speed / Level
            // Lives
            
            //Start Game
            //Settings
        }

        private void GameManager()
        {
            //Calculate game over / lives
            //Score calc
        }

        private void PlayerManager()
        {
            Graphics dc = this.CreateGraphics();
            //Build player
        }

        private void RaindropManager()
        {
            //Build raindrops / update raindrops
            //Raindrop Manager (Check raindrops in screen, Count in screen, 
        }

        private void CalcScreenSize()
        {
        }

        private void CalcPlayerStartPOS()
        {
        }

        private int[,] GenerateRaindropPOS()
        {
            //Random number gen x2

            return new int[1,1];
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            //Timer - Render screen
        }

        private void GameScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString() == "39" || e.KeyValue.ToString() == "102")
            {
                currentPos = currentPos + 1;
            }

            if (e.KeyValue.ToString() == "37" || e.KeyValue.ToString() == "100")
            {
                currentPos = currentPos - 1;
            }
        }
    }
}
