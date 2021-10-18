Feature: Login

@AplicacionCRUD
Scenario: Iniciar Sesion
	Given entro a la pagina de la AplicacionCRUD
	Then Escribo email
	Then Escribo password
	And Submit
	Then Verificar que estoy logeado
	#Then Cerrar navegador
	Then Entro al Index de la AplicacionCRUD
	Then Selecciono el menu Usuarios
	Then Selecciono Agregar Usuario
	Then Relleno la informacion del Usuario
	And Submit Usuario
	Then Verificar que el usuario haya sido creado
	Then Verificar que el usuario haya sido creado2
	Then Eliminar el Usuario Creado
	Then Cerrar el navegador