#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Web/MonArtisan.Web/MonArtisan.Web.csproj", "Web/MonArtisan.Web/"]
COPY ["Web/MonArtisan.Web.Infrastructure/MonArtisan.Web.Infrastructure.csproj", "Web/MonArtisan.Web.Infrastructure/"]
COPY ["Data/MonArtisan.Data/MonArtisan.Data.csproj", "Data/MonArtisan.Data/"]
COPY ["MonArtisan.Common/MonArtisan.Common.csproj", "MonArtisan.Common/"]
COPY ["Data/MonArtisan.Data.Models/MonArtisan.Data.Models.csproj", "Data/MonArtisan.Data.Models/"]
COPY ["Data/MonArtisan.Data.Common/MonArtisan.Data.Common.csproj", "Data/MonArtisan.Data.Common/"]
COPY ["Services/MonArtisan.Services.Data/MonArtisan.Services.Data.csproj", "Services/MonArtisan.Services.Data/"]
COPY ["Web/MonArtisan.Web.ViewModels/MonArtisan.Web.ViewModels.csproj", "Web/MonArtisan.Web.ViewModels/"]
COPY ["Services/MonArtisan.Services.Mapping/MonArtisan.Services.Mapping.csproj", "Services/MonArtisan.Services.Mapping/"]
COPY ["Services/MonArtisan.Services/MonArtisan.Services.csproj", "Services/MonArtisan.Services/"]
COPY ["Services/MonArtisan.Services.Messaging/MonArtisan.Services.Messaging.csproj", "Services/MonArtisan.Services.Messaging/"]
RUN dotnet restore "Web/MonArtisan.Web/MonArtisan.Web.csproj"
COPY . .
WORKDIR "/src/Web/MonArtisan.Web"
RUN dotnet build "MonArtisan.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MonArtisan.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MonArtisan.Web.dll"]