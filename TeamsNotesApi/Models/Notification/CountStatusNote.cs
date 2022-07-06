namespace TeamsNotesApi.Models.Notification
{
    public class CountStatusNote
    {
        public int id_user { get; set; }
        public string? token { get; set; }
        public int countNotesPending { get; set; }
    }
}
