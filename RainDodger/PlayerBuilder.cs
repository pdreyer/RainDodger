using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainDodger
{
    public class PlayerBuilder
    {
        public Graphics GetPlayer(Graphics graphics, int x, int y)
        {
            Pen RedPen = new Pen(Color.Black, 2);
            //graphics.DrawEllipse(RedPen, x, 310, 50, 50); //Head
            //graphics.DrawEllipse(RedPen, x + 15, 320, 5, 5); //Left Eye
            //graphics.DrawEllipse(RedPen, x + 30, 320, 5, 5); //Right Eye
            //graphics.DrawEllipse(RedPen, x + 16, 340, 20, 6); //Mouth
            //graphics.DrawLine(RedPen, x + 25, 360, x + 25, 400); //Body
            //graphics.DrawLine(RedPen, x + 10, 380, x + 40, 380); //Arms
            //graphics.DrawLine(RedPen, x + 25, 400, x + 10, 420); //Left Leg
            //graphics.DrawLine(RedPen, x + 25, 400, x + 40, 420); //Right Leg

            graphics.DrawEllipse(RedPen, x, y, 50, 50); //Head
            graphics.DrawEllipse(RedPen, x + 15, y + 10, 5, 5); //Left Eye
            graphics.DrawEllipse(RedPen, x + 30, y + 10, 5, 5); //Right Eye
            graphics.DrawEllipse(RedPen, x + 16, y + 30, 20, 6); //Mouth
            graphics.DrawLine(RedPen, x + 25, y + 50, x + 25, y + 90); //Body
            graphics.DrawLine(RedPen, x + 10, y + 70, x + 40, y + 70); //Arms
            graphics.DrawLine(RedPen, x + 25, y + 90, x + 10, y + 110); //Left Leg
            graphics.DrawLine(RedPen, x + 25, y + 90, x + 40, y + 110); //Right Leg

            return graphics;
        }
    }
}
