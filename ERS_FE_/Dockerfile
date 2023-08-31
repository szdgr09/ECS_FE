#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ERS_FE_/ERS_FE_.csproj", "ERS_FE_/"]
RUN dotnet restore "ERS_FE_/ERS_FE_.csproj"
COPY . .
WORKDIR "/src/ERS_FE_"
RUN dotnet build "ERS_FE_.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ERS_FE_.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ERS_FE_.dll"]