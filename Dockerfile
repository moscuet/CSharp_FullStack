# Use .NET 8.0 SDK for build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy the solution file and restore dependencies as distinct layers
COPY *.sln ./
COPY Eshop.Core/*.csproj ./Eshop.Core/
COPY Eshop.Service/*.csproj ./Eshop.Service/
COPY Eshop.WebAPI/*.csproj ./Eshop.WebAPI/
COPY Eshop.Controller/*.csproj ./Eshop.Controller/
RUN dotnet restore

# Copy everything else and build the application
COPY . ./
RUN dotnet publish Eshop.WebAPI/Eshop.WebAPI.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Eshop.WebAPI.dll"]
