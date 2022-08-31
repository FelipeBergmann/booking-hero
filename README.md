
# hotel-booking

Hotel booking is a challenge to develop a booking api for the very last hotel.


## Customer requirements

- no downtime: 99.99 to 100% availabilyt
- Any stay can't be longer than 3 days and can't be reserved more than 30 days in advance
- All reservations start at least the next day of booking
- To simplify: DAY starts from 00:00 to 23:59:59
- Every end-user can check the room availability, place a reservation, cancel it or modify it.
- To simplify the API is insecure.
## Architecture and Desing code

Even though the last available room presents a strong competition to be booked, we have to consider the project deadline. Maybe, the best architecture to be chosen is a microservice that includes besides an api gateway, an microservice queue manager, to delivery preference numbers for end-users guarantee their bookings and other microservices to allow scalate in any direction and improve the services availiability. It could be combined with a CQRS pattern taking the advantage from the best of SQL databases to write bookings and NOSql databases to read the booking availiability.
Although it seems to be the best solution it wouldn't be possible to implement on the proposed time of the project. So, I chose a very simple architecture that represents a single API targeting a first delivery, like any agile project.
To make it possible migrating the current project domain to a more appropriated architecture, I chose to follow some patterns:

### Repository pattern
Although the project data access layer is using EF Core, the repository pattern brings more flexibility for testing and implement a CQRS architecture in a next version

### Interactor / Clean Architecture / Commands
Implements for Clean Architecture takes some additional time, but it keeps the boundary clean and it will allow to separate our commands and queries in interactions throught commands.



## Tech Stack

**Front-end:** no front end

**Back-end:** .NET 6, Sql Server


## Frameworks, Packs and Libs
For the conclusion of this challenge it was used:
ASPNEt Core, OpenAPI Doc (Swagger), Entity Framework Core and NUnit

Special thanks for [DateOnlyTimeOnly.AspNet.Swashbuckle](https://www.nuget.org/packages/DateOnlyTimeOnly.AspNet.Swashbuckle/) that adds support for recieving and returning DateOnly/TimeOnly as ISO 8601 string to ASP.NET API

## Features

* Room:
 * Creates a Room
 * List all rooms
 * Gets a Room
* Reservation:
 * Availability Room
 * Gets reservation (by id or reservation code)
 * Reserve a Room
 * Change a reservation
 * Cancel a reservation




## Run Requirements
* .NET 6
* SQL Server 
* Visual Studio 2022
## Tests
There are 2 types of tests:
* DbTests: tests repositories writing and reading
* Use case tests: Tests use cases funcionality creating mocks for repositories

To run DbTests it's necessary to run migrations first:

Package Manager Console:
```
Update-Database
```
dotnet cli:
```
dotnet ef database update
```

For use case tests it's not necessary run migrations first.
## Roadmap

### Initializing the project
Just set BookingHero.Booking.API as startup project and run the migrations

If everything is well Visual Studio will open a new browser on API Swagger documentation page.

1. Create a room: POST /api/v1/room
2. Check room availability throught: GET /api/v1/reservation/{roomId}/availability
3. If it's available, create a reservation: POST api/v1/reservation/{roomId}/reservation
4. Get your reservation: 
4.1. if you have the reservationId: /api/v1/reservation/{roomId}/reservation/{reservationId}
4.2. if you have the reservation code: /api/v1/reservation/{roomId}/reservation/code/{reservationCode}
5. Change your reseravtion: PUT /api/v1/reservation/{roomId}/reservation/{reservationCode}
6. Cancel your reservation: DELETE /api/v1/reservation/{roomId}/reservation/{reservationCode}
