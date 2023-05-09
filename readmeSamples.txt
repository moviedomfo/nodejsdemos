error

Permite manejo de errores del lado de clientes con un HttpInterseptor
Basicamente maneja el worwflow de llamar al front de Login si no existe el jwt y asi obtenerlo
Tambien genera un jwt a partiro del Refresh Token hasta que este expire definitivamente y todo 
reinicie desde la pantalla de login

Backend:  celam-api (.net core) https://github.com/moviedomfo/celam_api

scaffolding
  app
	common
	  errorDialor
	  errors
		global-error-handler
		error-handler.module

		HttpInterseptor
		LoadingDialogService : front end component, solo muestra un dialog
	 