# api

API of ceviduernten.ch

Cevi Duernten has different applications and client in place who all consume different data. The api is responsible to
ship that data from different systems to the clients and ensure the business logic for it.

## Swagger Documation

[Swagger Documentation](https://api.ceviduernten.ch/swagger)

## Inital dev setup

- Install postgres db on your pc or use one on a server
- Add custom appsettings.Development.json for local programming

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  },
  "GeneralSettings": {
    "EventsCalendar": "Id_of_Nextcloud_Calendar",
    "ServiceHash": "Some Random Hash",
    "GroupName": "Name of your cevi group, for example Cevi Trüllikon",
    "ReservationMail": "mail for reservations"
  },
  "MailSettings": {
    "Host": "mailserver_host",
    "HostUsername": "username_for_mailaccount",
    "HostPassword": "password_for_mailaccount",
    "HostPort": 587,
    "SenderAddress": "sender address for mails"
  },
  "ConnectionStrings": {
    "Database": "connection_string_to_psql_database"
  },
  "NextcloudInterfaceSettings": {
    "Host": "nextcloud_host",
    "BaseUrl": "/remote.php/dav/public-calendars/",
    "Parameters": "?export&accept=jcal"
  }
}

```

- Start to develop with Visual Studio, Visual Studio Code or Rider (Jetbrains)

## Setup on the production

- Install docker, if it's not already installed
- Install docker-compose, if it's not already installed
- Setup custom docker-compose.yml on the server
- Start the docker container
- Test with one client (web) if the connection is correctly working
