global using Microsoft.EntityFrameworkCore;
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
using TeamsNotesApi.Services.Connections;

var builder = WebApplication.CreateBuilder(args);

//ALL COMMON SERVICES>>>>
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//<-----DatabaseContext EntityFramework------>

builder.Services.AddDbContext<CVM_GPA_SEG_01Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStrings"));
});

//<----------------tools--------------------->
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IMyNotesService, MyNotesService>();

//<----------------Security------------------>
builder.Services.AddScoped<IEncrypt, Encrypt>();
builder.Services.AddScoped<IJwtAuth, JwtAuth>();

//<--------------Notification---------------->
builder.Services.AddScoped<IStatusUserNotificationService, StatusUserNotificationService>();
builder.Services.AddSingleton<IMessageNotifiedService, MessageNotifiedService>();
builder.Services.AddSingleton<ICountStatusNoteService, CountStatusNoteService>();
builder.Services.AddSingleton<INotificationExpoService, NotificationExpoService>();
builder.Services.AddSingleton<ICountStatusNotesService2, CountStatusNotesService2>();
//builder.Services.AddHostedService<BackGroundTaskService>();

/*
    builder.Services.AddHttpClient<FcmSender>();
    builder.Services.AddHttpClient<ApnSender>();
    var appSettingsSection = builder.Configuration.GetSection("FcmNotification");
    builder.Services.Configure<FcmNotificationSetting>(appSettingsSection);
*/


//<<<<<ALL COMMON SERVICES

//JWT>>>>>
var appAppSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appAppSettingsSection);

//<---------settings------------->
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

//<<<<<JWT


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
