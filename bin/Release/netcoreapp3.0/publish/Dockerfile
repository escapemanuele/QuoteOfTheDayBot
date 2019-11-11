FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime

ENV ASPNETCORE_URLS http://+:80

WORKDIR /app
COPY . .

EXPOSE 80/tcp

CMD ASPNETCORE_URLS=http://* dotnet QuoteOfTheDay.dll