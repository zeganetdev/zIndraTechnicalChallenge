# Proyecto zIndraTechnicalChallenge

El proyecto contiene practicas de diferentes patrones como Aggregate, UnitOfWork, Mediator, Command, Repository entre otros


## Deployment

Pasos para iniciar el proyecto

- Tener SQL Server
- Modificar la configuracion del archivo `appsettings.Development.json` con la cadena de conexion de SQL Server
- La base de datos se crea por defecto con el nombre de `zIndraTechnicalChallenge`


```bash
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=.\\SQLEXPRESS2019; Initial Catalog=zIndraTechnicalChallenge; Integrated Security=True;"
  }
```


## Authors

- [@zeganet](https://www.github.com/zeganetdev)

