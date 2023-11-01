#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["QueflityMVC/QueflityMVC.Web.csproj", "QueflityMVC/"]
COPY ["QueflityMVC.Application/QueflityMVC.Application.csproj", "QueflityMVC.Application/"]
COPY ["QueflityMVC.Domain/QueflityMVC.Domain.csproj", "QueflityMVC.Domain/"]
COPY ["QueflityMVC.Infrastructure/QueflityMVC.Infrastructure.csproj", "QueflityMVC.Infrastructure/"]
RUN dotnet restore "QueflityMVC/QueflityMVC.Web.csproj"
COPY . .
WORKDIR "/src/QueflityMVC"
RUN dotnet build "QueflityMVC.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QueflityMVC.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QueflityMVC.Web.dll"]