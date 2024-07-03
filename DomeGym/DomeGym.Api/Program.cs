using DomeGym.Application;
using DomeGym.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddAuthorization();
    builder.Services.AddAuthentication();
    builder.Services.AddControllers();
    builder.Services.AddProblemDetails();

    builder.Services.AddApplication(builder.Configuration)
        .AddInfrastructure(builder.Configuration);
}


var app = builder.Build();
{
    app.UseExceptionHandler();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors(c =>
    {
        c.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.MapControllers();

    app.Run();
}