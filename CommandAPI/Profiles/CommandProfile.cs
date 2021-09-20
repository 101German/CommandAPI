using AutoMapper;
using CommandAPI.DTO;
using CommandAPI.Models;


namespace CommandAPI.Profiles
{
    public class CommandProfile:Profile
    {
        public CommandProfile()
        {
            CreateMap<Command, CommandReadDTO>();
            CreateMap<CommandCreateDTO, Command>();
            CreateMap<CommandUpdateDTO, Command>().ReverseMap();
        }
    }
}
