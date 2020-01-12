
echo "changin env.json"
ls /usr/share/nginx/html/ -lh
ls /usr/share/nginx/html/env/ -lh

#TODO replace with jq. this commands depends on positions
sed -i "/baseUrl/c\   \"baseUrl\" : \"$BASE_URL\"" /usr/share/nginx/html/env/env.json
sed -i "/client_id/c\   \"client_id\" : \"$CLIENT_ID\"," /usr/share/nginx/html/env/env.json
sed -i "/client_secret/c\   \"client_secret\" : \"$CLIENT_SECRET\"" /usr/share/nginx/html/env/env.json

cat /usr/share/nginx/html/env/env.json