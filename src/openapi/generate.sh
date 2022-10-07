#!/bin/bash

OUTPUT=generated
ME=$(id -u):$(id -g)

rm -rf ${OUTPUT}

# Generate controllers with nswag
docker run --rm -v ${PWD}:/local --user ${ME} countingup/nswag run "local/config.nswag" /runtime:Net50 \
  /variables:specUrl=measurements-api.yaml,component=Measurements.Api,output=${OUTPUT}/api

generate() {
    local generator=$1

    docker run --rm -v "${PWD}:/local" --user ${ME} openapitools/openapi-generator-cli generate \
        -i local/measurements-api.yaml \
        -g ${generator} \
        -o /local/${OUTPUT}/${generator} \
        -c local/config-${generator}.yaml
}

# Generate clients using openapitools
generators=('aspnetcore' 'python')

for gen in "${generators[@]}"; do
  generate $gen
done

cp generated/api/Measurements.Api.generated.cs ../measurements-api/Measurements.Api/Controllers
cp measurements-api.yaml ../measurements-api/Measurements.Api/wwwroot/openapi
