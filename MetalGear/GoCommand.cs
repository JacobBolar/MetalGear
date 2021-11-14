using System.Collections;
using System.Collections.Generic;

namespace MetalGear
{
    public class GoCommand : Command
    {

        public GoCommand() : base()
        {
            this.Name = "go";
        }

        override
            public bool Execute(Snake snake)
        {
            if (this.HasSecondWord())
            {
                snake.WalkTo(this.SecondWord);
            }
            else
            {
                snake.OutputMessage("\nGo Where?");
            }
            return false;
        }
    }
}