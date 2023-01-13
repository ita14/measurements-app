# measurements-app

Exercise for collecting [RuuviTag](https://ruuvi.com/fi/ruuvitag/) measurements displaying them in React app.

![react app](/images/app.png)

## Components

### [measurements-app](src/measurements-app/README.md)

React application for charting the measurents data.

### [measurements-api](src/measurements-api/README.md)

Net7 API for managing sensors and storing measurements data.

### [measurements-collector](src/measurements-collector/README.md)

Python script for collecting RuuviTag data and publishing it to the API.

## Start local environment

Build images

```
docker compose build
```

Start environment

```
docker compose up -d
```

Open React application at http://localhost:8001

Keycloak Realm is configured having user `user` with password `foobar` which can be used for logging into React app.
Logging is required for accessing sensor settings.

Stop

```
docker compose down
```

Stop and remove volumes

```
docker compose down -v --remove-orphans
```

## Keycloak

Use `admin` username and `admin` password to access [Keycloak admin panel](http://keycloak.local:8080/admin).

https://www.keycloak.org/getting-started/getting-started-docker

## Cosmos DB

Open emulator management UI at this [address](https://localhost:8081/_explorer/index.html).

To enable https on windows host download certificate...

```
curl -k https://localhost:8081/_explorer/emulator.pem > emulatorcert.crt
```

Right-click -> `install certificate.` Place certificate to `Trusted Root Certification Authorities`.

## API

Development API is running at port 7001 and Swagger can be accessed at http://localhost:7001/swagger

## Code generation

This project uses design first approach for API development. When openapi spec is updated code should be generated using script

```shell
cd src/openapi
./generate.sh
```

This will generate API controllers and typescript and python clients.
