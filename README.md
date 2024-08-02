# EntityDataAPI

## Overview

EntityDataAPI is an API designed to manage entities, including their addresses, dates, and names. The project is built using ASP.NET Core and leverages Entity Framework Core for data access. The code is structured in a highly modular way to ensure scalability and maintainability.

## Features

- CRUD operations for Entities, Addresses, Dates, and Names.
- Filtering, pagination, and sorting capabilities for Entities.
- Basic retry and backoff strategy for handling transient failures.
- Modular and organized code structure with a focus on maintainability and scalability.

## Implementation Details

### Modularity and Scalability

The codebase is designed to be modular, which means each functionality is separated into its own component or module. This modularity ensures that the code can be easily scaled and maintained. Key components include:

1. **Controllers**: Each entity type (Entity, Address) has its own controller that handles HTTP requests and responses. This separation of concerns makes it easier to manage and extend functionality for each entity type independently.

2. **Repositories**: The repositories handle data access and encapsulate all logic for interacting with the database. Each entity type has its own repository, making it easy to modify or extend data access logic for a specific entity type without affecting others.

3. **DTOs (Data Transfer Objects)**: DTOs are used to define the shape of data being transferred between the client and the server. This abstraction allows the internal data models to change without impacting the API contract with the clients.

4. **Filters**: The filtering logic for retrieving entities is encapsulated in filter classes. This design allows for complex filtering, pagination, and sorting logic to be handled in a reusable and maintainable way.

5. **Utilities**: Utility classes, such as `RetryHelper` for implementing retry and backoff strategies, are used to encapsulate common logic that can be reused across different parts of the application.

### Retry and Backoff Strategy

In this project, a basic retry and backoff strategy was implemented to handle potential transient failures. The `RetryHelper` class provides a mechanism to retry operations that throw an `OperationException`, with an exponential backoff strategy.

#### Rationale

1. **System Stability**: Implementing a retry and backoff strategy helps in maintaining the stability of the system by gracefully handling transient errors which might occur due to temporary network issues, database deadlocks, etc.
2. **User Experience**: By retrying operations that fail due to transient issues, we can reduce the likelihood of presenting errors to the user, thereby improving the user experience.
3. **Nature of Potential Transient Failures**: Transient failures are typically temporary and can often be resolved by retrying the operation after a short delay. The exponential backoff strategy helps in gradually increasing the delay between retries, which prevents overwhelming the system with rapid retries.

### Next Steps

1. **Unit Testing**: Unfortunately, due to time constraints, unit tests were not implemented in this submission. Adding unit tests would be the next step to ensure the correctness and reliability of the code, the modularity of the code will allow this to be easily 
2. **Logging Service**: A logging service (`LoggerService`) was implemented but not yet integrated into the code. Integrating the logging service would help in monitoring and troubleshooting the application by providing detailed logs of operations and errors.
3. **Completion Of Other Controllers And Address**: All the code is there to implement the rest of the controllers, and finish the address, time constraints restricted this.

### API Endpoints

- **Entity**
- GET `/api/entity` - Get all entities with filtering, pagination, and sorting.
- GET `/api/entity/{id}` - Get an entity by ID.
- GET `/api/entity/search` - Search For Entities
- POST `/api/entity` - Create a new entity.
- PUT `/api/entity/{id}` - Update an entity by ID.
- DELETE `/api/entity/{id}` - Delete an entity by ID.

- **Address**
- GET `/api/address` - Get all addresses.
- GET `/api/address/{id}` - Get an address by ID.
- POST `/api/address` - Create a new address.
