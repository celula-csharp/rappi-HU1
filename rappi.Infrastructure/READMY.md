## DDD ( Domain Driven Design )

# creacion de proyecto pasos

## *Paso 1:  Crear los archivos*

    dotnet new sln -n nombre_aplicacion       -> crear aplicacion 

    dotnet new classlib -n nombre.Api         -> ejecucion del proyecto ( controladores  o endpoints )


    dotnet new classlib -n nombre.Application  -> logica( casos de usos, interfaces, serviciox )

    
    dotnet new classlib -n nombre.Domain      -> Corazon del proyecto define las reglas del negocio 


    dotnet new classlib -n nombre.Infrastructure -> Implemento tecnico ( bases de datos, repositorios, correos, APis


## *Paso 2: Agregarlos a la solucino*

    - dotnet soln add nombre.Api nombre.Application nombre.Domain nombre.Infrastructure

## *Paso 3: Hacer las 3 referencias o 4 toca mirar cual*

    dotnet add nombre.Api               reference     nombre.Application

    dotnet add nombre.Application       reference     nombre.Domain

    dotnet add nombre.Infrastructure    reference     nombre.Application

    
    -Esta en algunos ejemplo se pone en otros no 

    dotnet add nombre.Infrastructre      reference      nombre.Domain 


## *Ejecutar proyecto*

    dotnet run --project nombre.Api




## conexion db
- Base de datos desplegada en **aiven**
- 1 INSTALAR  :: dotnet add package microsoft.EntityFrameworkCore
- 2 INSTALAR  :: dotnet add package microsoft.EntityFrameworkCore.design
-  3 INSTALAR  :: dotnet add package Pomelo.EntityFrameworkCore.Mysql
-  4  INSTALAR  :: dotnet tool install --global dotnet-ef
- Verificar que este importando Entity
- Configuraciones para la variable de entorno y traer la configuracion de la db
- para finalizar necesitamos ejeccutar los siguientes comando para crear la migracion y para crear la db en la base de datos }

## Migracion desde DDD ---> importante

                       *Estar en la RAIZ DEL PROYECTO PARA EJECUTAR*

    - dotnet ef migrations add InitialUpdate --project rappi.Infrastructure --startup-project rappi.Api

    - dotnet ef database update --project rappi.Infrastructure --startup-project rappi.Api



                            *Que significa* 

    - project              Indica dónde está tu AppDbContext y dónde se guardarán las migraciones.
    -startup-project       Indica desde dónde se ejecutará la configuración de EF (el Program.cs que tiene la conexión).


## Configuracion de las FK


- [ForeignKey("Order")]
- public int OrderId { get; set; }
- public virtual Order order { get; set; }

## ver las relaciones
     - dotnet list apiweb.Infrastructure reference     

                Referencias de proyecto
                -----------------------
                ..\apiweb.Application\apiweb.Application.csproj
                ..\apiweb.Domain\apiweb.Domain.csproj

## ver la lista de SLN

    - dotnet sln list                                                                                    
                    Proyectos
                    ---------
                    apiweb.Api\apiweb.Api.csprojsss
                    apiweb.Application\apiweb.Application.csproj
                    apiweb.Domain\apiweb.Domain.csproj
                    apiweb.Infrastructure\apiweb.Infrastructure.csproj
## gitignore:
- dotnet new gitignore