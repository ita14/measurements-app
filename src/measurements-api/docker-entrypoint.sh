#!/bin/bash

echo Running docker-entrypoint.sh

#
# Retrieve the self-signed SSL certificate of the CosmosDB Emulator and install it
# # Ref: https://docs.microsoft.com/en-us/azure/cosmos-db/linux-emulator?tabs=ssl-netstd21#run-on-linux
#
echo Retrieving self-signed SSL certificate from CosmosDB Emulator

retry=1
while [ $retry -lt 21 ]
do
   wget --no-check-certificate --output-document=/tmp/emulator.crt https://cosmosdb.local:8081/_explorer/emulator.pem
   if [ "$?" -eq 0 ]
   then
      echo "Got CosmosDB certificate!"
      break
   else
      echo "******* Waiting for retry" $retry "*******"
      sleep 10
   fi
   retry=`expr $retry + 1`
done

cp /tmp/emulator.crt /usr/local/share/ca-certificates
update-ca-certificates

echo Starting api..

dotnet Measurements.Api.dll "$@"
