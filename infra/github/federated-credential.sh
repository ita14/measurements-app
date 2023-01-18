#!/bin/bash

if [ -z "$1" ]
  then echo "Missing argument: Azure ad application object id"
fi

az ad app federated-credential create \
  --id $1 \
  --parameters credential.json
