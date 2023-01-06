# Introduction

This is a little project I wanted to do to practice using Kubernetes.

The goal is only to have a back-end, a front-end _and a database_ (will do in the future) builded into their own Docker image in a functionning Kubernetes cluster.

The functionnalities are very basic, you can only get the list of todos from the back-end and display them in the app. The goal is only to make a Single Page Application successfully communicate with a back-end in a cluster with an Ingress.

# To launch for Docker

## API

docker run -d -p 5000:80 --name todo-api m-arioux/todo-api

## APP

docker run --name todo-app -d -p 8080:5000 -e API_BASE_URL="http://localhost:5000" m-arioux/todo-app

# To launch in a minikube Kubernetes cluster

## Prerequisites

You will need:

- minikube installed
- Docker
- HyperV (should work for other minikube drivers **EXCEPT Docker on Windows** [because of this](https://github.com/kubernetes/minikube/issues/10245), but you will need to change start.ps1)

If you don't have Powershell installed, you can go into start.ps1 and extract into a shell script only the required stuff.

## Launch

Just execute the `start.ps1` Powershell script. It will create a HyperV minikube cluster, build the apps in their respective docker image and setup the Kubernetes cluster.

Then, get the IP of your cluster with the command `minikube ip` and point these subdomains to that IP in your DNS settings (hosts file on Windows):

- todo-api.local-cluster
- todo-app.local-cluster

If everything is set correctly, go to http://todo-app.local-cluster in your browser and you should see the app with the 2 todos send from the back-end.