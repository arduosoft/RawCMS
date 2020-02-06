## Deply on docker containers
You can start from the base docker-compose. At the moment we have two images, one for the api and one for the ui. This means you have two containers. Both of them must be reachable by the user, so they have to be exposed. The following example creare and set using local address and port mapping. On production you have to change them with public url. You can bind directly the port, even this may be tricky in case you want to use standard ports and the machine is not embedded for this application. Moreover, to enable https and get more control about traffic, it is suggester to run all the containers under a nginx proxy.

'''
example
'''
