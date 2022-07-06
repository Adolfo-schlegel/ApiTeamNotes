using TeamsNotesApi.Models.Notification;

namespace TeamsNotesApi.Services.Interfaces
{
    public interface ICountStatusNoteService
    {
        public List<CountStatusNote> SelectCountNotes();
    }
}
