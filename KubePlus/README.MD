run the ES and Kibana docker images
- docker-compose -f elasticsearch-docker-compose.yml up
- elasticsearch: localhost:9200
- kibana: localhost:5601

build the application dockwer image(Dockerfile image)
- docker build -t kubeplus .
run the imgage 
- docker run -p 8080:80 kubeplus


