# identityserver4.on.docker.with.https
this sample demo for identityserver4 run on docker with self-sign certificate

# dockerfile

  FROM microsoft/aspnetcore:2.0<br/>
  ARG source<br/>
  WORKDIR /app <br/>
  ENV certPassword pass@word </br>

  #Use opnssl to generate a self signed certificate cert.pfx with password $env:certPassword
  RUN openssl genrsa -des3 -passout pass:${certPassword} -out server.key 2048 </br>
  RUN openssl rsa -passin pass:${certPassword} -in server.key -out server.key </br>
  RUN openssl req -sha256 -new -key server.key -out server.csr -subj '/CN=localhost' </br>
  RUN openssl x509 -req -sha256 -days 365 -in server.csr -signkey server.key -out server.crt </br>
  RUN openssl pkcs12 -export -out ./cert.pfx -inkey server.key -in server.crt -certfile server.crt -passout pass:${certPassword} <br/>

  #Expose port 443 for the application.
  EXPOSE 443 <br>
  COPY ${source:-obj/Docker/publish} . <br/>
  ENTRYPOINT ["dotnet", "QuickstartIdentityServer.dll"] <br/>
  
 # References
 <ul><li> <a href="https://github.com/cmendible/dotnetcore.samples/tree/master/aspnet.https.on.docker">cmendible/dotnetcore.samples</a></li>
  <li><a href="https://github.com/IdentityServer/IdentityServer4.Samples/tree/release/Quickstarts/1_ClientCredentials">IdentityServer4.Samples/Quickstarts/1_ClientCredentials</a></li>
  </ul>
