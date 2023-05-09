
#Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . . 

RUN dotnet restore "./WorkerServiceKon/WorkerServiceKon.csproj" --disable-parallel
RUN dotnet publish "./WorkerServiceKon/WorkerServiceKon.csproj" -c Release -o /app --no-restore

#base release stage
FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "WorkerServiceKon.dll"]