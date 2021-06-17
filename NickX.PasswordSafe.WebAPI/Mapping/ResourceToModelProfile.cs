using AutoMapper;
using NickX.PasswordSafe.WebAPI.Domain.Models;
using NickX.PasswordSafe.WebAPI.Resources;

namespace NickX.PasswordSafe.WebAPI.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveCategoryResource, Category>();
            CreateMap<SavePasswordResource, Password>();
        }
    }
}
