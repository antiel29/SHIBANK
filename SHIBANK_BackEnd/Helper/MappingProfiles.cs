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
            CreateMap<UserDto, User>();

            CreateMap<BankAccount, BankAccountDto>();

            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionCreateDto, Transaction>();

            CreateMap<Card, CardDto>()
                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate.ToString("MM/yy"))) ;
        }
    }
}
