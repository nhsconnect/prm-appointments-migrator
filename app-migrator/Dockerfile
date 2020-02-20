FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS debug
ENV DOTNET_USE_POLLING_FILE_WATCHER 1
WORKDIR /app
COPY . ./
RUN dotnet restore

FROM debug AS build
COPY . ./
RUN dotnet publish -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
EXPOSE 5001
ENTRYPOINT ["dotnet", "Api.dll"]