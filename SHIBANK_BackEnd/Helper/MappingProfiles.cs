using AutoMapper;
using SHIBANK.Models;
using SHIBANK.Dto;

namespace SHIBANK.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
            CreateMap<BankAccount, BankAccountDto>();

        }
    }
}
