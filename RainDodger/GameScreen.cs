using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;           // The namespace being used from the Graphic functions
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RainDodger
{
    public partial class GameScreen : Form
    {
        /*Private variables used throughout the GameScreen class - Private so that's its only available within this GameScreen class*/
        private int LivesUsed = 0;                                                                                  // Counter that keeps track how many times the player died
        private int POSx = -1;                                                                                      // Players X (Horizontal) position on the screen
        private int POSy = -1;                                                                                      // Players Y (Vertical) position on the screen
        private bool runOncePlayer = true;                                                                          // boolean allowing the player start position to only be set once
        private bool runOnceRaindrops = true;                                                                       // boolean allowing the building and creation of the raindrops once
        private int raindropCount = int.Parse(ConfigurationSettings.AppSettings["RaindropCount"].ToString());       // Loading the config value for the amount of raindrops to be created
        private int rainddropSpeed = int.Parse(ConfigurationSettings.AppSettings["RaindropSpeed"].ToString());      // Loading the config value for the speed that the raindrops should move vertical

        private int playerLives = int.Parse(ConfigurationSettings.AppSettings["PlayerLives"].ToString());           // Loading the config value for the amount of lives the player has
        private int PlayerHeight = int.Parse(ConfigurationSettings.AppSettings["PlayerHeight"].ToString());         // Loading the config value for the players height to determine how far the player should be drawn from the bottom
        private int PlayerWidth = int.Parse(ConfigurationSettings.AppSettings["PlayerWidth"].ToString());           // Loading the config value for the players width to determine the centre position the player should be drawn

        private int[,] raindrops;                                                                                   // Two dimensional array that will hold the raindrops being created

        /*Constructor for the GameScreen class*/
        public GameScreen()
        {
            InitializeComponent();

            this.Width = int.Parse(ConfigurationSettings.AppSettings["ScreenWidth"].ToString());                    // Loading and setting the config value for the game screen width
            this.Height = int.Parse(ConfigurationSettings.AppSettings["ScreenHeight"].ToString());                  // Loading and setting the config value for the game screen height
            GameTimer.Interval = rainddropSpeed;                                                                    // Setting the game timers interval to the config value

            GameManager();                                                                                          // Calling the GameManager method to load the game functions
        }

        /*Form Load method - Gets loaded when the form is being initialised/Loaded*/
        private void GameScreen_Load(object sender, EventArgs e)
        {
            /*Clearing the screen and loading the player for the Welcome screen*/

            Graphics background = this.CreateGraphics();
            background.Clear(Color.White);

            /*Calling the GameManager method to load the game functions*/
            
            GameManager();
            this.Show();

            /*Displaying the welcome screen - Heading and loading the buttons to start the game*/

            lblLives.Text = "Welcome to the Rain Dodger game.";
            btnEndGame.Visible = true;
            btnRestart.Visible = true;
            lblLives.Visible = true;
        }

        /*GameManager method - Clear the background, Load the Player and Load the Raindrops*/
        private void GameManager()
        {
            Graphics background = this.CreateGraphics();
            background.Clear(Color.White);
            this.Show();
            PlayerManager();                                                                                // Calling the PlayerManager method to build and load the player
            RaindropManager();                                                                              // Calling the RaindropManager method to build and load the raindrops
        }

        /*PlayerManager method - Loading the player start position once, Load the player in the specified position*/
        private void PlayerManager()
        {
            if (runOncePlayer)                                                                              // Will calculate the players start position for the starting of the game only once
            {
                runOncePlayer = false;
                CalcPlayerStartPOS();                                                                       // Calling the CalcPlayerStartPOS to calculate the players starting position
            }

            Graphics playerGraphics = this.CreateGraphics();
            PlayerBuilder playerBuilder = new PlayerBuilder();                                              // Creating a new object of the PlayerBuilder class
            playerGraphics = playerBuilder.BuildPlayer(playerGraphics, POSx, POSy);                         // using the object created to call the BuildPlayer method to build and load the Player
        }

        /*RaindropManager method - Loading the raindrops original starting position once, Load the raindrops in the specified position*/
        private void RaindropManager()
        {
            Graphics raindrop = this.CreateGraphics();

            if (runOnceRaindrops)                                                                           // Will calculate the raindrops start position for the starting of the game only once
            {
                runOnceRaindrops = false;
                RaindropBuilder raindropBuilder = new RaindropBuilder();                                    // Creating a new object of the RaindropBuilder class
                raindrops = raindropBuilder.RaindropManager(this.Width, raindrop);                          // using the object created to call the RaindropManager method to build and load the raindrops
            }

            Pen BluePen = new Pen(Color.Blue, 3);                                                           // Creating a new instance of the Pen object to use for the raindrop creation
            this.Show();

            for (int i = 0; i < raindropCount; i++)
            {
                //Looping through all the raindrops that was created and loading them on screen
                raindrop.DrawEllipse(BluePen, raindrops[i, 0], raindrops[i, 1], 2, 5);                      // The DrawEllipse object is used to draw the raindrop shaped graphics
            }
        }

        /*CalcPlayerStartPOS method - Used to calculate the X (horizontal) and Y (Vertical) position for the player on the configurable player height and width*/
        public void CalcPlayerStartPOS()
        {
            int playerHeight = int.Parse(ConfigurationSettings.AppSettings["PlayerHeight"].ToString());     // Loading the config value for the players height
            int playerWidth = int.Parse(ConfigurationSettings.AppSettings["PlayerWidth"].ToString());       // Loading the config value for the players width

            List<int> screenSize = CalcScreenSize();                                                        // List variable of type integer created, populated by the response of the CalcScreenSize method
            int screenWidth = screenSize[0];                                                                // Integer variable populated by the integer List position 0 value
            int screenHeight = screenSize[1];                                                               // Integer variable populated by the integer List position 1 value

            POSx = (screenWidth / 2) - playerWidth;                                                         // Calculate the players start position in the middle of the screen by using the player width
            POSy = screenHeight - playerHeight;                                                             // Calculate the players start position at the bottom of the screen by using the player height
        }

        /*CalcScreenSize method - Used to calculate the size of the screen depending on the users preference (config)*/
        private List<int> CalcScreenSize()
        {
            List<int> screenSize = new List<int>();                                                         // List variable of type integer created, to hold game screen width and height
            screenSize.Add(this.Width);                                                                     // Adding the current game screen width to the List variable
            screenSize.Add(this.Height);                                                                    // Adding the current game screen height to the List variable

            return screenSize;                                                                              // returning the list variable to the calling method
        }

        /*GameTimer timer - Used to update the raindrop positions on the configured interval*/
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            RaindropBuilder raindropBuilder = new RaindropBuilder();                                        // Creating a new object of the RaindropBuilder class
            raindropBuilder.UpdateRaindrops(raindropCount, raindrops, this.Height, this.Width);             // using the object created to call the UpdateRaindrops method to increment there vertical position
            RenderScreen();                                                                                 // Calling the RenderScreen method to reload the screen with new raindrop positions
        }

        /*RenderScreen method - Used to reload the screen with updated player and raindrop positions, Check if player is still alive*/
        private void RenderScreen()
        {
            Graphics background = this.CreateGraphics();
            background.Clear(Color.White);                                                                  // Clearing the screen
            this.Show();

            PlayerManager();                                                                                // Calling the PlayerManager method to load the Player
            RaindropManager();                                                                              // Calling the RaindropManager method to load the Raindrops

            Pen playerArea = new Pen(Color.White, 2);                                                       // Creating a new instance of the Pen object to use for the player area creation
            background.DrawRectangle(playerArea, POSx - 2, POSy - 2, PlayerWidth + 5, PlayerHeight + 2);    // Drawing the player area around the player

            CheckAliveStatus();                                                                             // Calling the CheckAliveStatus method to calculate if the raindrop entered the player area and died because of it
        }
        
        /*CheckAliveStatus method - Used to calculate if any raindrops entered the players area and died because of it*/
        private void CheckAliveStatus()
        {
            for (int i = 0; i < raindropCount; i++)
            {
                int currRaindropXPos = raindrops[i, 0];                                                     // Loading each raindrops X (horizontal) position in a integer variable, one at a time
                int currRaindropYPos = raindrops[i, 1];                                                     // Loading each raindrops Y (vertical) position in a integer variable, one at a time
                int currRaindropType = raindrops[i, 2];                                                     // Loading each raindrops type in a integer variable, one at a time

                if (currRaindropXPos >= POSx && currRaindropXPos <= (POSx + PlayerWidth))                   // Check the condition of the X (horizontal) position of the raindrops, to see if it's inline with the player area
                    if (currRaindropYPos >= POSy && currRaindropYPos <= (POSy + PlayerHeight))              // Check the condition of the Y (vertical) position of the raindrops, to see if it's inline with the player area
                    {
                        if (currRaindropType == 0)                                                          // Check the condition of the raindrop type, 0 = decrement live and game ends
                        {
                            GameOver();                                                                     // If both the X and y position of any raindrop is inline with the player area the GameOver method is called
                            break;                                                                          // If the first raindrop inside the player area is found the loop through the raindrops is broken because the player is already dead
                        }
                    }
            }
        }

        /*GameOver method - used to end the game because the player is dead, calculating the lives left, Reload the screen with the dead player, Display the status on the screen*/
        private void GameOver()
        {
            GameTimer.Enabled = false;                                                                      // Stops the game timer to update the raindrops Y (vertical) position since the player is dead
            LivesUsed++;                                                                                    // Increment the lives the player already used up
            int remainingLives = playerLives - LivesUsed;                                                   // Calculating the remaining lives through subtracting the lives used from the total player lives

            Graphics deadPlayer = this.CreateGraphics();
            deadPlayer.Clear(Color.White);                                                                  // Clearing the screen
            this.Show();

            PlayerBuilder playerBuilder = new PlayerBuilder();                                              // Creating a new object of the PlayerBuilder class
            playerBuilder.KillPlayer(deadPlayer, POSx, POSy);                                               // using the object created to call the KillPlayer method to load the dead player

            /*Displaying the game over screen - Heading and loading the buttons to start the game*/

            btnEndGame.Visible = true;
            btnRestart.Visible = true;
            lblLives.Visible = true;
            btnRestart.Text = "Retry";
            lblLives.Text = String.Format("GAME OVER !!!! {0} / {1} lives remaining.", remainingLives.ToString(), playerLives.ToString());

            if (remainingLives == 0)                                                                        // Checking the condition if the player used up all the lives
                btnRestart.Enabled = false;                                                                 // Disabling the Restart button so that the player can't restart the game after the lives are finished
        }

        /*GameScreen_KeyDown method/event - Executes every time a button is pressed down*/
        private void GameScreen_KeyDown(object sender, KeyEventArgs e)
        {
            GameController gameController = new GameController();                                           // Creating a new object of the GameController class
            int moveDirection = gameController.PlayerMovementDirection(e.KeyValue.ToString());              // Calls the method exposed by using the created class object to get the movement direction returned

            POSx = POSx + moveDirection;                                                                    // If the RIGHT arrow was pressed the X (horizontal) position will be incremented - Player moves to the right
        }

        /*btnRestart_Click method/event - Event when the "restart" button was pressed ("New  Game" and "Restart" functionality*/
        private void btnRestart_Click(object sender, EventArgs e)
        {
            btnEndGame.Visible = false;
            btnRestart.Visible = false;
            lblLives.Visible = false;

            if (btnRestart.Text == "Retry") 
            {
                raindrops = new int[raindropCount, 3];                                                      // Creating a new object of the raindrops two dimensional array, this will clear the arrays values
                runOnceRaindrops = true;                                                                    // Reset the boolean runOnceRaindrops variables value so that the raindrops initial position gets loaded
            }

            GameManager();                                                                                  // Calling the GameManager method to load the Player and Raindrop methods
            GameTimer.Enabled = true;                                                                       // Starting the game timer so that the raindrops gets loaded

            this.Focus();                                                                                   // Set the focus on the Form control to activate the forms event
        }

        /*btnEndGame method/event - Event when the "end game" button was pressed*/
        private void btnEndGame_Click(object sender, EventArgs e)
        {
            Application.Exit();                                                                             // The application will be terminated
        }
    }
}
