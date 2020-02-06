FROM nginx:alpine

#!/bin/sh

COPY ./docker/config/ui/nginx.conf /etc/nginx/nginx.conf
COPY ./docker/config/ui/bootstrap.sh /bootstrap.sh

## Remove default nginx index page
RUN rm -rf /usr/share/nginx/html/*

# Copy from the stahg 1
COPY ./raw-cms-app/src  /usr/share/nginx/html

# Add bash
RUN apk add --no-cache bash 

CMD ["/bin/bash", "-c", " chmod 777 /bootstrap.sh && /bootstrap.sh && nginx -g \"daemon off;\""]