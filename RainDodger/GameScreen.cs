using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
        public int POSx = -1;
        public int POSy = -1;

        public GameScreen()
        {
            InitializeComponent();

            GameManager();
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

            Graphics background = this.CreateGraphics();
            background.Clear(Color.White);
            this.Show();
            PlayerManager();

        }

        private void PlayerManager()
        {
            CalcPlayerStartPOS();

            Graphics dc = this.CreateGraphics();
            PlayerBuilder playerBuilder = new PlayerBuilder();
            dc = playerBuilder.GetPlayer(dc, POSx, POSy);
        }

        private void RaindropManager()
        {
            //Build raindrops / update raindrops
            //Raindrop Manager (Check raindrops in screen, Count in screen, 
        }

        private void CalcPlayerStartPOS()
        {
            int playerHeight = int.Parse(ConfigurationSettings.AppSettings["PlayerHeight"].ToString());

            List<int> screenSize = CalcScreenSize();
            int screenWidth = screenSize[0];
            int screenHeight = screenSize[1];

            POSx = screenWidth / 2;
            POSy = screenHeight - playerHeight;
        }

        private List<int> CalcScreenSize()
        {
            List<int> screenSize = new List<int>();
            screenSize.Add(this.Width);
            screenSize.Add(this.Height);

            return screenSize;
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
                //currentPos = currentPos + 1;
                POSx = POSx + 1;
            }

            if (e.KeyValue.ToString() == "37" || e.KeyValue.ToString() == "100")
            {
                //currentPos = currentPos - 1;
                POSx = POSx - 1;
            }
        }
    }
}
