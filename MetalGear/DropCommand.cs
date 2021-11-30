using System;
namespace MetalGear
{
    public class DropCommand : Command
    {
        public DropCommand()
        {
            this.Name = "drop";
        }

        public override bool Execute(Snake snake)
        {
            if (this.HasSecondWord())
            {
                snake.drop(SecondWord);
            }
            else
            {
                Console.WriteLine("Drop What?");
            }

            return false;
        }
    }
}