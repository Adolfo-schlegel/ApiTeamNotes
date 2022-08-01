namespace TeamsNotesApi.Services.Interfaces
{
    public interface IStatusUserNotificationService
    {
        public string InsertTokenUsers(int id_user, string token);
        public string ChangeStatusUser(int id_user, int status);
    }
}
