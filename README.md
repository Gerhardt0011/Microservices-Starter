# Microservices Starter Kit

## Building the Project

In the root folder simply run the following to restore and build all the services
```shell
dotnet build
```

## Getting Started with Skaffold

In order to simplify the process of running the project in your development environment we recommend that you use Skaffold, you can download Skaffold here: [Skaffold.dev](https://skaffold.dev/docs/install/)

When making changes to services including the Web service, Skaffold will automatically rebuild and deploy your service in Kubernetes.

Live reload is also available for Web services out of the box.

Apply Ingress Services and Persistent Layers

```shell
kubectl apply -f K8S/Local/Core/
kubectl apply -f K8S/Local/PVC/
kubectl apply -f K8S/Local/Data/
```

Check if ingress controller is running before running skaffold
```shell
kubectl get pods -n ingress-nginx
```

Start Skaffold
```shell
skaffold dev
```

Update hosts file and add host entry.
```shell
127.0.0.1 microservice.test
```
**NB** If you are using Minikube, use your minikube ip instead of 127.0.0.1

## Documentation

**Documentation is currently Work in Progress**

Documentation can be found in The Docs Folder, i'm using vuepress for the documentation Learn more here: [VuePress](https://vuepress.vuejs.org/)