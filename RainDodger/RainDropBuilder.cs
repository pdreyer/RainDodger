using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainDodger
{
    public class RaindropBuilder
    {
        private int[,] RaindropArr;

        public int[,] RaindropManager(int screenWidth, Graphics graphRaindrop)
        {
            int raindropCount = int.Parse(ConfigurationSettings.AppSettings["RaindropCount"].ToString());
            RaindropArr = new int[raindropCount, 2];
            List<Graphics> graphRaindrops = new List<Graphics>();

            int[] RaindropXpos = GenerateRaindropPOS(raindropCount, 10, screenWidth);
            int[] RaindropYpos = GenerateRaindropPOS(raindropCount, -500, 0);

            for (int i = 0; i < raindropCount; i++)
            {
                RaindropArr[i, 0] = RaindropXpos[i];
                RaindropArr[i, 1] = RaindropYpos[i];
            }

            return RaindropArr;
        }

        private int[] GenerateRaindropPOS(int length, int minValue, int maxValue)
        {
            int[] RandomArr = new int[length];

            for (int i = 0; i < length; i++)
            {
                Random rnd = new Random();
                int seconds = rnd.Next(3, 50);
                System.Threading.Thread.Sleep(seconds);

                int seed = System.Environment.TickCount;

                Random r = new Random(seed);
                int raindropPOS = r.Next(minValue, maxValue);

                RandomArr[i] = raindropPOS;
            }

            return RandomArr;
        }

        public int[,] UpdateRaindrops(int raindropCount, int[,] graphRaindrops, int screenHeight, int screenWidth)
        {
            for (int i = 0; i < raindropCount; i++)
            {
                int newPOS = int.Parse(graphRaindrops[i, 1].ToString()) + 5;

                if (newPOS > screenHeight)
                {
                    int[] RaindropXpos = GenerateRaindropPOS(1, 10, screenWidth);
                    newPOS = RaindropXpos[0];

                    //graphRaindrops[i, 0] = newPOS;
                    graphRaindrops[i, 1] = 0;
                }

                graphRaindrops[i, 1] = newPOS;
            }

            return graphRaindrops;
        }
    }
}
