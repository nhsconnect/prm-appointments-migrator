FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY . ./
RUN dotnet restore

# copy everything else and build app
RUN dotnet build -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
EXPOSE 5001
ENTRYPOINT ["dotnet", "Api.dll"]