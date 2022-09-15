
Projet d'intégration
====================

Équipe: Vlissides

Bienvenue au repértoire git du projet de bibliothèque Vlissides!

# Lignes de conduite
Dans la section de lignes de conduite, vous allez trouver de l'information concernant comment configurer les outils pour le projet, les standards de git du projet ainsi que le standard de code du projet.

Svp, bien lire avant de commencer à participer au projet. L'index des lignes de conduite ce trouve [ici](org/lignes-conduite/README.md).

### Gestion du temps
Assurez-vous de toujours confirmer le temps que vous passez sur une fonctionnalité/bug fix dans le projet sur azure.

### Communications
Utiliser le groupe [matrix](https://matrix.org) fait pour le projet. Demander plus de clarifications à l'équipe au besoin.

### Notes
Au beosin, ajouter vos notes dans le répertoire de notes sur ce répertoire git.

#### Fichiers de notes
Afin de partager nos notes facilement à travers git et accessibles avec
notre éditeur de texte préféré, on les téléverse dans le repo. git.

- Nom
  - En minuscules
  - Sans accents
  - Séparé par des tirets
- Format
  - markdown [documentation pour Azure](https://docs.microsoft.com/en-us/azure/devops/project/wiki/markdown-guidance?view=azure-devops)

# Légal
Section dédiée aux informations concernant les licences et les déclarations légales.

## Licence du projet
Le projet est licencié sous la licence [WTFPL](www.wtfpl.net).

![logo de la licence](LICENSES/wtfpl-badge.png)

Il est important de bien lire la licence avant de regarder, d'exécuter et de modifier le code du projet. [lire la licence et plus d'informations](LICENSES/README.md)

## Tiers
Voici une liste des ressources tierces utilisée par le projet.

### Dépendances communes
Dépendances générales s'appliquant à tout le projet. Autant au seeder qu'à la bibliothèque.

| Licence | Dépendance |
|-----------|-----------:|
| [MIT](LICENSES/MIT) | dotnet |


### Dépendances du système de bibliothèque
Dépendances nécessaires pour le projet du système de la bibliothèque.

#### Paquets nuget
| Licence | Dépendance | 
|-----------|-----------:|
| [MIT](LICENSES/MIT) | Faker.Net |
| [MIT](LICENSES/MIT) | Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore |
| [MIT](LICENSES/MIT) | Microsoft.AspNetCore.Identity.EntityFrameworkCore |
| [MIT](LICENSES/MIT) | Microsoft.AspNetCore.Identity.UI |
| [MIT](LICENSES/MIT) | Microsoft.EntityFrameworkCore.Tools |

#### Esthétique
Dépendances concernant l'esthétique du projet.

| Licence | Dépendance | 
|-----------|-----------:|
| [MIT](LICENSES/MIT) | bootstrap |
| [PFL](LICENSES/PFL) [WTFPL](LICENSES/WTFPL) [MIT](LICENSES/MIT) [Apache 2.0](LICENSES/Apache2_0) [](LICENSES/) | materialdesignicons |

### Dépendances du seeder
Dépendances nécessaires pour le projet seeder.

| Licence | Dépendance |
|-----------|-----------:|
| [MIT](LICENSES/MIT) | dotnet |
| [MIT](LICENSES/MIT) | NBuilder |
| [MIT](LICENSES/MIT) | Microsoft.EntityFrameworkCore |
| [MIT](LICENSES/MIT) | Microsoft.EntityFrameworkCore.Design |
| [MIT](LICENSES/MIT) | Microsoft.Extensions.Configuration |
| [Apache 2.0](LICENSES/APACHE2_0) | Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore |
*(Paquets nuget)*

### Dépendances de la base de données
Selon la base de données vous voulez utiliser, les dépendances changent d'une technologie de base de donnée à une autre.

#### Paquets nuget
Technologies utilisées pour se conecter à une base de données.

| Licence | Dépendance | Système d'exploitation du serveur | Branche |
|-----------|:----------:|:----------:|-----------:|
| [MIT](LICENSES/MIT) | Microsoft.Data.Sqlite.Core | \*BSD, Linux, Windows | `main-freebsd`, `develop-freebsd` |
| [MIT](LICENSES/MIT) | Pomelo.EntityFrameworkCore.MySql | \*BSD, Linux, Windows | `main-freebsd`, `develop-freebsd` |
| [MIT](LICENSES/MIT) | Microsoft.EntityFrameworkCore.SqlServer | Linux, Windows | `main`, `develop` |

#### Base de données
Technologies utilisées pour servir la base de données.

**NB**: du à des limitations de `dotnet`, vous ne pouvez pas utiliser le seeder sur un autre système d'exploitation BSD que FreeBSD, mais vous pouvez servir la base de donnée sur n'importe quel système BSD.

| Licence | Technologie | Système d'exploitation du serveur | Branche |
|-----------|:----------:|:----------:|-----------:|
| [Domaine public](LICENSES/MIT) | sqlite3 | \*BSD, Linux, Windows | `main-freebsd`, `develop-freebsd` |
| [GPLv3](LICENSES/GPLv3) [LGPLv2.1](LICENSES/LGPLv2_1) | mariadb | \*BSD, Linux, Windows | `main-freebsd`, `develop-freebsd` |
| [Propriétaire](https://www.microsoft.com/en-us/Licensing/product-licensing/sql-server) | mssql | Linux, Windows | `main`, `develop` |

