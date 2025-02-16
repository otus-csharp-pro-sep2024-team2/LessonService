# Stage 1: Base the image for executing the applicationя
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Stage 2: The image for building the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
# Copying project files (assuming that's all .the csproj files are in the appropriate folders)
COPY ["LessonService.Application.Services/LessonService.Application.Services.csproj", "LessonService.Application.Services/"]
COPY ["LessonService.Commands/LessonService.Commands.csproj", "LessonService.Commands/"]
COPY ["LessonService.Domain.Entities/LessonService.Domain.Entities.csproj", "LessonService.Domain.Entities/"]
COPY ["LessonService.Domain.Models/LessonService.Domain.Models.csproj", "LessonService.Domain.Models/"]
COPY ["LessonService.Domain.ValueObjects/LessonService.Domain.ValueObjects.csproj", "LessonService.Domain.ValueObjects/"]
COPY ["LessonService.Infrastructure.EF/LessonService.Infrastructure.EF.csproj", "LessonService.Infrastructure.EF/"]
COPY ["LessonService.Interfaces/LessonService.Interfaces.csproj", "LessonService.Interfaces/"]
COPY ["LessonService.WebApi/LessonService.WebApi.csproj", "LessonService.WebApi/"]

RUN dotnet restore "LessonService.WebApi/LessonService.WebApi.csproj"
COPY . .
WORKDIR "/src/LessonService.WebApi"
RUN dotnet build "LessonService.WebApi.csproj" -c Release -o /app/build

# Stage 3: Publishing an application
FROM build AS publish
RUN dotnet publish "LessonService.WebApi.csproj" -c Release -o /app/publish

# Stage 4: The final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LessonService.WebApi.dll"]
