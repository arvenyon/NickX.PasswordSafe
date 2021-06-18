using AutoMapper;
using NickX.PasswordSafe.Models.Main;
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
