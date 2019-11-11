FROM microsoft/dotnet:3.0-aspnetcore-runtime
WORKDIR /app
COPY . .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet QuoteOfTheDay.dll