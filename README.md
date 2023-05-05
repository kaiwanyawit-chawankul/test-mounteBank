# How this repo works
## C# project to call to the external service

Command to create the test project
```
    dotnet new sln -n DemoMounteBank
    dotnet new xunit -n DemoMounteBank.Test -o test/DemoMounteBank.Test
    dotnet add test/DemoMounteBank.Test package FluentAssertions
    dotnet sln add test/DemoMounteBank.Test
    dotnet test
```
## Testing service locally

Use the script for docker-compose.yml
```
  test:
    build:
      context: .
      dockerfile: Dockerfile.test
    volumes:
      - .:/app
    working_dir: /app
    command: ["dotnet", "test", "--logger", "console;verbosity=normal" ]
```
To run test
```
docker-compose up --build --exit-code-from test
```

**Don't forget the argument `--exit-code-from test` to make the test stop when running along side with mountebank**

## Testing service with mounteBank
Add the script for docker-compose.yml
```
  mountebank:
    image: docker.io/bbyars/mountebank:latest
    ports:
      - "2525:2525"
      - "8080:8080"
    volumes:
      - ./mountebank/imposters:/imposters
    command: ["start", "--configfile", "/imposters/proxy-to-realserver.json"]
```
**Don't forget the `depends_on` ** on the test section

```
depends_on:
      - mountebank
```
Update service to use mountebank url
```
    environment:
      - MY_SERVICE_URL=http://mountebank:8080
    depends_on:
      - mountebank
```

## Getting mock request from mounteBank

Access docker container by `docker exec -it <container name> <command>` to generate the response file(in case you have any change on the endpoint)

```
docker exec -it test-mountebank-mountebank-1 mb save --port 2525 --savefile /imposters/imposters.json --removeProxies
```

Change docker-compose.yml to run from `imposters/response.json`
```
    command: ["start", "--configfile", "/imposters/response.json"]
```