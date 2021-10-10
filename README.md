# Build and run the Docker image

1.Open a command prompt and navigate to your project folder.

2.Use the following commands to build and run your Docker image:

```bash
docker build -t docker101 .
docker run -d --name docker101-container docker101
```

## View the web page running from a container

Go to http://localhost:8099/swagger/index.html to access your app in a web browser.

## Develop

### Local windows

Native

    ```cmd
    dotnet publish -c Release -o out
    dotnet .\out\IdentityServer.API.dll
    ```

Docker

```sh
docker build -t docker101 .
docker run -p 80:80 --name docker101-container docker101
```

### Curl

```sh
curl --location --request GET 'http://localhost:80/api/v1/Products/GetAllx/health'
```
