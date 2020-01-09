# docker conventions

ports are +1000 i.e. elastic is on 9300 instead of 9200

## files

- dockerfiles
- config
  - machine name
    - files to be burn\used
- data
  - machine
    - volumes mount for data

# start for dev

docker-compose up

#start prod in dev

docker-compose -f docker-compose.yml -f ./docker-compose-app.yml up

# trick

docker-compose build images only if are not present. to rebuild force it by docker-compose -f ./docker-compose-app.yml build
