# Épicas

## Gestión de Clientes
Permitir el registro, consulta, actualización y eliminación de clientes.

Gestión de Pedidos
Permitir crear, modificar y listar pedidos asociados a clientes.

Gestión de Detalles de Pedido
Permitir registrar y consultar los productos dentro de un pedido.

Configuración de la Arquitectura DDD y Base de Datos
Implementar la estructura de capas y configurar la persistencia con Entity Framework Core.

Historias de Usuario

EP1 – Gestión de Clientes

HU1: Como administrador quiero registrar un cliente con nombre y correo para mantener su información.

HU2: Como administrador quiero listar todos los clientes para visualizarlos en el sistema.

HU3: Como administrador quiero actualizar los datos de un cliente para mantener la información actualizada.

HU4: Como administrador quiero eliminar un cliente para depurar información obsoleta.

EP2 – Gestión de Pedidos

HU5: Como administrador quiero registrar un pedido asociado a un cliente para controlar sus compras.

HU6: Como administrador quiero actualizar el estado de un pedido para reflejar su progreso.

HU7: Como administrador quiero consultar los pedidos de un cliente para conocer su historial.

EP3 – Gestión de Detalles de Pedido

HU8: Como administrador quiero agregar productos, cantidades y precios a un pedido para calcular el total.

HU9: Como administrador quiero visualizar los detalles de un pedido para verificar la información.

EP4 – Configuración Técnica

HU10: Como desarrollador quiero definir las capas del sistema para mantener una arquitectura limpia.

HU11: Como desarrollador quiero configurar la base de datos y las migraciones para persistir los datos.

HU12: Como desarrollador quiero exponer endpoints RESTful para interactuar con los datos mediante Postman.

División del Trabajo (5 Miembros)
Rol / Miembro	Responsabilidad principal	Entregables
Dev 1 – Arquitectura / Infraestructura	Crear estructura DDD, configurar EF Core, DbContext, repositorios.	Capas Domain y Infrastructure, migraciones, appsettings.
Dev 2 – Backend Clientes	Implementar servicios y controladores para clientes.	CustomerService, CustomerController, pruebas en Postman.
Dev 3 – Backend Pedidos	Implementar lógica de pedidos (crear, listar, actualizar estado).	OrderService, OrderController.
Dev 4 – Backend Detalles de Pedido	Implementar lógica de OrderDetail y cálculo de totales.	OrderDetailService, endpoints.
Dev 5 – QA / Documentación / Integración	Integrar endpoints, probar con Postman, documentar API y flujo.	Documento de pruebas, README, colección Postman.
Tablero Kanban sugerido

Columnas:

Backlog

To Do

In Progress

In Review

Done

Ejemplo de tarjetas iniciales:

Crear entidades base (Customer, Order, OrderDetail)

Configurar DbContext y repositorios

Crear endpoints POST /customers, GET /orders

Probar endpoints y documentar respuestas

Implementar actualización de estado de pedido