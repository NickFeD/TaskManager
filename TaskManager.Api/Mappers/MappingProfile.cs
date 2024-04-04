using AutoMapper;
using TaskManager.Core.Entities;
using TaskManager.Core.Models;

namespace TaskManager.Api.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Board, BoardModel>(MemberList.Destination).ReverseMap();
        CreateMap<Board, BoardCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<Board, BoardUpdateModel>(MemberList.Destination).ReverseMap();

        CreateMap<ProjectParticipant, ParticipantModel>(MemberList.Destination).ReverseMap();
        CreateMap<ProjectParticipant, ParticipantCreateModel>(MemberList.Destination).ReverseMap();
        CreateMap<ProjectParticipant, ParticipantUpdateModel>(MemberList.Destination).ReverseMap();
    }
}