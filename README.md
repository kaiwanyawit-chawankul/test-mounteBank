# How this repo works
## C# project to call to the external service
```
    dotnet new sln -n DemoMounteBank
    dotnet new xunit -n DemoMounteBank.Test -o test/DemoMounteBank.Test
    dotnet add test/DemoMounteBank.Test package FluentAssertions
    dotnet sln add test/DemoMounteBank.Test
    dotnet test
```
## Testing service locally
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

```
docker-compose up --build
```

## Testing service with mounteBank
```
  mountebank:
    image: docker.io/bbyars/mountebank:latest
    ports:
      - "2525:2525"
      - "8080:8080"
    volumes:
      - ./mountebank/imposters:/imposters
      - ./proxy-to-realserver.json:/imposters/proxy-to-realserver.json
    command: ["start", "--configfile", "/imposters/proxy-to-realserver.json"]
```

```
    environment:
      - MY_SERVICE_URL=http://mountebank:8080
    depends_on:
      - mountebank
```

```
docker-compose up --build --exit-code-from test
```
Don't forget the argument `--exit-code-from test` to make the test stop when running along side with mountebank


## Getting mock request from mounteBank
```
docker exec -it <container name> <command>

docker exec -it test-mountebank-mountebank-1 mb save --port 2525 --savefile /imposters/imposters.json --removeProxies
```

```
    command: ["start", "--configfile", "/imposters/response.json"]
```