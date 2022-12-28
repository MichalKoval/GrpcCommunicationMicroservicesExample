### Microservices: gRPC Communication Example in .NET 6

The repo is a part of a Blog Post: link??? and is showing the example of communication between API gateway and Order/Product microservices where the API gateway fetches information about orders and products from Order and Product microservices by using gRPC and Protobuf files and .NET 6.

#### Project architecture
Api, OrderMicroservice and ProductMicroservice projects are part of the same .NET solution.
OrderMicroservice and ProductMicroservice projects have:
•	In-memory database and Entity Framework (code-first approach) with repositories
•	Proto Services
•	.proto files to define gRPC contract
The Api project exposes simple minimal REST API to retrieve list of product orders composed by calling respective Order and Product microservice gRPC methods.
