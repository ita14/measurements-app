# measurements-app

Exercise for collecting [RuuviTag](https://ruuvi.com/fi/ruuvitag/) measurements and displaying them in React app.

![react app](/images/app.png)

## Components

### [measurements-app](src/measurements-app/README.md)

React application for charting the measurents data.

### [measurements-api](src/measurements-api/README.md)

Net Core API for managing sensors and storing measurements data.

### [measurements-collector](src/measurements-collector/README.md)

Python script for collecting RuuviTag data and publishing it to the API.

## Start local environment

Build and start

```
docker compose build
docker compose up -d
```

After system is up collector will publish mock data on 30 second intervals.

> For token validation to work keycloak and react app must have following aliases.On windows `hosts` file can be found from `C:\Windows\System32\drivers\etc`

```
127.0.0.1 keycloak.local
127.0.0.1 measurements-app.local
```

Following table lists components and url's...

| Component               | Url                                          | Notes                        |
| ----------------------- | -------------------------------------------- |------------------------------|
| React app               | http://measurements-app.local:8001           | Username `user` pw `foobar`  |
| Keycloak admin panel    | http://keycloak.local:8080/admin             | Username `admin` pw `admin`  |
| Cosmos DB management UI | https://localhost:8081/\_explorer/index.html |                              |
| API swagger doc         | http://localhost:7001/swagger                |                              |

Stop

```
docker compose down
```

Stop and remove volumes

```
docker compose down -v --remove-orphans
```

## Cosmos DB

To enable https on windows host download certificate...

```
curl -k https://localhost:8081/_explorer/emulator.pem > emulatorcert.crt
```

Right-click -> `install certificate.` Place certificate to `Trusted Root Certification Authorities`.

## Code generation

This project uses design first approach for API development. When openapi spec is updated code should be generated using script

```shell
cd src/openapi
./generate.sh
```

This will generate API controllers and typescript and python clients.

## Known issues

Sometimes cosmos db emulator gets stuck on initialization and won't start. In this case
the Api fails to start as it can't fetch the cosmos db certificate. Restarting the containers should fix the issue.

## Links

https://www.keycloak.org/getting-started/getting-started-docker
