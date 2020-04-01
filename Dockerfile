FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.csproj ./
COPY packages.config ./
COPY *.json ./
COPY . ./
RUN ls /app


RUN nuget.exe restore cryptopayments.csproj -SolutionDirectory ../ -Verbosity normal
RUN MSBuild.exe cryptopayments.csproj /t:build /p:Configuration=Release /p:OutputPath=./out



FROM mcr.microsoft.com/dotnet/framework/sdk:4.8

WORKDIR /app
COPY --from=build app/out .


ENTRYPOINT ["CryptoPayments.exe", "."]