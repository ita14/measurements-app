#!/bin/bash
set -o errexit

if [ -z "$1" ]
  then echo "Missing argument: azure subscription id" ; exit
fi

if [ -z "$2" ]
  then echo "Missing argument: resource group name" ; exit
fi

subscriptionId=$1
resourceGroup=$2

appId=$(az ad app create --display-name "Github OIDC" --output tsv --query appId)
echo Created app registration $appId

assigneeObjectId=$(az ad sp create --id $appId --output tsv --query id)
echo Created service principal $assigneeObjectId

roleAssignmentId=$(az role assignment create \
  --role contributor \
  --subscription $subscriptionId \
  --assignee-object-id $assigneeObjectId \
  --assignee-principal-type ServicePrincipal \
  --scope /subscriptions/$subscriptionId/resourceGroups/$resourceGroup \
  --output tsv \
  --query id)

echo Created role assignment $roleAssignmentId

#
# Using service principal secret. Will be deprecated.
#
# az ad sp create-for-rbac \
#  --name "myApp" \
#  --role contributor \
#  --scopes /subscriptions/<subsription-id>/resourceGroups/<resource-group-id> \
#  --sdk-auth
