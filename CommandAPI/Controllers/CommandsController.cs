using AutoMapper;
using CommandAPI.Data;
using CommandAPI.DTO;
using CommandAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public ActionResult<IEnumerable<CommandReadDTO>> GetAllCommands()
        {
            var commands = _commandRepo.GetAllCommands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDTO>>(commands));
        }

        [HttpGet("{id}",Name ="GetCommandById")]
        public ActionResult<CommandReadDTO> GetCommandById(int id)
        {
            var commandItem = _commandRepo.GetCommandById(id);

            if(commandItem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CommandReadDTO>(commandItem));
        }

        [HttpPost]
        public ActionResult<CommandReadDTO> CreateCommand(CommandCreateDTO cmdDTO)
        {
            var cmdModel = _mapper.Map<Command>(cmdDTO);
            _commandRepo.CreateCommand(cmdModel);
            _commandRepo.SaveChanges();

            var cmdReadDTO = _mapper.Map<CommandReadDTO>(cmdModel);

            return CreatedAtRoute(nameof(GetCommandById), new { Id = cmdModel.Id }, cmdReadDTO);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id,CommandUpdateDTO cmdUpdate)
        {
            var cmdModel = _commandRepo.GetCommandById(id);
            if (cmdModel == null)
            {
                return NotFound();
            }


            _mapper.Map(cmdUpdate, cmdModel);
            _commandRepo.UpdateCommand(cmdModel);
            _commandRepo.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id,JsonPatchDocument<CommandUpdateDTO> pathDoc)
        {
            var cmdModel = _commandRepo.GetCommandById(id);
            if (cmdModel == null)
            {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDTO>(cmdModel);
            pathDoc.ApplyTo(commandToPatch, ModelState);

            if (!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, cmdModel);

            _commandRepo.UpdateCommand(cmdModel);

            _commandRepo.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var cmdModel = _commandRepo.GetCommandById(id);
            if (cmdModel == null)
            {
                return NotFound();
            }

            _commandRepo.DeleteCommand(cmdModel);
            _commandRepo.SaveChanges();

            return NoContent();
        }
    }
}
