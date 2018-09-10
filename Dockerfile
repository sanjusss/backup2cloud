
FROM microsoft/dotnet:2.1-sdk AS build
MAINTAINER sanjusss <sanjusss@qq.com>
WORKDIR /src
COPY . /src
#RUN dotnet restore 
#RUN dotnet build  -c Release 
RUN dotnet publish -c Release -o /app

FROM microsoft/dotnet:2.1-runtime AS final
MAINTAINER sanjusss <sanjusss@qq.com>
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "Backup2Cloud.dll"]