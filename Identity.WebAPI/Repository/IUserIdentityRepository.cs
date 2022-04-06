using Identity.WebAPI.Models;
using System.Threading.Tasks;

namespace Identity.WebAPI.Repository
{
    public interface IUserIdentityRepository
    {

        Task<UserIdentity> RegisterUser(UserIdentity item);

        UserIdentity LoginUser(LoginModel user);

        Task<UserIdentity> ChangePassword(int id, UserIdentity user);

        string GetToken(UserIdentity user);

        string GetPassword(int id);


    }
}
