namespace MetalGear
{
    public class BackCommand : Command // used to go back to last room
    {
        

        public BackCommand()
        {
            this.Name = "back";
        }

        public override bool Execute(Snake snake)
        {
            snake.Back();
            return false;
        }
    }
}