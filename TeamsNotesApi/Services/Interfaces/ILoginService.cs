using TeamsNotesApi.Models;
using TeamsNotesApi.Models.UserLogin;

namespace TeamsNotesApi.Services.Interfaces
{
    public interface ILoginService
    {
        public Reply? ValidateUser(User use);
    }
}
