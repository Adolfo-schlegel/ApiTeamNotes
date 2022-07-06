using TeamsNotesApi.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TeamsNotesApi.Models.Common;
using TeamsNotesApi.Tools.Interface;
using TeamsNotesApi.Tools;
using TeamsNotesApi.Services.Security;
using TeamsNotesApi.Services.Notes;
using TeamsNotesApi.Tools.EncryptPass;
using TeamsNotesApi.Services.Notifications;
using CorePush.Google;
using CorePush.Apple;
using TeamsNotesApi.Models.Notification.Firebase;

var builder = WebApplication.CreateBuilder(args);

//{{{{{{{{{{{{{ALL COMMON SERVICES}}}}}}}}}}}}}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IMyNotesService, MyNotesService>();

//<----------------Security------------------>
builder.Services.AddScoped<IEncrypt, Encrypt>();
builder.Services.AddScoped<IJwtAuth, JwtAuth>();
//<같같같같같같같같같같같같같같같같같같같같같�>

//<--------------Notification---------------->
builder.Services.AddScoped<ISaveTokenUserService, SaveTokenUserService>();
builder.Services.AddSingleton<IMessageNotifiedService, MessageNotifiedService>();
builder.Services.AddSingleton<ICountStatusNoteService, CountStatusNoteService>();
builder.Services.AddSingleton<INotificationExpoService, NotificationExpoService>();

//builder.Services.AddHttpClient<FcmSender>();
//builder.Services.AddHttpClient<ApnSender>();

//var appSettingsSection = builder.Configuration.GetSection("FcmNotification");
//builder.Services.Configure<FcmNotificationSetting>(appSettingsSection);

//<같같같같같같같같같같같같같같같같같같같같같�>


//<--------------Task in BackGround------------------>
builder.Services.AddHostedService<BackGroundTaskService>();
//<같같같같같같같같같같같같같같같같같같같같같같같>



//|||||||||||||||||||||||||||||||JWT|||||||||||||||||||||||||||||||||||||||
var appAppSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appAppSettingsSection);

//settings jwt
var appSettings = appAppSettingsSection.Get<AppSettings>();
var llave = Encoding.ASCII.GetBytes(appSettings.Secreto);

builder.Services.AddAuthentication(option => {
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
    .AddJwtBearer(option => {
        option.RequireHttpsMetadata = false;
        option.SaveToken = true;
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(llave),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

//|||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
