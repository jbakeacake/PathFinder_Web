FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

#Copy all libraries/apps related to dotnet (should exlcude client-app)
COPY . ./
RUN dotnet test API.Tests/ -c Release
RUN dotnet publish PathFinder_Web.sln -c Release -o PathFinder_Web/out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
EXPOSE 5000
ENV ASPNETCORE_URLS http://*:5000
WORKDIR /app

COPY --from=build-env /app/PathFinder_Web/out .

ENTRYPOINT ["dotnet", "API.dll"]