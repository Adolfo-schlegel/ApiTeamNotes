using CorePush.Google;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using TeamsNotesApi.Models.Notification.Firebase;
using TeamsNotesApi.Services.Interfaces;
using static TeamsNotesApi.Models.Notification.Firebase.GoogleNotification;

namespace TeamsNotesApi.Services.Notifications
{    
    public class NotificationFirebaseService : INotificationFirebaseService
    {
        private readonly FcmNotificationSetting _FcmNotificationSetting;
        public NotificationFirebaseService(IOptions<FcmNotificationSetting> fcmNorificationSettings)
        {
            _FcmNotificationSetting = fcmNorificationSettings.Value;
        }
        public async Task<Response> SendNotification(Notification notificationModel)
        {
            Response response = new Response();
            try
            {
                if (notificationModel.IsAndroiodDevice)
                {
                    /* FCM Sender (Android Device) */
                    FcmSettings settings = new FcmSettings()
                    {
                        SenderId = _FcmNotificationSetting.SenderId,
                        ServerKey = _FcmNotificationSetting.ServerKey
                    };

                    HttpClient httpClient = new HttpClient();

                    string authorizationKey = string.Format("keyy={0}", settings.ServerKey);
                    string deviceToken = notificationModel.DeviceId;

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorizationKey);
                    httpClient.DefaultRequestHeaders.Accept
                            .Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //custom message for send
                    DataPayload dataPayload = new DataPayload();

                    dataPayload.Title = notificationModel.Title;
                    dataPayload.Body = notificationModel.Body;

                    GoogleNotification notification = new GoogleNotification();
                    notification.Data = dataPayload;
                    notification.Notification = dataPayload;

                    var fcm = new FcmSender(settings, httpClient);
                    var fcmSendResponse = await fcm.SendAsync(deviceToken, notification);

                    if (fcmSendResponse.IsSuccess())
                    {
                        response.IsSuccess = true;
                        response.Message = "Notification sent successfully";
                        return response;
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = fcmSendResponse.Results[0].Error;
                        return response;
                    }
                }
                else
                {
                    /* Code here for APN Sender (iOS Device) */
                    //var apn = new ApnSender(apnSettings, httpClient);
                    //await apn.SendAsync(notification, deviceToken);
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Something went wrong";
                return response;
            }
        }
    }
}
