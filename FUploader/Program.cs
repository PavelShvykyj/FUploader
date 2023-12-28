var builder = WebApplication.CreateBuilder(args);
builder.Host.UseWindowsService();
builder.Services.AddWindowsService();
builder.Services.AddControllers().AddNewtonsoftJson();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.Run();
