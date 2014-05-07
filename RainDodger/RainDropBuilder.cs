using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;           // The namespace being used from the Graphic functions
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainDodger
{
    public class RaindropBuilder
    {
        private int[,] RaindropArr;                                                                                 // Two dimensional array that will hold the raindrops being created

        /*RaindropManager method - used to generate all the raindrops*/
        public int[,] RaindropManager(int screenWidth, Graphics graphRaindrop)
        {
            int raindropCount = int.Parse(ConfigurationSettings.AppSettings["RaindropCount"].ToString());           // Loading the config value for the amount of raindrops to be created
            RaindropArr = new int[raindropCount, 2];                                                                // Two dimensional array that will hold the raindrops being created

            int[] RaindropXpos = GenerateRaindropPOS(raindropCount, 10, screenWidth);                               // Calling the GenerateRaindropsPOS method to get the raindrops X (horizontal) positions
            int[] RaindropYpos = GenerateRaindropPOS(raindropCount, -500, 0);                                       // Calling the GenerateRaindropsPOS method to get the raindrops Y (vertical) positions

            for (int i = 0; i < raindropCount; i++)
            {
                RaindropArr[i, 0] = RaindropXpos[i];                                                                // Loading the X (horizontal) positions into the two dimensional array position 0
                RaindropArr[i, 1] = RaindropYpos[i];                                                                // Loading the Y (vertical) positions into the two dimensional array position 1
            }

            return RaindropArr;                                                                                     // returning the fully populated two dimensional array
        }

        /*GenerateRaindropPOS method - used to randomly generate the X or Y positions*/
        private int[] GenerateRaindropPOS(int length, int minValue, int maxValue)
        {
            int[] RandomArr = new int[length];                                                                      // Declaring the one dimensional array that will hold the random X or Y position

            for (int i = 0; i < length; i++)
            {
                Random rnd = new Random();
                int seconds = rnd.Next(3, 50);                                                                      // Generating a random number (seconds) that the thread will sleep, This is to help with the random number generation to have a more random position
                System.Threading.Thread.Sleep(seconds);                                                             // Sleep thread used to make the generation of the random number more random

                int seed = System.Environment.TickCount;                                                            // Getting the new seed that will be used for generating the random number

                Random r = new Random(seed);                                                                        // Creating a new object of the random function with the generated seed
                int raindropPOS = r.Next(minValue, maxValue);                                                       // Generating a random number between the specified values (screen width or height) that will be used for the X or Y position

                RandomArr[i] = raindropPOS;                                                                         // Populating the generated value into the one dimensional array
            }

            return RandomArr;                                                                                       // return the fully populated array with all the X or Y positions
        }

        /*UpdateRaindrops method - used to update the raindrops location to move them downwards (vertical)*/
        public int[,] UpdateRaindrops(int raindropCount, int[,] graphRaindrops, int screenHeight, int screenWidth)
        {
            for (int i = 0; i < raindropCount; i++)
            {
                int newXPOS = int.Parse(graphRaindrops[i, 0].ToString());
                int newYPOS = int.Parse(graphRaindrops[i, 1].ToString()) + 5;                                        // Incrementing the raindrops vertical position so that they move downwards (vertical)

                if (newYPOS > screenHeight)                                                                          // Checking if the new Y position (vertical) of the raindrop is outside the game screens boundary (raindrop moved out the bottom of the screen)
                {
                    int[] RaindropXpos = GenerateRaindropPOS(1, 0, screenWidth);                                     // Generating a new random position for the raindrop that moved outside the screen by calling the GenerateRaindropPOS method
                    newXPOS = RaindropXpos[0];                                                                       // Populating the new X (horizontal) position
                    newYPOS = 0;                                                                                     // Populating the Y (vertical) position as 0 so that the raindrops appear from the top again
                }

                graphRaindrops[i, 0] = newXPOS;                                                                     // Populating the new X (horizontal) position in the two dimensional array
                graphRaindrops[i, 1] = newYPOS;                                                                     // Populating the new Y (vertical) position in the two dimensional array
            }

            return graphRaindrops;                                                                                  // returning the newly populated two dimensional array
        }
    }
}
