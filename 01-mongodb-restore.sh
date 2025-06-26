#!/bin/bash
set -e

# Only restore if database is empty
if [ -z "$(mongosh --quiet --eval "db.getSiblingDB('$MONGO_INITDB_DATABASE').getCollectionNames().length" --username $MONGO_INITDB_ROOT_USERNAME --password $MONGO_INITDB_ROOT_PASSWORD --authenticationDatabase admin)" ] ||
    [ "$(mongosh --quiet --eval "db.getSiblingDB('$MONGO_INITDB_DATABASE').getCollectionNames().length" --username $MONGO_INITDB_ROOT_USERNAME --password $MONGO_INITDB_ROOT_PASSWORD --authenticationDatabase admin)" -eq "0" ]; then
    echo "$MONGO_INITDB_DATABASE Database is empty. Restoring data..."
    
    echo "mongorestore \
      --username $MONGO_INITDB_ROOT_USERNAME \
      --password $MONGO_INITDB_ROOT_PASSWORD \
      --authenticationDatabase admin \
      --db $MONGO_INITDB_DATABASE \
      /mongodb_dump/$MONGO_INITDB_DATABASE"

    # Restore from database dump
    mongorestore \
      --username $MONGO_INITDB_ROOT_USERNAME \
      --password $MONGO_INITDB_ROOT_PASSWORD \
      --authenticationDatabase admin \
      --db $MONGO_INITDB_DATABASE \
      /mongodb_dump/$MONGO_INITDB_DATABASE
      
    echo "Data restoration completed."
else
    echo "$MONGO_INITDB_DATABASE Database already contains data. Skipping restoration."
fi