using TeamsNotesApi.Models.UserLogin;

namespace TeamsNotesApi.Tools.Interface
{
    public interface IJwtAuth
    {
        public string GetNewToken(User User);
    }
}
