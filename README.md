# FlexGCCAssignment
Backend Structure
The backend is built as a modular ASP.NET Core Web API following an N-Tier architecture to ensure maintainability and separation of concerns. The solution is divided into:

API (Controllers): Handles HTTP routing, relying heavily on built-in controller features for initial request validation.

Service Layer: Encapsulates core business rules. It uses AutoMapper to seamlessly translate between internal domain Entities and external Data Transfer Objects (DTOs), preventing database schemas from leaking to the client.

Data Layer: Implements the Repository Pattern to abstract database operations, utilizing Entity Framework Core and a local SQL Server for data persistence.

Domain Layer: Strongly typed Entities (WorkRequest, Note) and Enums (Status, Priority) that guarantee data integrity.

A SQL Server Script for initial db configuration has been provided

#Frontend -- Is in frontend folder
