FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
ENV ASPNETCORE_Environment=Production
ENV ASPNETCORE_URLS http://+:5000

WORKDIR /app
COPY . .
CMD ASPNETCORE_URLS=http://* dotnet QuoteOfTheDay.dll