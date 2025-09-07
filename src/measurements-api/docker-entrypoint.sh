#!/bin/bash

echo Running docker-entrypoint.sh

#
# Wait for PostgreSQL to be ready
#
echo Waiting for PostgreSQL to be ready...

retry=1
while [ $retry -lt 21 ]
do
   # Use pg_isready to check if PostgreSQL is accepting connections
   pg_isready -h postgres.local -p 5432 -U measurements_user -d MeasurementsDB
   if [ "$?" -eq 0 ]
   then
      echo "PostgreSQL is ready!"
      break
   else
      echo "******* Waiting for PostgreSQL, retry" $retry "*******"
      sleep 5
   fi
   retry=`expr $retry + 1`
done

if [ $retry -eq 21 ]; then
   echo "PostgreSQL failed to become ready after 20 retries"
   exit 1
fi

echo Starting api...

dotnet Measurements.Api.dll "$@"
