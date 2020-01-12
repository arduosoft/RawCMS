
echo "changin env.json"
ls /app/wwwroot/ -lh
ls /app/wwwroot/env/ -lh

#TODO replace with jq. this commands depends on positions
sed -i "/baseUrl/c\   \"baseUrl\" : \"$BASE_URL\"" /app/wwwroot/env/env.json
sed -i "/client_id/c\   \"client_id\" : \"$CLIENT_ID\"," /app/wwwroot/env/env.json
sed -i "/client_secret/c\   \"client_secret\" : \"$CLIENT_SECRET\"" /app/wwwroot/env/env.json


#main js
#sed -i "/url/c\   \"url\" : \"/ui/env/enc.json\"," /app/wwwroot/main.js

cat /app/wwwroot/env/env.json