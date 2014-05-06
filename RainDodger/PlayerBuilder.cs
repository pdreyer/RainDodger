using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainDodger
{
    public class PlayerBuilder
    {
        /*BuildPlayer method - Builds the player for the game*/
        public Graphics BuildPlayer(Graphics graphics, int x, int y)
        {
            Pen penColor = new Pen(Color.Black, 2);

            graphics.DrawEllipse(penColor, x, y, 50, 50); //Head
            graphics.DrawEllipse(penColor, x + 15, y + 10, 5, 5); //Left Eye
            graphics.DrawEllipse(penColor, x + 30, y + 10, 5, 5); //Right Eye
            graphics.DrawEllipse(penColor, x + 16, y + 30, 20, 6); //Mouth
            graphics.DrawLine(penColor, x + 25, y + 50, x + 25, y + 90); //Body
            graphics.DrawLine(penColor, x + 10, y + 70, x + 40, y + 70); //Arms
            graphics.DrawLine(penColor, x + 25, y + 90, x + 10, y + 110); //Left Leg
            graphics.DrawLine(penColor, x + 25, y + 90, x + 40, y + 110); //Right Leg

            return graphics;
        }

        /*KillPlayer method - Builds the dead player for the game*/
        public Graphics KillPlayer(Graphics graphics, int x, int y)
        {
            Pen penColor = new Pen(Color.Black, 2);

            graphics.DrawEllipse(penColor, x, y, 50, 50); //Head
            graphics.DrawLine(penColor, x + 15, y + 10, x + 25, y + 20); //Left Eye
            graphics.DrawLine(penColor, x + 25, y + 10, x + 15, y + 20); //Left Eye
            graphics.DrawLine(penColor, x + 30, y + 10, x + 40, y + 20); //Right Eye
            graphics.DrawLine(penColor, x + 40, y + 10, x + 30, y + 20); //Right Eye
            graphics.DrawEllipse(penColor, x + 16, y + 30, 20, 0); //Mouth
            graphics.DrawLine(penColor, x + 25, y + 50, x + 25, y + 90); //Body
            graphics.DrawLine(penColor, x + 10, y + 70, x + 40, y + 70); //Arms
            graphics.DrawLine(penColor, x + 25, y + 90, x + 10, y + 110); //Left Leg
            graphics.DrawLine(penColor, x + 25, y + 90, x + 40, y + 110); //Right Leg

            return graphics;
        }
    }
}
