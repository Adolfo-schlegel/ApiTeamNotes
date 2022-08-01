using TeamsNotesApi.Services.Interfaces;
using System.Threading.Tasks;
using System.Threading;
using TeamsNotesApi.Models.Notification;
using TeamsNotesApi.Models.Notification.Firebase;
using TeamsNotesApi.Models.Notification.Expo;

namespace TeamsNotesApi.Services.Notifications
{
    public class BackGroundTaskService : IHostedService, IDisposable
    {
        private INotificationExpoService? _notificationExpoService;
        private IMessageNotifiedService _messageNotifiedService;        
        private ICountStatusNoteService _countStatusNoteService;
        private ICountStatusNotesService2 _countStatusNotesService2;
        private System.Threading.Timer? _timer;

        public BackGroundTaskService(ICountStatusNoteService countStatusNoteService, INotificationExpoService? notificationExpoService, IMessageNotifiedService messageNotifiedService,ICountStatusNotesService2 countStatusNotesService2)
        {       
            _countStatusNoteService = countStatusNoteService;
            _countStatusNotesService2 = countStatusNotesService2;
            _notificationExpoService = notificationExpoService;
            _messageNotifiedService = messageNotifiedService;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {            
            //cuando empieza la tarea
            _timer = new Timer(TaskExpo, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));

            return Task.CompletedTask;
        }

        //public async void TaskExpo(object state)
        //{
        //    List<CountStatusNote> lst = new();

        //    lst = _countStatusNoteService.SelectCountNotes();

        //    foreach (var statusNote in lst)
        //    {                
        //        var result = await _notificationExpoService?.PushSendAsync(_messageNotifiedService.MessageToSend($"Tenes {statusNote.countNotesPending} notas pendientes", "CVM NOTAS", statusNote.token));

        //        //log
        //        Console.BackgroundColor = ConsoleColor.Red;
        //        Console.WriteLine("\nuser (" + statusNote.id_user + ") - token (" + statusNote.token + ")" + " < Send message > ");

        //        if (result?.PushTicketStatuses?.Count() > 0)
        //        {
        //            foreach (var error in result.PushTicketStatuses)
        //            {
        //                Console.ResetColor();
        //                Console.ForegroundColor = ConsoleColor.DarkYellow;
        //                Console.WriteLine($"\n--> response: {error.TicketId} - {error.TicketMessage} - {error.TicketStatus} - {error.TicketDetails} <--");
        //                Console.ResetColor();                                            
        //            }
        //        }
        //    }
        //}

        public async void TaskExpo(object state)
        {
            List<CountStatusNote> lst = new();

            lst = _countStatusNoteService.SelectCountNotes();

            foreach (var statusNote in lst)
            {
                var result = await _notificationExpoService?.PushSendAsync(_messageNotifiedService?.MessageToSend(_countStatusNotesService2?.SelectCountNotes(statusNote.id_user), "CVM NOTAS", statusNote?.token));

                //log
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("\nuser (" + statusNote.id_user + ") - token (" + statusNote.token + ")" + " < Send message > ");

                if (result?.PushTicketStatuses?.Count() > 0)
                {
                    foreach (var error in result.PushTicketStatuses)
                    {
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"\n--> response: {error.TicketId} - {error.TicketMessage} - {error.TicketStatus} - {error.TicketDetails} <--");
                        Console.ResetColor();
                    }
                }
            }
        }
        public async void TaskFirebase(object state)
        {
            //Console.WriteLine("\n Task Firebase eneble");

            //Notification notification = new()
            //{
            //    DeviceId = "ExponentPushToken[c527kLG3pOzb8uSm8ifIyd]",
            //    IsAndroiodDevice = true,
            //    Title = "Hola",
            //    Body ="Esto funciona 😎"            
            //};

            //Response result = await _notificationFirebaseService.SendNotification(notification);

            //Console.WriteLine("State = " + result.IsSuccess + " Message = " + result.Message);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //cuando la tarea se detenga, se va a nullear el timer este o no este null
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //cuando se muera el objeto
            _timer?.Dispose();
        }
    }
}
