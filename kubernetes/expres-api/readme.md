# Boleteria Vip rapi api

    API that allows consume Socios Updates by diferents endpoints .-

## Table of Contents

- [ Install on-premise ](#install)
- [ Run locally ](#run-locally)
- [ Enviropments ](#env)

<a name="install"></a>

## Make deployd without docker

### using PM2 as service management

    1. On the project folder run yarn build to generate dist folder and bounde.js file
    ./dist/boundle.js

    2.  Install PM2 on the on-premise server:
            npm install -g pm2

    3.  Create a new folder to host the service:
          %server_path%

    4.  Copy the 'dist' content inside the previously mentioned folder
        ./dist to   %server_path%/dist

    5. Over this folder right click and open new NodeJs console or powershell
        execute this command:

        yarn install --production

    6. Copy all filesToRelease folder content to %server_path% (not the folder, only it content)

            filesToRelease/*.* --> %server_path%

    7. Edit 'serviceStart_pm2.bat' to change the 'cd' command to point to %server_path%.

    8. Run

        yarn install --production

    9. copy .env file and copy to %server_path%

        .env --> %server_path%/.env

    10. In th Console cd to %server_path% and run:

        pm2 start ecosystem.config.js

    10.1 Type pm2 ls to  check if the process is within the PM2 process list

        The name that is in the "ecosystem.config.js" file should appear

<a name="run-locally"></a>

<a name="env"></a>

## Setting Enviropment

<table>
  <thead>
    <tr>
      <th>Variable</th>
      <th>Valor</th>
      <th>Explicación</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>APP_VERSION</td>
      <td>1.0</td>
      <td>La versión actual de la aplicación.</td>
    </tr>
    <tr>
      <td>APP_CLIENT_NAME</td>
      <td>Socios rapi API</td>
      <td>El nombre del cliente de la aplicación.</td>
    </tr>
    <tr>
      <td>APP_PORT</td>
      <td>3016</td>
      <td>Puerto donde correra la app </td>
    </tr>
    <tr>
      <td>APP_LOGS_PATH</td>
      <td>'./files'</td>
      <td>Directorio donde se almacenan los archivos de logs (de momento no se usa).</td>
    </tr>
    <tr>
      <td>APP_BASE_URL</td>
      <td>http://localhost </td>
      <td>solo a fines de logs . para indicar donde esta alojada la api</td>
    </tr>
    <tr>
      <td>APP_SCHEDULING</td>
      <td>"*/1 * * * *"</td>
      <td>Programación de tareas en formato <a href="#setting-sheduling">cron</a></td>
    </tr>
    <tr>
      <td>BD_PORT</td>
      <td>1433</td>
      <td>Puerto utilizado para la conexión a la base de datos.</td>
    </tr>
    <tr>
      <td>BD_HOST</td>
      <td>localhost</td>
      <td>Dirección IP / nmombre del servidor de la base de datos. </td>
    </tr>
    <tr>
      <td>BD_INSTANCE</td>
      <td>SQLEXPRESS</td>
      <td>Nombre de la instancia de SQL Server.</td>
    </tr>
    <tr>
      <td>BD_USER</td>
      <td>******</td>
      <td>Nombre de usuario</td>
    </tr>
    <tr>
      <td>BD_PWD</td>
      <td>******</td>
      <td>Contraseña para el usuario de la base de datos.</td>
    </tr>
    <tr>
      <td>BD_DATABASE_NAME</td>
      <td>socios_integracion</td>
      <td>Nombre de la base de datos utilizada por la aplicación.</td>
    </tr>
    <tr>
      <td>BD_LOG</td>
      <td>false | true</td>
      <td>Indica si se deben registrar logs de scripts de ejecuciones (true para habilitar, false para deshabilitar).</td>
    </tr>
    <tr>
      <td>BD_LOCAL</td>
      <td>false</td>
      <td>Indica si la base de datos está en una máquina local (true) o remota (false).</td>
    </tr>

  </tbody>
</table>

## Run locally

[1] Clone the repo locally
[2] run -> yarn install
[3] run dev command

    ```
        yarn dev
    ```

[4] Additionally if you have dockerhub installed. We leave you a dockerfil ready!!
pleasse ref to [Dockerize](#dockerize)

## models generation

We have used sequalize-auto to generate all models from dexisting database

1. first of all you must install :

```
  yarn add sequelize-auto
```

2. To generate database run the following cmd

Opt params
-v, --views Include database views in generated models [boolean]
--useDefine Use `sequelize.define` instead of `init` for es6|esm|ts

```
    yarn sequelize-auto -h 100.1.1.1 -d [database] -u [username] -x [pwd] -p 7780  --dialect mssql  -o ./src/infra/db/seq-models -l ts -views
```
