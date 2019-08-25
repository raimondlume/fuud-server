FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY BLL.App/*.csproj ./BLL.App/
COPY BLL.App.DTO/*.csproj ./BLL.App.DTO/
COPY Contracts.BLL.App/*.csproj ./Contracts.BLL.App/
COPY Contracts.DAL.App/*.csproj ./Contracts.DAL.App/
COPY Contracts.WebCrawler.Base/*.csproj ./Contracts.WebCrawler.Base/
COPY Contracts.WebCrawler.App/*.csproj ./Contracts.WebCrawler.App/
COPY DAL.App.DTO/*.csproj ./DAL.App.DTO/
COPY DAL.App.EF/*.csproj ./DAL.App.EF/
COPY Domain/*.csproj ./Domain/
COPY PublicApi.v1/*.csproj ./PublicApi.v1/
COPY PublicApi.v1.DTO/*.csproj ./PublicApi.v1.DTO/
COPY WebApp/*.csproj ./WebApp/
COPY WebCrawler/*.csproj ./WebCrawler/
RUN dotnet restore

# copy everything else and build app

COPY BLL.App/. ./BLL.App/
COPY BLL.App.DTO/. ./BLL.App.DTO/
COPY Contracts.BLL.App/. ./Contracts.BLL.App/
COPY Contracts.DAL.App/. ./Contracts.DAL.App/
COPY Contracts.WebCrawler.Base/. ./Contracts.WebCrawler.Base/
COPY Contracts.WebCrawler.App/. ./Contracts.WebCrawler.App/
COPY DAL.App.DTO/. ./DAL.App.DTO/
COPY DAL.App.EF/. ./DAL.App.EF/
COPY Domain/. ./Domain/
COPY PublicApi.v1/. ./PublicApi.v1/
COPY PublicApi.v1.DTO/. ./PublicApi.v1.DTO/
COPY WebApp/. ./WebApp/
COPY WebCrawler/. ./WebCrawler/
WORKDIR /app/WebApp
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS http://+:5000
EXPOSE 5000
COPY --from=build /app/WebApp/out ./
ENTRYPOINT ["dotnet", "WebApp.dll"]
