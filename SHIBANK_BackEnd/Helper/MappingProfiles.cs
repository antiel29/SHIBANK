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

            CreateMap<BankAccount, BankAccountDto>()
                .ForMember(dest => dest.OpeningDate, opt => opt.MapFrom(src => src.OpeningDate.ToString("dd/MM/yy")));

            CreateMap<Transaction, TransactionDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("dd/MM/yy hh:mm:ss")));

            CreateMap<TransactionCreateDto, Transaction>();

            CreateMap<Card, CardDto>()
                .ForMember(dest => dest.ExpirationDate, opt => opt.MapFrom(src => src.ExpirationDate.ToString("MM/yy"))) ;
        }
    }
}
