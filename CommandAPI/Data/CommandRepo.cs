using CommandAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace CommandAPI.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly CommandContext _repositoryContext;

        public CommandRepo(CommandContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public void CreateCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _repositoryContext.Add(cmd);
        }

        public void DeleteCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }
            _repositoryContext.CommandItems.Remove(cmd);
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return _repositoryContext.CommandItems.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _repositoryContext.CommandItems.FirstOrDefault(cmd => cmd.Id == id);
        }

        public bool SaveChanges()
        {
            return _repositoryContext.SaveChanges() >= 0;
        }

        public void UpdateCommand(Command cmd)
        {
           //TO DO
        }
    }
}
