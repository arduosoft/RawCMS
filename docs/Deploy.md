## Deply on docker containers
You can start from the base docker-compose. At the moment we have two images, one for the api and one for the ui. This means you have two containers. Both of them must be reachable by the user, so they have to be exposed. The following example creates and sets using local address and port mapping. On production you have to change them with public url. You can bind directly the port, even this may be tricky in case you want to use standard ports and the machine is not embedded for this application. Moreover, to enable https and get more control about traffic, it is suggested to run all the containers under a nginx proxy.

```yaml
version: "3"
services:
  rawcms-api:
    image: arduosoft/rawcms-api-preview:latest
    ports:
      - "3581:3581"
    environment:
      - MongoSettings__ConnectionString=mongodb://root:password@mongo:27017/rawcms?authSource=admin
      - PORT=3581
      - ASPNETCORE_ENVIRONMENT=Docker
      - ASPNETCORE_SERVER_URLS=http://*:3581
  rawcms-ui:
    image: arduosoft/rawcms-ui-preview:latest
    environment:
      - BASE_URL=http://localhost:3581
      - CLIENT_ID=raw.client
      - CLIENT_SECRET=raw.secret
  
    ports:
      - "3681:80"
  mongo:
    image: mongo
    environment:
      - MONGO_INITDB_ROOT_USERNAME=root
      - MONGO_INITDB_ROOT_PASSWORD=password
      - MONGO_INITDB_DATABASE=rawcms
    ports:
      - 38017:27017
  elasticsearch:
    image: elasticsearch:7.4.0
    environment:
      - discovery.type=single-node
      - http.cors.enabled=true
      - http.cors.allow-credentials=true
      - http.cors.allow-headers=X-Requested-With,X-Auth-Token,Content-Type,Content-Length,Authorization
      - http.cors.allow-origin=/https?:\/\/localhost(:[0-9]+)?/
      - "ES_JAVA_OPTS=-Xms512m -Xmx512m"
    ulimits:
      memlock:
        soft: -1
        hard: -1
    ports:
      - 4200:9200

```

Api will be available at http://localhost:3680 (api http://localhost:3580).

You can find documentationa about each docker image on [docker hub](https://hub.docker.com/u/arduosoft).


## Deploy on heroku
As the CMS is released in two different containers, you need to deploy two different application

### Deploy UI on Heroku
1. Create an app, ie. your-demo-ui
2. Set the environment variables. See later the variable mapping.
3. Deploy using the heroku cli

**Variables**
```bash
BASE_URL=<your api heroky url, i.e your-demo-api.herokuapp.com>
CLIENT_ID=raw.client
CLIENT_SECRET=raw.secret
GOOGLE_ANALITYCS= <your api key on GA, optional>
```

**deploy**
```bash
heroku container:push web -a your-demo-ui
heroku container:release web -a your-demo-ui

```


### Deploy API on Heroku
1. Create an app, ie. your-demo-ui
2. Set the environment variables. See later the variable mapping.
3. Deploy using the heroku cli

**Variables**
```bash
MongoSettings__ConnectionString=<url to mongo db, a mongo atlas free account can be OK>
ASPNETCORE_ENVIRONMENT=Docker
```

**deploy**
```bash
heroku container:push web -a your-demo-api
heroku container:release web -a your-demo-api

```


## Deploy using Kubernetes
A simple configuration for Kubernetes can be made using following yaml files

### UI 
save this file as ui.yml
```yaml
apiVersion: v1
kind: Service
metadata:
  name: ui
  labels:
    run: ui
spec:
  type: ClusterIP
  ports:
  - port: 80
    targetPort: 80
    protocol: TCP
    name: http
  selector:
    run: ui
---
apiVersion: extensions/v1beta1
kind: Deployment
metadata:
  name: ui
spec:
  replicas: 1
  template:
    metadata:
      labels:
        run: ui
    spec:
      containers:
      - name: api
        image: arduosoft/rawcms-ui-preview
        imagePullPolicy: Always
        ports:
         - containerPort: 80
        env:
        ## use secret on production
        - name: BASE_URL
          value: <your api url>
        - name: CLIENT_ID
          value: raw.client
        - name: CLIENT_ID
          value: raw.secret
 
```

## API

save this snippet as api.yml
```yaml
apiVersion: v1
kind: Service
metadata:
  name: api
  labels:
    run: api
spec:
  type: ClusterIP
  ports:
  - port: 80
    targetPort: 80
    protocol: TCP
    name: http
  selector:
    run: api
---
apiVersion: extensions/v1beta1
kind: Deployment
metadata:
  name: api
spec:
  replicas: 1
  template:
    metadata:
      labels:
        run: api
    spec:
      containers:
      - name: api
        image: arduosoft/rawcms-api-preview
        imagePullPolicy: Always
        ports:
         - containerPort: 80
        env:
        ## use secret on production
        - name: MongoSettings__ConnectionString
          value: <your mongodb url>
        - name: ASPNETCORE_ENVIRONMENT
          value: Docker
      
```

### Ingress
save this snippet as ingress.yml
```yaml
apiVersion: extensions/v1beta1
kind: Ingress
metadata:
  name: ingress
  annotations:
    # kubernetes.io/ingress.class: addon-http-application-routing # this directive is for azure AKS
spec:
  rules:
  - host: <my API url>
    http:
      paths:
      - backend:
          serviceName: frontend
          servicePort: 80
        path: /
  - host: <my API url>
    http:
      paths:
      - backend:
          serviceName: api
          servicePort: 80
        path: /

```
### Deploy it
```bash
kubectl create -f ui.yml

kubectl create -f api.yml

kubectl create -f ingress.yml
```

You can create a kubernetes cluster from scratch using Microsoft Azure using [this simple tutorial](https://medium.com/swlh/how-to-deploy-an-asp-net-application-with-kubernetes-3c00c5fa1c6e?source=friends_link&sk=de1e07739413943d6a03f8ae232e5408)


## Manual deployment
If you want you can use the zip packages and deploy them directly. This practice is niether recommended nor supported.
The two applications can be deployed as following:
- **UI** is a static html web site, can be serverd by nginx or all other web server. All the url must point to index.html with a rewrite rule. You can use the nginx condiguration of our [nginx container as refernence ](https://github.com/arduosoft/RawCMS/blob/master/docker/config/ui/nginx.conf)
- **API** is a regular aspnet core application, and can be run using command 'dotnet RawCMS.dll'. IIS should work as well. It is tested on frmework 2.2 and 2.1
