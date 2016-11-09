FROM microsoft/dotnet:latest
COPY . /app
WORKDIR /app
 
RUN ["dotnet", "restore"]
RUN ["dotnet", "build", "./src/FutureStack"]
 
EXPOSE 5000/tcp
ENV ASPNETCORE_URLS http://*:5000
 
WORKDIR /app/src/FutureStack
ENTRYPOINT ["dotnet", "run"]