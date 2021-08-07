#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["KHLBotSharp.WebHook.NetCore3/KHLBotSharp.WebHook.NetCore3.csproj", "KHLBotSharp.WebHook.NetCore3/"]
RUN dotnet restore "KHLBotSharp.WebHook.NetCore3/KHLBotSharp.WebHook.NetCore3.csproj"
COPY . .
WORKDIR "/src/KHLBotSharp.WebHook.NetCore3"
RUN dotnet build "KHLBotSharp.WebHook.NetCore3.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KHLBotSharp.WebHook.NetCore3.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KHLBotSharp.WebHook.NetCore3.dll"]