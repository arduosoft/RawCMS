## Deply on docker containers

You can start from the base docker-compose. 
The following example creates and sets using local address and port mapping. On production you have to change them with public url. You can bind directly the port, even this may be tricky in case you want to use standard ports and the machine is not embedded for this application. Moreover, to enable https and get more control about traffic, it is suggested to run all the containers under a nginx proxy.

```yaml
version: "3"
services:
  mongo:
    image: mongo
    environment:
      - MONGO_INITDB_ROOT_USERNAME=root
      - MONGO_INITDB_ROOT_PASSWORD=password
      - MONGO_INITDB_DATABASE=rawcms
      - MONGO_INITDB_USERNAME=dev
      - MONGO_INITDB_PASSWORD=password
    ports:
      - 28017:27017
  elasticsearchtest:
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
      - 9300:9200  
  rawcms-app:
    image: arduosoft/rawcms-alpha
    depends_on:
      - mongo
      - elasticsearchtest
    ports:
      - "6580:80"
      - "6543:443"
    environment:
      - MongoSettings__ConnectionString=mongodb://dev:password@mongo:27017/rawcms
      - PORT=80
      - ASPNETCORE_ENVIRONMENT=Docker
```

Api will be available at http://localhost:3680 (api http://localhost:3580).

You can find documentationa about each docker image on [docker hub](https://hub.docker.com/u/arduosoft).

## Deploy on heroku

1. Create an app, ie. your-demo-rawcms
2. Set the environment variables. See later the variable mapping.
3. Deploy using the heroku cli

**Variables**

```bash
ASPNETCORE_ENVIRONMENT=Docker
ASPNETCORE_SERVER_URLS=http://*:$PORT
MongoSettings__ConnectionString=<your connection string for mongodb service>
GOOGLE_ANALITYCS= <your api key on GA, optional>
```

**deploy**

```bash
heroku container:push web -a your-demo-rawcms
heroku container:release web -a your-demo-rawcms

```

## Deploy using Kubernetes

A simple configuration for Kubernetes can be made using following yaml files

### UI

save this file as rawcms.yml

```yaml
apiVersion: v1
kind: Service
metadata:
  name: rawcms
  labels:
    run: rawcms
spec:
  type: ClusterIP
  ports:
    - port: 80
      targetPort: 80
      protocol: TCP
      name: http
  selector:
    run: rawcms
---
apiVersion: extensions/v1beta1
kind: Deployment
metadata:
  name: rawcms
spec:
  replicas: 1
  template:
    metadata:
      labels:
        run: rawcms
    spec:
      containers:
        - name: rawcms
          image: arduosoft/rawcms-alpha
          imagePullPolicy: Always
          ports:
            - containerPort: 80
          env:
            ## use secret on production
            - name: ASPNETCORE_ENVIRONMENT
              value: Docker
            - name: MongoSettings__ConnectionString
              value: <your connectionstring for mondodb  service>
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
    - host: <my url>
      http:
        paths:
          - backend:
              serviceName: rawcms
              servicePort: 80
            path: /
```

### Deploy it

```bash
kubectl create -f rawcms.yml

kubectl create -f ingress.yml
```

You can create a kubernetes cluster from scratch using Microsoft Azure using [this simple tutorial](https://medium.com/swlh/how-to-deploy-an-asp-net-application-with-kubernetes-3c00c5fa1c6e?source=friends_link&sk=de1e07739413943d6a03f8ae232e5408)

## Manual deployment

If you want you can use the zip packages and deploy them directly. This practice is niether recommended nor supported.
The applications can be deployed as following:

- **IIS** configure IIS web side and use zip package content as resource. Change appsetting.json for configure application
- **Kesterl** run 'dotnet RaWCms.dll' from unziped package
