#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ProjetoVidaTOBE_Server/ProjetoVidaTOBE_Server.csproj", "ProjetoVidaTOBE_Server/"]
RUN dotnet restore "ProjetoVidaTOBE_Server/ProjetoVidaTOBE_Server.csproj"
COPY . .
WORKDIR "/src/ProjetoVidaTOBE_Server"
RUN dotnet build "ProjetoVidaTOBE_Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProjetoVidaTOBE_Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjetoVidaTOBE_Server.dll"]