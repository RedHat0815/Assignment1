FROM mcr.microsoft.com/dotnet/aspnet:6.0
RUN mkdir -p /Assignment1

COPY ./release ./Assignment1

WORKDIR /Assignment1

ENTRYPOINT [ "dotnet", "Assignment1.dll" ]
