# measurements-app

Start environment

```
docker compose up -d
```

Stop

```
docker compose down
```

Stop and remove volumes

```
docker compose down -v --remove-orphans
```

# Cosmos DB

Open emulator management UI at this [address](https://localhost:8081/_explorer/index.html).

To enable https on windows host download certificate...

```
curl -k https://localhost:8081/_explorer/emulator.pem > emulatorcert.crt
```

Right-click -> `install certificate.` Place certificate to `Trusted Root Certification Authorities`.
