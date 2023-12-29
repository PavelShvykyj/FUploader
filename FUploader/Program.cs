using FUploader.Core;
using FUploader.Core.FireBase;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseWindowsService();
builder.Services.AddWindowsService();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddSingleton<FirebaseAuth>();
builder.Services.AddSingleton <UploadManager>();

var app = builder.Build();


app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();
