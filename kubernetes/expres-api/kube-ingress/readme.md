## rapi-api-ingress.yaml

kubectl apply -f rapi-api-ingress.yaml
📌 Explicación del Ingress:

    Define el host rapi.local (podés modificarlo o agregarlo en /etc/hosts).
    Redirige /cliente1 al servicio rapi-api-cliente1-service.
    Redirige /cliente2 al servicio rapi-api-cliente2-service.
    
📌 Después de hacer estos pasos, probá nuevamente en el navegador o con curl:
ping rapi.local

curl http://rapi.local/cliente1
curl http://rapi.local/cliente2



3️⃣ Verificá que esté creado correctamente:

  kubectl get ingress

📌 Si ADDRESS está vacío, significa que el Ingress Controller no está corriendo.

Revisar si el controlador de Ingress está instalado, asegurate de que nginx-ingress está corriendo:

   kubectl get pods -n kube-system | grep nginx

    Debería salir algo como:

      ingress-nginx-controller-xxxxx   1/1   Running   0     5m
📌 Si no aparece nada, instalalo con:

  kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/main/deploy/static/provider/cloud/deploy.yaml

4️⃣  Revisar si las rutas están bien configuradas
  
    kubectl describe ingress rapi-api-ingress

6️⃣ Revisar los logs del Ingress
    
    kubectl logs -n ingress-nginx -l app.kubernetes.io/name=ingress-nginx


1️⃣ Revisar si el Ingress Controller está corriendo:

    kubectl get pods -n ingress-nginx

    Si el controlador está bien, debería aparecer algo como:
      NAME                                       READY   STATUS      RESTARTS   AGE
      ingress-nginx-admission-create-4rrsd       0/1     Completed   0          11m
      ingress-nginx-admission-patch-dt69b        0/1     Completed   1          11m
      ingress-nginx-controller-cbb88bdbc-dvjdn   1/1     Running     0          11m

2️⃣ Revisar los servicios del Ingress Controller
    
    Si todo está bien, deberías ver algo como:
      kubectl get svc -n ingress-nginx
      NAME                                 TYPE           CLUSTER-IP      EXTERNAL-IP   PORT(S)                      AGE
      ingress-nginx-controller             LoadBalancer   10.109.170.98   localhost     80:32636/TCP,443:31978/TCP   12m
      ingress-nginx-controller-admission   ClusterIP      10.103.91.105   <none>        443/TCP                      12m


    🔧 Solución si EXTERNAL-IP está vacío
    Si EXTERNAL-IP sigue vacío o <pending>, podemos usar un port-forward temporalmente para probar el acceso manualmente:

    kubectl port-forward --namespace ingress-nginx service/ingress-nginx-controller 8080:80



kubectl exec -it rapi-api-cliente1-dep-858498488d-2p28s  -- curl -s http://rapi-api-cliente1-service/health