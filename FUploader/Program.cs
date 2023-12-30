using FUploader.Core;
using FUploader.Core.FireBase;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseWindowsService();
builder.Services.AddWindowsService();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddSingleton<FirebaseAuth>();
builder.Services.AddSingleton <UploadManager>();
builder.Services.AddSingleton<Notifocator>();




var app = builder.Build();


app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapGet("/", () => "Here will be short description of api");
app.MapControllers();
app.Run();
