### Microservices: gRPC Communication Example in .NET 6

The repo is a part of the Blog Post: link??? and shows the example of communication between API gateway and Order/Product microservices where the API gateway fetches information about orders and products from Order and Product microservices by using gRPC and Protobuf files.

#### Project architecture

Api, OrderMicroservice and ProductMicroservice projects are part of the same .NET solution. OrderMicroservice and ProductMicroservice projects have:
- In-memory database and Entity Framework (code-first approach) with repositories,
- Proto Services,
- .proto files to define gRPC contracts,
- AutoMapper used to map gRPC and entity DTOs.

The Api project exposes simple minimal REST API to retrieve list of product orders composed by calling respective Order and Product microservice gRPC methods.

#### Running the solution (Rider)
The solution is divided into 3 projects where each should be running its own instance.
1. Navigate to the Run/Debug Configuration,
2. Create Compound configuration and add the projects in this order (API gateway should be running first):
   - Api,
   - OrderMicroservice,
   - ProductMicroservice,
3. Run/Debug the Compound.