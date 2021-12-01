using System;
namespace MetalGear
{
    public class BuyCommand : Command //command used to buy items in trading room
    {
        public BuyCommand()
        {
            this.Name = "buy";
        }

        public override bool Execute(Snake snake)
        {
            if (this.HasSecondWord())
            {
                snake.Buy(this.SecondWord);
            }
            else
            {
                snake.OutputMessage("\nDrop what?");
            }
            return false;
        }
    }
}