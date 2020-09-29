1.	Requisitos mínimos del hardware que ocupamos. 
	https://docs.microsoft.com/en-us/visualstudio/releases/2019/system-requirements
	
2.	Node.js
	https://nodejs.org/en/download/
	Instalar la última versión LTS (Long Term Support).	
	Se puede comprobar la versión por medio de la línea de comandos con: node --version.
	
3.	NPM actualizado
	Ese se incluye con parte del Nodejs, pero necesitamos la última versión.
	Se puede comprobar la versión por medio de la línea de comandos con: npm --version.
	Para actualizarlo se ejecuta desde la línea de comandos y ejecutando como administrador: npm i npm -g.
	
4.	Última versión del Microsoft .NET Core SDK
	https://www.microsoft.com/net/download/windows,  el de 64bits aquí, 
	https://dotnet.microsoft.com/download/thank-you/dotnet-sdk-2.2.402-windows-x64-installer
	
	Utilizar el Windows Installer de acuerdo con la versión de Windows que se esté utilizando.
	Se hace efectuar una ejecución inicial para descargar los paquetes iniciales. 
	Estos pasos crean una pequeña aplicación de línea de comandos que imprime la palabra “Hello World” en la consola. 
	
	Ejecutar desde la línea de comandos: 
	
	mkdir t1
	cd t1
	dotnet new console 

	En este paso puede aparecer un mensaje que señala que se están descargando los paquetes iniciales de .NET Core. 
	Esperar a que se complete la descarga.
		
	dotnet build
	dotnet run

5.	Última versión del .NET Core Hosting Bundle 
	https://dotnet.microsoft.com/download/thank-you/dotnet-runtime-2.2.7-windows-hosting-bundle-installer

6.	Microsoft Visual Studio Code 
	https://code.visualstudio.com/
	Instalar o actualizar a la última versión.
	
7.	Microsoft SQL Server 2008 R2 o superior. 
	https://www.microsoft.com/en-us/sql-server/sql-server-downloads
	Se acostumbra a utilizar la edición Express, en SQL Server 2017 para desarrollo es posible utilizar la edición Developer.	
	
8.	Microsoft Visual Studio 2019 (edición Community o superior) 
	https://www.visualstudio.com/downloads/
	https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2019#install-workloads
	
	Aquí se documenta como obtener los instaladores para la instalación local, 
	https://docs.microsoft.com/en-us/visualstudio/install/create-a-network-installation-of-visual-studio?view=vs-2019, 
	esto baja todos los “workloads” pero al momento de instalar no se deben de instalar todos.
	
	Si el Visual Studio 2019 ya se encuentra instalado se puede utilizar el Visual Studio Installer, 
	para efectuar la actualización.

	Se deben instalar al menos los “workloads”: 
	- Windows 
		+ .NET desktop development
	- Web & Cloud 
		+ ASP.NET and web development
		+ Data storage and processing
	- Other Toolsets 
		+ .NET Core cross-platform development
		
	En caso de contar con una instalación del Visual Studio 2019, proceder con la actualización a la última versión, 
	y confirmar que se tengan instalados los “workloads” señalados en el punto anterior. Esto se hace ejecutando el 
	Visual Studio Installer, y aplicar en el equipo la actualización cuando aparece el botón “Update”, es solo de 
	aplicarlo y esperar que finalice.
 
	Se puede confirmar el resultado con el “Acerca de” de Visual Studio 2019.
	
9.	Internet Information Services habilitado 
	http://technet.microsoft.com/en-us/library/cc731911.aspx
	
10.	Web Deploy 3.6 
	http://www.iis.net/downloads/microsoft/web-deploy.  El enlace del instalador se encuentra en la parte inferior 
	de la página.
	
11.	Navegadores Web actualizados a la última versión. 
	https://www.mozilla.org/en-US/firefox/
	https://www.google.com/chrome/index.html

12. Postman
	https://www.getpostman.com/apps	
	
De ser posible efectuar la instalación de las versiones del software con el idioma inglés, para unificar con la 
configuración utilizada por el profesor.

-----------------------------------------------------------------------------------------------
Opcionales

0.	TypeScript actualizado
	Se puede comprobar la versión por medio de la línea de comandos con: tsc --version.
	Para actualizarlo se ejecuta desde la línea de comandos y ejecutando como administrador: npm i typescript -g.	
	
0.	Yarn actualizado
	https://yarnpkg.com/en/docs/install#windows-stable
	Para verificar que la instalación es efectiva se debe ejecutar desde línea de comandos: yarn -–version.	

0.	Webpack actualizado 
	Para actualizarlo se ejecuta desde la línea de comandos y ejecutando como administrador: npm i webpack -g.
		
0.	Angular CLI actualizado
	Se debe ejecutar desde línea de comandos y con derechos de administración: npm i -g @angular/cli
	Para confirmar la correcta instalación se ejecuta desde línea de comandos: ng -–version
	
0.	Soporte de TypeScript para Visual Studio 2019
	https://www.typescriptlang.org/index.html#download-links
	Se debe utilizar el enlace de instalación relacionado con Visual Studio 2019.	
