#!/bin/sh
echo "changing env.json"
ls /usr/share/nginx/html/ 
ls /usr/share/nginx/html/env/ 

#TODO replace with jq. this commands depends on positions
sed -i "/baseUrl/c\   \"baseUrl\" : \"$BASE_URL\"" /usr/share/nginx/html/env/env.json
sed -i "/client_id/c\   \"client_id\" : \"$CLIENT_ID\"," /usr/share/nginx/html/env/env.json
sed -i "/client_secret/c\   \"client_secret\" : \"$CLIENT_SECRET\"" /usr/share/nginx/html/env/env.json

cat /usr/share/nginx/html/env/env.json

if [ ! -z "$PORT" ]
then
 sed -i -e 's/80/'"$PORT"'/g' /etc/nginx/nginx.conf
 echo "\$PORT is NOT empty"
else 
 echo "\$PORT is empty, used default port 80"
fi

ls  /usr/share/nginx/html

if [ ! -z "$GOOGLE_ANALITYCS" ]
then
 echo "settings google analytic for $GOOGLE_ANALITYCS (no regex)"
 GA="<script async src=\"https://www.googletagmanager.com/gtag/js?id=$GOOGLE_ANALITYCS\"></script> <script> window.dataLayer = window.dataLayer \|\| []; function gtag(){dataLayer.push(arguments);} gtag('js', new Date()); gtag('config', '$GOOGLE_ANALITYCS'); </script>"
 sed -i -e "s|</body>|$GA|g" /usr/share/nginx/html/index.html

fi