using CommandAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new NotImplementedException();
        }
    }
}
