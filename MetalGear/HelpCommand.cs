using System.Collections;
using System.Collections.Generic;

namespace MetalGear
{
    public class HelpCommand : Command
    {
        CommandWords words;

        public HelpCommand() : this(new CommandWords())
        {
        }

        public HelpCommand(CommandWords commands) : base()
        {
            words = commands;
            this.Name = "help";
        }

        override
            public bool Execute(Snake snake)
        {
            if (this.HasSecondWord())
            {
                snake.OutputMessage("\nI cannot help you with " + this.SecondWord);
            }
            else
            {
                snake.OutputMessage("\nYou are lost. You are alone. You wander around the university, \n\nYour available commands are " + words.Description());
            }
            return false;
        }
    }
}