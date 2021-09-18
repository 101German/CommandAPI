using CommandAPI.Data;
using CommandAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController:ControllerBase
    {
        private readonly ICommandRepo _commandRepo;

        public CommandsController(ICommandRepo commandRepo)
        {
            _commandRepo = commandRepo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Command>> Get()
        {
            var commands = _commandRepo.GetAllCommands();

            return Ok(commands);
        }

        [HttpGet("{id}")]
        public ActionResult<Command> GetCompanyById(int id)
        {
            var commandItem = _commandRepo.GetCommandById(id);

            if(commandItem == null)
            {
                return NotFound();
            }
            return Ok(commandItem);
        }
    }
}
