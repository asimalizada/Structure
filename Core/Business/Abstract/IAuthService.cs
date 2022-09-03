using Core.Entities.Concrete;
using Core.Entities.Models;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.Jwt;

namespace Core.Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegister userForRegisterDto, string password);
        IDataResult<User> Login(UserForLogin userForLoginDto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
