FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime


WORKDIR /app
COPY . .
EXPOSE 80
CMD ASPNETCORE_URLS=http://* dotnet QuoteOfTheDay.dll