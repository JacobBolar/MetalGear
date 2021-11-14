namespace MetalGear
{
    public class InspectCommand : Command // inspect the chest in the room
    {
        public InspectCommand()
        {
            this.Name = "inspect";
        }

        public override bool Execute(Snake snake)
        {
            if (this.HasSecondWord())
            {
                snake.Inspect(this.SecondWord);
            }
            else
            {
                snake.OutputMessage("\nInspect what?");
            }
            return false;
        }
    }
}