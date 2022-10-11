FROM mcr.microsoft.com/dotnet/aspnet:6.0

COPY ./release ./AWebApp

WORKDIR /AWebApp

ENTRYPOINT [ "dotnet", "AWebApp.dll" ]