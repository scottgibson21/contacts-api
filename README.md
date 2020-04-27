# ContactsApi
This is a demo of a basic CRUD Web API built on .Net Core and Mongo with support for containerization with Docker.  The repo includes a docker-compose file to easily spin up the and API service and MongoDb database in docker containers.

## Setup
1. Clone the repo
2. Ensure that you have Docker Desktop installed (download [here](https://www.docker.com/products/docker-desktop))
3. ensure that you have Docker Compose installed (download [here](https://docs.docker.com/compose/install/))
4. Open a terminal at the main repo folder level
5. run `docker-compose up`

## Known Issues
```diff
- There are known issues with the docker-compose.yaml file, specifically with connecting to the docker MongoDb instance. This is currently being worked on.  In the meantime you would need to install mongoDb locally, and manually create a MongoDb database named 'ContactsDb'
```

## Troubleshooting

### Helpful Docker Commands

#### Images

##### ***Manually build a docker image*** (details docs [here](https://docs.docker.com/engine/reference/commandline/build/))
Note: you must cd to the repo folder level or where the dockerfile exists  
`docker build . -f .\Dockerfile.txt -t {reponame}:{tage}`
 
##### ***View all local images***    
`docker image list`

##### ***Delete all local images***   
`docker rmi -f $(docker images -a -q)`

#### Containers

##### ***Manually start a container from an image*** (Docker run docs [here](https://docs.docker.com/engine/reference/commandline/run/))
Note: the example below binds the host port 5007 to port 80 of the container   
`docker run -p 5007:80 {reponame}:{tag}`

##### ***Delete all local containers and their volumes***  
`docker rm -vf $(docker ps -a -q)`






