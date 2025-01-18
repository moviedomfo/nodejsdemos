# Boleteria VIP olecram Daemon

Daemon application that periodically checks for new partner updates on Boleteria Vip and stores the changes in the database. It maintains a pointer to the last update and manages logs of all events.

## Table of Contents

- [Install on-premise](#install)
- [Enviropments](#env)
- [Setting Sheduling](#setting-sheduling)
- [Running manually from code](#deployd-run)
- [Generate deployd](#manually-deployd)

  <a name="install"></a>

## Install using PM2 as service management

    1.  Install PM2 on the on-premise server:

        npm install -g pm2

    2.  Create a new folder to host the server:

        %server_path%

    3.  Copy the 'dist' content inside the previously mentioned folder

         ./dist to   %server_path%/dist

    4.  Copy all filesToRelease folder content to %server_path%

        filesToRelease --> %server_path%

    5. Edit 'serviceStart_pm2.bat' to change the 'cd' command to point to %server_path%.

    6. in  %server_path% path run

       yarn install --production

    7- copy .env file and copy to %server_path%

      .env --> %server_path%/.env

<a name="env"></a>

## Enviropment

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
      <td>APP_CLIENT_NAME</td>
      <td>Socios olecram daemon</td>
      <td>El nombre del cliente de la aplicación.</td>
    </tr>
    <tr>
      <td>APP_BOLETERIA_BASE_URL</td>
      <td>http://192.168.200.6:7076</td>
      <td>La URL base del servicio de boletería.</td>
    </tr>
    <tr>
      <td>APP_FILES</td>
      <td>'logs'</td>
      <td>Directorio donde se almacenan los archivos de logs.</td>
    </tr>
    <tr>
      <td>APP_REPORTS</td>
      <td>'csv'</td>
      <td>Directorio donde se almacenan los reportes csv</td>
    </tr>
    
    <tr>
      <td>APP_LIMITS</td>
      <td>2</td>
      <td>Cantidad de socios a procesar por iteracion del servicio</td>
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
      <td>192.168.2.106</td>
      <td>Dirección IP del servidor de la base de datos. </td>
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
      <td>BD_LOCAL</td>
      <td>false</td>
      <td>Indica si la base de datos está en una máquina local (true) o remota (false).</td>
    </tr>

  </tbody>
</table>

<a name="deployd-run"></a>

## Running manually from code

    - yarn prod  => to generate 'dist' whit builded files and start up the app

<a name="manually-deployd"></a>

## Generate deployd (dist folder)

- yarn build

  This comand will generate dist/bundle.js

<a name="#setting-sheduling"></a>

## Setting Sheduling

### Time Fields \* \* \* \* \*

- **Minute (0 - 59):** Indicates the minute when the command will be executed.
- **Hour (0 - 23):** Indicates the hour of the day when the command will be executed.
- **Day of the month (1 - 31):** Indicates the day of the month when the command will be executed.
- **Month (1 - 12):** Indicates the month of the year when the command will be executed.
- **Day of the week (0 - 7):** Indicates the day of the week when the command will be executed (0 and 7 both represent Sunday).

### Example

    //"*/1 10-17 * * *" | Every minute de 10 AM a 5 PM  (10 a 17)
    //"* 10-17 * * *"   | Every minute de 10 AM a 5 PM   (10 a 17)
    //"*/5 * * * *"     | Every 5 minutes
