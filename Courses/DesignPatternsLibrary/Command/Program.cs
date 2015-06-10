/**
 * From Wikipedia
 * In object-oriented programming, the command pattern is a behavioral design pattern in which an object is used to encapsulate 
 * all information needed to perform an action or trigger an event at a later time. This information includes the method name, 
 * the object that owns the method and values for the method parameters.
 **/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Command
{
    class Client
    {
        static void Main(string[] args)
        {
            var availableCommands = GetAvailableCommands();

            if(args.Length != 0)
            {
                var parser = new CommandParser(availableCommands);
                var command = parser.ParseCommand(args);

                if (command != null)
                    command.Execute();
            }
        }

        static IEnumerable<ICommandFactory> GetAvailableCommands()
        {
            return new ICommandFactory[]
            {
                new CreateOrderCommand(),
                new UpdateCommand(),
            };
        }
    }

    interface ICommand
    {
        void Execute();
    }

    interface ICommandFactory 
    {
        string CommandName { get; }
        string Description { get; }
        ICommand MakeCommand(string[] arguments);
    }

    #region Command Objects
    class UpdateCommand : ICommand, ICommandFactory
    {
        public int NewQuantity { get; private set; }
        public void Execute()
        {
            const int oldQuantity = 5;
            Console.WriteLine("Database: Updated");
            Console.WriteLine("LOG: Updated order quantity from {0} to {1}", oldQuantity, NewQuantity);
        }


        public string CommandName
        {
            get { return "UpdateCommand"; }
        }

        public string Description
        {
            get { return "Update the DB"; }
        }

        public ICommand MakeCommand(string[] arguments)
        {
            return new UpdateCommand();
        }
    }

    class CreateOrderCommand : ICommand, ICommandFactory
    {

        public void Execute()
        {
            Console.WriteLine("Order Created.");
        }

        public string CommandName
        {
            get { return "CreateOrderCommand"; }
        }

        public string Description
        {
            get { return "Create Order"; }
        }

        public ICommand MakeCommand(string[] arguments)
        {
            return new CreateOrderCommand();
        }
    }

    class NullCommand : ICommand
    {
        readonly string _name;
        public NullCommand(string name)
        {
            _name = name;
        }

        public void Execute()
        {
            Console.WriteLine(string.Format("Could not find command: {0}", _name));
        }
    }

    #endregion

    class CommandParser
    {
        readonly IEnumerable<ICommandFactory> _availableCommands;

        public CommandParser(IEnumerable<ICommandFactory> availableCommands)
        {
            this._availableCommands = availableCommands;
        }

        internal ICommand ParseCommand(string[] args)
        {
            var requestedCommandName = args[0];

            var command = FindRequestedCommand(requestedCommandName);
            if (command == null)
                return new NullCommand(requestedCommandName);

            return command.MakeCommand(args);
        }

        ICommandFactory FindRequestedCommand(string commandName)
        {
            return _availableCommands.FirstOrDefault(cmd => cmd.CommandName == commandName);
        }
    }




}
