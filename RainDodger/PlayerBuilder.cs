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
        public Graphics GetPlayer(int x, int y)
        {
            Graphics dc = new Graphics();

            Pen RedPen = new Pen(Color.Red, 2);
            dc.DrawEllipse(RedPen, x + y, 310, 50, 50); //Head
            dc.DrawEllipse(RedPen, x + 15 + y, 320, 5, 5); //Left Eye
            dc.DrawEllipse(RedPen, x + 30 + y, 320, 5, 5); //Right Eye
            dc.DrawEllipse(RedPen, x + 16 + y, 340, 20, 6); //Mouth
            dc.DrawLine(RedPen, x + 25 + y, 360, x + 25 + y, 400); //Body
            dc.DrawLine(RedPen, x + 10 + y, 380, x + 40 + y, 380); //Arms
            dc.DrawLine(RedPen, x + 25 + y, 400, x + 10 + y, 420); //Left Leg
            dc.DrawLine(RedPen, x + 25 + y, 400, x + 40 + y, 420); //Right Leg

            return dc;
        }
    }
}
