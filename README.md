Asegurarse de usar SQL Server
Para la creación de la base de datos puede usar las migraciones o si prefiere se ha proporcionado el archivo AuthApplicationDB.sql para la creación de la base de datos

Configurar la Cadena de Conexión
Asegurarse de configurar la cadena de conexión. Acceder al archivo de configuración appsettings.json ubicado en la raíz del proyecto. Aquí deberá definir la cadena de conexión a su base de datos SQL Server, reemplazando los valores de servidor, base de datos, usuario y contraseña según su entorno.

Aplicar Migraciones y Crear la Base de Datos
Se usa Base de Datos SQL Server.
Una vez configurada la cadena de conexión, aplique las migraciones de Entity Framework para crear la estructura de la base de datos, incluyendo la creación de tablas e inserción de datos iniciales. Utilice los comandos de la CLI de .NET para realizar estas acciones.


Controladores
Los Controladores manejan las solicitudes HTTP y determinan qué vistas devolver o qué acciones realizar. 

Modelos
Los Modelos representan las entidades de datos en la aplicación. 

Vistas
Las Vistas son las páginas HTML que se renderizan para el usuario. Utilizan Razor para integrar lógica de servidor con HTML y están organizadas en carpetas correspondientes a los controladores, como Views/Account y Views/Admin.

Servicios
Los Servicios encapsulan la lógica de negocio y facilitan la reutilización de código, entre ellos están registro, autenticación y actualización de perfiles. También se incluye un servicio para el hashing de contraseñas (PasswordHasher).

Migraciones
Las Migraciones gestionan los cambios en el esquema de la base de datos a lo largo del tiempo. Utilizan Entity Framework para crear, actualizar y mantener la estructura de la base de datos de manera coherente con los modelos definidos en el código.

Configuración y Personalización
Layout Principal
El Layout Principal define la estructura común de todas las páginas de la aplicación, incluyendo el encabezado, el menú de navegación y el pie de página. Utiliza Bootstrap para asegurar que la interfaz sea responsiva y estéticamente agradable. El menú de navegación se adapta dinámicamente según el estado de autenticación del usuario.



Hashing de Contraseñas
Las contraseñas se almacenan en la base de datos como hashes, evitando que se guarden en texto plano.

Gestión de Sesiones
La Gestión de Sesiones se maneja mediante el almacenamiento de identificadores de usuario en la sesión. Cuando un usuario inicia sesión, su UserId se guarda en la sesión, permitiendo que la aplicación identifique si el usuario está autenticado en diferentes partes de la interfaz. Al cerrar sesión, la información de la sesión se elimina, revocando el acceso a áreas protegidas.

Uso de la Aplicación
Registro de Usuarios
Los nuevos usuarios pueden registrarse proporcionando su correo electrónico, una contraseña y otros detalles personales como nombre, dirección y fecha de nacimiento. Al registrarse, la contraseña se almacena de forma segura utilizando hashing.

Inicio de Sesión
Los usuarios registrados pueden iniciar sesión proporcionando su correo electrónico y contraseña. La aplicación verifica las credenciales comparando la contraseña ingresada con el hash almacenado en la base de datos. Si las credenciales son correctas y la cuenta está activa, se inicia una sesión para el usuario.

Gestión de Perfil
Una vez autenticado, el usuario puede acceder a su perfil para ver y editar su información personal. Esto incluye actualizar nombres, apellidos, dirección y fecha de nacimiento. La contraseña no se solicita en este proceso para mejorar la experiencia de usuario.

Recuperación de Contraseña
Si un usuario olvida su contraseña, puede utilizar la funcionalidad de Recuperación de Contraseña. Ingresando su correo electrónico, la aplicación envía un enlace de recuperación que permite al usuario restablecer su contraseña de manera segura.

Administración de la Base de Datos de Usuarios
Los administradores tienen acceso a una sección denominada Data Base, donde pueden visualizar el contenido de la tabla Users de la base de datos. Esta sección muestra información detallada de cada usuario, facilitando la gestión y administración de cuentas.

Solución de Problemas
Error de Timeout en la Conexión a la Base de Datos
Descripción del Error:
El error "Connection Timeout Expired" indica que la aplicación no pudo establecer una conexión con el servidor de la base de datos dentro del tiempo esperado.
