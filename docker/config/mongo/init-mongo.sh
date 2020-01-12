#!/bin/bash
echo "restoring data"
cd /docker-entrypoint-initdb.d/seed/

ls -1 *.json | sed 's/.json$//' | while read col; do 
    echo "restoring $col"
    mongoimport  -d rawcms -c $col --type json --jsonArray < $col.json; 
done

echo "data restored"

# echo "create $MONGO_INITDB_USERNAME user on $MONGO_INITDB_DATABASE"

# mongo -- "$MONGO_INITDB_DATABASE" <<EOF
#     var rootUser = '$MONGO_INITDB_ROOT_USERNAME';
#     var rootPassword = '$MONGO_INITDB_ROOT_PASSWORD';
#     var user = '$MONGO_INITDB_USERNAME';
#     var passwd = '$MONGO_INITDB_PASSWORD';
#     var admin = db.getSiblingDB('admin');
#     admin.auth(rootUser, rootPassword);
#     db.createUser({user: user, pwd: passwd, roles: [{role:"readWrite",db:"$MONGO_INITDB_DATABASE"}]);
#     db.createUser({user: user, pwd: passwd, roles: [{role:"readWrite",db:"admin"}]);
# EOF

# echo "user created"
