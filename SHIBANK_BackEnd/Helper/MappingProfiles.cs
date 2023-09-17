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
            CreateMap<UserRegisterDto, User>();

            CreateMap<BankAccount, BankAccountDto>();
            CreateMap<BankAccountCreateDto, BankAccount>();

            CreateMap<Transaction, TransactionDto>();
        }
    }
}
