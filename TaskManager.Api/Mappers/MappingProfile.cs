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

        CreateMap<Product, ProductDeleteRequest>(MemberList.Destination).ReverseMap();

        CreateMap<Product, ProductUpdateRequest>(MemberList.Destination).ReverseMap();

        CreateMap<Category, CategoryCreateRequest>(MemberList.Destination).ReverseMap();

        CreateMap<Category, CategoryResponse>(MemberList.Destination).ReverseMap();

        CreateMap<Store, StoreCreateRequest>(MemberList.Destination).ReverseMap();

        CreateMap<Store, StoreResponse>(MemberList.Destination).ReverseMap();
    }
}