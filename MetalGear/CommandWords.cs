using System.Collections;
using System.Collections.Generic;
using System;

namespace MetalGear
{
    public class CommandWords
    {
        Dictionary<string, Command> commands;
        //NOTE BACK COMMAND IS NOT WORKING!!!!!!
        private static Command[] commandArray = { new GoCommand(), new QuitCommand(), new BackCommand(), new PickupCommand(), new InspectCommand(), new StatsCommand(), new UnlockCommand(), new GiveCommand(), new DropCommand()};

        public CommandWords() : this(commandArray)
        {
        }

        public CommandWords(Command[] commandList)
        {
            commands = new Dictionary<string, Command>();
            foreach (Command command in commandList)
            {
                commands[command.Name] = command;
            }
            Command help = new HelpCommand(this);
            commands[help.Name] = help;
        }

        public Command Get(string word)
        {
            Command command = null;
            commands.TryGetValue(word, out command);
            return command;
        }

        public string Description()
        {
            string commandNames = "";
            Dictionary<string, Command>.KeyCollection keys = commands.Keys;
            foreach (string commandName in keys)
            {
                commandNames += " " + commandName;
            }
            return commandNames;


        }
    }
}