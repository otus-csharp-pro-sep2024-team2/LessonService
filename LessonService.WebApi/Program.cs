using LessonService.WebApi.Endpoints;
using LessonService.WebApi.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();
app.UseExceptionHandler();

app.MapGroup("/api/v1")
    .WithTags("LessonService endpoints")
    .MapLessonEndPoint();

app.Run();


