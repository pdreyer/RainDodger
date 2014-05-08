using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RainDodger
{
    class GameController
    {
        /*PlayerMovementDirection method - Used to set the players movement direction*/
        public int PlayerMovementDirection(string keyPressedValue)
        {
            int direction = -1;

            if (keyPressedValue == "39" || keyPressedValue == "102")                            // Check if the key pressed down is the RIGHT arrow
            {
                direction = 3;                                                                  // If the RIGHT arrow was pressed the X (horizontal) position will be incremented - Player moves to the right
            }

            if (keyPressedValue == "37" || keyPressedValue == "100")                            // Check if the key pressed down is the LEFT arrow
            {
                direction = - 3;                                                                // If the LEFT arrow was pressed the X (horizontal) position will be decremented - Player moves to the left
            }

            return direction;                                                                   // returns the direction value the player must move
        }
    }
}
