using System;

namespace MetalGear
{
    public class GiveCommand : Command
    {
        public GiveCommand()
        {
            this.Name = "give";
        }

        public override bool Execute(Snake snake)
        {
            if (this.HasSecondWord())
            {
                snake.Give(SecondWord);
            }
            else
            {
                Console.WriteLine("Give What?");
            }
            return false;
        }
    }
}