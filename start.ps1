#Requires -RunAsAdministrator

$ErrorActionPreference = "Stop"

minikube start --memory="5g" --cpus="max" --driver=hyperv
minikube addons enable ingress
minikube -p minikube docker-env --shell powershell | Invoke-Expression

docker build ./todo-api -t m-arioux/todo-api
docker build ./todo-app -t m-arioux/todo-app

kubectl apply -f kubernetes.yaml