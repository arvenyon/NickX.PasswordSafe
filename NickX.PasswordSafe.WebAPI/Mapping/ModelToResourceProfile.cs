using AutoMapper;
using NickX.PasswordSafe.WebAPI.Domain.Models;
using NickX.PasswordSafe.WebAPI.Resources;

namespace NickX.PasswordSafe.WebAPI.Mapping
{
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Category, CategoryResource>();
            CreateMap<Password, PasswordResource>();
        }
    }
}
