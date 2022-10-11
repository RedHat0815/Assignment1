#!/bin/bash

if ["$#" -t 1]; then
    echo "usage: build-container.sh <build-number>"
    exit 1
fi
 
USER_PREFIX=jesmoo
BUILD_NUMBER=$1

docker build -t awebapp .

docker tag awebapp "idi2020.azurecr.io/$USER_PREFIX-backend:latest"
docker tag awebapp "idi2020.azurecr.io/$USER_PREFIX-backend:$BUILD_NUMBER"


docker push "idi2020.azurecr.io/$USER_PREFIX-backend:latest"
docker push "idi2020.azurecr.io/$USER_PREFIX-backend:$BUILD_NUMBER"