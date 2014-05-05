﻿using System;
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
        private int LivesUsed = 0;
        private int POSx = -1;
        private int POSy = -1;
        private bool runOncePlayer = true;
        private bool runOnceRaindrops = true;
        private int raindropCount = int.Parse(ConfigurationSettings.AppSettings["RaindropCount"].ToString());
        private int rainddropSpeed = int.Parse(ConfigurationSettings.AppSettings["RaindropSpeed"].ToString());

        private int playerLives = int.Parse(ConfigurationSettings.AppSettings["PlayerLives"].ToString());
        private int PlayerHeight = int.Parse(ConfigurationSettings.AppSettings["PlayerHeight"].ToString());
        private int PlayerWidth = int.Parse(ConfigurationSettings.AppSettings["PlayerWidth"].ToString());

        private int[,] raindrops;

        public GameScreen()
        {
            InitializeComponent();

            GameManager();
            RaindropManager();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Graphics background = this.CreateGraphics();
            background.Clear(Color.White);

            GameManager();
            this.Show();

            lblLives.Text = "Welcome to the Rain Dodger game.";
            btnEndGame.Visible = true;
            btnRestart.Visible = true;
            lblLives.Visible = true;
        }

        private void GameManager()
        {
            PlayerLives();
            PlayerScoreCalc();

            Graphics background = this.CreateGraphics();
            background.Clear(Color.White);
            this.Show();
            PlayerManager();
            RaindropManager();
        }

        private void PlayerLives()
        {
        }

        private void PlayerScoreCalc()
        {
        }

        private void PlayerManager()
        {
            if (runOncePlayer)
            {
                runOncePlayer = false;
                CalcPlayerStartPOS();
            }

            Graphics dc = this.CreateGraphics();
            PlayerBuilder playerBuilder = new PlayerBuilder();
            dc = playerBuilder.BuildPlayer(dc, POSx, POSy);
        }

        private void RaindropManager()
        {
            Graphics raindrop = this.CreateGraphics();

            if (runOnceRaindrops)
            {
                runOnceRaindrops = false;
                RaindropBuilder raindropBuilder = new RaindropBuilder();
                raindrops = raindropBuilder.RaindropManager(this.Width, raindrop);
            }

            Pen BluePen = new Pen(Color.Blue, 3);
            this.Show();

            for (int i = 0; i < raindropCount; i++)
            {
                raindrop.DrawEllipse(BluePen, raindrops[i, 0], raindrops[i, 1], 2, 5);
            }
        }

        public void CalcPlayerStartPOS()
        {
            int playerHeight = int.Parse(ConfigurationSettings.AppSettings["PlayerHeight"].ToString());
            int playerWidth = int.Parse(ConfigurationSettings.AppSettings["PlayerWidth"].ToString());

            List<int> screenSize = CalcScreenSize();
            int screenWidth = screenSize[0];
            int screenHeight = screenSize[1];

            List<int> playerPOS = new List<int>();
            POSx = (screenWidth / 2) - playerWidth;
            POSy = screenHeight - playerHeight;
        }

        private List<int> CalcScreenSize()
        {
            List<int> screenSize = new List<int>();
            screenSize.Add(this.Width);
            screenSize.Add(this.Height);

            return screenSize;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            RaindropBuilder raindropBuilder = new RaindropBuilder();
            raindropBuilder.UpdateRaindrops(raindropCount, raindrops, this.Height, this.Width);
            RenderScreen();
        }

        private void RenderScreen()
        {
            Graphics background = this.CreateGraphics();
            background.Clear(Color.White);
            this.Show();

            PlayerManager();
            RaindropManager();

            Pen playerArea = new Pen(Color.White, 2);
            background.DrawRectangle(playerArea, POSx - 2, POSy - 2, PlayerWidth + 5, PlayerHeight + 2); 

            CheckAliveStatus();
        }

        private void CheckAliveStatus()
        {
            bool Dead = false;

            for (int i = 0; i < raindropCount; i++)
            {
                int currRaindropXPos = raindrops[i, 0];
                int currRaindropYPos = raindrops[i, 1];

                if (currRaindropXPos >= POSx && currRaindropXPos <= (POSx + PlayerWidth))
                    if (currRaindropYPos >= POSy && currRaindropYPos <= (POSy + PlayerHeight))
                    {
                        GameOver();
                        break;
                    }
            }
        }

        private void GameOver()
        {
            GameTimer.Enabled = false;
            LivesUsed++;
            int remainingLives = playerLives - LivesUsed;

            Graphics deadPlayer = this.CreateGraphics();
            deadPlayer.Clear(Color.White);
            this.Show();

            PlayerBuilder playerBuilder = new PlayerBuilder();
            playerBuilder.KillPlayer(deadPlayer, POSx, POSy);

            btnEndGame.Visible = true;
            btnRestart.Visible = true;
            lblLives.Visible = true;
            btnRestart.Text = "Retry";
            lblLives.Text = String.Format("GAME OVER !!!! {0} / {1} lives remaining.", remainingLives.ToString(), playerLives.ToString());

            if (remainingLives == 0)
                btnRestart.Enabled = false;
        }

        private void GameScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString() == "39" || e.KeyValue.ToString() == "102")
            {
                POSx = POSx + 3;
            }

            if (e.KeyValue.ToString() == "37" || e.KeyValue.ToString() == "100")
            {
                POSx = POSx - 3;
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            btnEndGame.Visible = false;
            btnRestart.Visible = false;
            lblLives.Visible = false;

            if (btnRestart.Text == "New Game")
            {
                GameManager();
                RaindropManager();
                GameTimer.Enabled = true;
            }
            else
            {
                raindrops = new int[raindropCount, 2];
                runOnceRaindrops = true;
                GameManager();
                RaindropManager();
                GameTimer.Enabled = true;
            }

            this.Focus();
        }

        private void btnEndGame_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
