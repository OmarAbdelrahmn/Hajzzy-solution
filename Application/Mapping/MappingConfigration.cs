using Application.Contracts.Auth;
using Domain.Entities;
using Mapster;

namespace Application.Mapping;

//public class MappingConfigration : IRegister
//{
//    public void Register(TypeAdapterConfig config)
//    {

//        //config.NewConfig<RegisterRequest, ApplicationUser>()
//        //    .Map(des => des.UserName, src => $"{src.FirstName}{src.LastName}");


//        config.NewConfig<RegisterRequest, ApplicationUser>()
//            .Map(des => des.UserName, src => src.Email);

//        config.NewConfig<(ApplicationUser user, string userrole), UserResponse>()
//            .Map(des => des, src => src.user
//            .Map(des => des.Roles, src => src.userroles);


//        config.NewConfig<Employees, EmpolyeeResponse>
//                ()
//                .Map(dest => dest.IBAN, src => src.IBAN)
//                .Map(dest => dest.NameEN, src => src.NameEN)
//                .Map(dest => dest.NameAR, src => src.NameAR);


//    }
//}
