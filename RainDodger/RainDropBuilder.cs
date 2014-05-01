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
        private int raindropCount = int.Parse(ConfigurationSettings.AppSettings["RaindropCount"].ToString());
        private int[,] RaindropArr;

        public int[,] RaindropManager(int screenWidth, Graphics graphRaindrop)
        {
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

            for (int i = 0; i < raindropCount; i++)
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

        public int[,] UpdateRaindrops(int[,] graphRaindrops)
        {
            for (int i = 0; i < raindropCount; i++)
            {
                graphRaindrops[i, 1] = int.Parse(graphRaindrops[i, 1].ToString()) + 5;
            }

            return graphRaindrops;
        }
    }
}
