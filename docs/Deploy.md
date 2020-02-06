## Deply on docker containers
You can start from the base docker-compose. At the moment we have two images, one for the api and one for the ui. This means you have two containers. Both of them must be reachable by the user, so they have to be exposed. The following example creare and set using local address and port mapping. On production you have to change them with public url. You can bind directly the port, even this may be tricky in case you want to use standard ports and the machine is not embedded for this application. Moreover, to enable https and get more control about traffic, it is suggester to run all the containers under a nginx proxy.

'''
example
'''

You can find documentationa about each docker image on [docker hub](https://hub.docker.com/u/arduosoft).


## Deploy on heroku
As the CMS is released in two different containers, you need to deploy two different application

### Deploy UI on Heroku
1. Create an app, ie. your-demo-ui
2. Set the environment variables. See later the variable mapping.
3. Deploy using the heroku cli

**Variables**
'''

'''

**deploy**
'''
heroku container:push web -a your-demo-ui
heroku container:release web -a your-demo-ui

'''


### Deploy API on Heroku
1. Create an app, ie. your-demo-ui
2. Set the environment variables. See later the variable mapping.
3. Deploy using the heroku cli

**Variables**
'''

'''

**deploy**
'''
heroku container:push web -a your-demo-ui
heroku container:release web -a your-demo-ui

'''


## Deploy using Kubernetes
A simple configuration for Kubernetes can be made using following yaml files

### UI 
save this file as ui.yml
'''
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
        - name: CONNECTION_STRING
          value: 
'''

## API

save this snippet as api.yml
'''
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
        - name: CONNECTION_STRING
          value: 
      
'''

### Ingress
save this snippet as ingress.yml
'''
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

'''
### Deploy it

'''
kubectl create -f ui.yml

kubectl create -f api.yml

kubectl create -f ingress.yml
'''

You can create a kubernetes cluster from scratch using Microsoft Azure using [this simple tutorial](https://medium.com/swlh/how-to-deploy-an-asp-net-application-with-kubernetes-3c00c5fa1c6e?source=friends_link&sk=de1e07739413943d6a03f8ae232e5408)


## Manual deployment
If you want you can use the zip packages and deploy them directly. This practice is not receommentede and supported.
The two applications can be deployed as following:
- **UI** is a static html web site, can be serverd by nginx or all other web server. All the url must point to index.html with a rewrite rule. You can use the nginx condiguration of our [nginx container as refernence ](https://github.com/arduosoft/RawCMS/blob/master/docker/config/ui/nginx.conf)
- **API** is a regular aspnet core application, and can be run using command 'dotnet RawCMS.dll'. IIS should work as well. It is tested on frmework 2.2 and 2.1
