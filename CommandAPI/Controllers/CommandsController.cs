using AutoMapper;
using CommandAPI.Data;
using CommandAPI.DTO;
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
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo commandRepo,IMapper mapper)
        {
            _commandRepo = commandRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDTO>> Get()
        {
            var commands = _commandRepo.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDTO>>(commands));
        }

        [HttpGet("{id}")]
        public ActionResult<Command> GetCompanyById(int id)
        {
            var commandItem = _commandRepo.GetCommandById(id);

            if(commandItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CommandReadDTO>(commandItem));
        }
    }
}
