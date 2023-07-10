
## EF Core 6 and Domain-Driven Design by Julie Lerman

- OVERVIEW:
    - Data persistence is important to your application workflow. 
    - This course will teach you how to use Entity Framework Core 6 and 7 effectively to persist data from your DDD designed software.

- EF Core 6 & DDD:
    - EF Core 6. .NET 6. Visual Studio 2022. C# 10.
    - NOTE: CosmosDB provider for the Azure document database.
    - Focus on solving the problems of the domain. Bounded context. A clearly defined boundary. Important to identify.
    - Storing, moving, sharing the data needed. Persistence has nothing to do with domain. Relational databases.
    - Domain models do not naturally align with relational database schemas. Transform rows/columns into DDD.
    - Mapping backing fields: Coding construct to protect properties from accidental misuse.
    - EF Core DbContext versus DDD Bounded Context.
        - DDD: represents a bounded scope of a particular domain model and its language.
        - EF Core: Defines mapping between classes and database schema. Coorsinates a session with a database to query and store data.
            - Tracks state of objects used during a particular instance.

- ANALYZING & PLANNING DOMAIN:
    - Our domain: Book Publishing. Six subdomains:
        - Core subdomain: Makes the business stand out among competition. COnfidential and under our control.
        - Supporting: Not necessarily unique, but wanting to keep control of and keep it close to the chest, as it were.
        - Generic: No secrets here. Same process as competitors.
            1. Talent & Book Acquisition: Core.
            2. Book Preperation: Supporting.
            3. Publicitity (CRM.): Supporting.
            4. Production: Generic.
            5. Warehouse & Shipping: Generic.
            6. Sales & Accounting: Generic.
        - Identifying bounded contexts. Can match subdomains, but do not have to.
            1. Talent & Book Aquisition: Three (3) bounded contexts:
                1. Book & Author Maintenance.
                2. Talent & Book Discovery.
                3. Contracting.
        - Bounded context relationships:
            1. Co-operation Relationships:
                1. Partnership.
                2. Shared Kernel: Common library. e.g.: Speciliazed "Date" class.
            2. Customer/Supplier Relationships:
                1. Conformist: Supplier is in control.
                2. Anti-Corruption Layer: Use translators.
                3. Open-host Service:
        - Contracting: 
            - Contact: Entity. Tracks the lifetime of a contract. High-level details.
            - Contract Version: Entity. Tracks each iteration of the contract.
            - Contract Spec: Value Object. Immuttable. Defines the collection of particulars of a version.
            - Author: Value Object. Name with ID if previously signed.
            - NOTE Contract: Aggreggate root. Maintains invariants. [(Version), (Specification Set), (Author), (Author)]
            - Any default contract should have contract version with default specs.
            - Apply naming rule for contract numbers.
            - Any change? A new version/revision.
            - Finalization of contract triggers production, publicity, and author/book maintenance.

- EXPLORING THE CONTRACT BOUNDED CONTEXT:
    - [GitHub](https://github.com/julielerman/EFCore6andDDDPluralsight)
    - C# Records for Value Objects? C# Records: Vuilt-in equality based on values. Built-in immutability.
    - Contract responsibilities:
        - Manages original version and revisions.
        - Notes when contract is verbally accepted and finalized.
        - Knows how to generate new contract numbers.
        - As the root, it ensures the aggreggate is in a consistent, valid state.
        - Contract: Are the specs the ssame? Version: Have we been revised?
    - Versions: Protected with the defensive copy.
    - The contract aggreggate, by definition, always includes its versions.
    - NOTE: The persistenmnce logic will need to honor the aggreggate when it retrieves any contract.
    - DDD: Encapsulation. Protecting domain model(s) from side effects. Simplifying the interface.
        - Reduced complexity. Adhering to aggreggate rules and behaviors. Critical unit tests.

- ADDING EF CORE DBCONTEXT:
    - Even though ContractContext has only one DbSet:
        - You can still technically query and add, update, or delete non-root entities:
        ```csharp
            var versions = context.Set<ContractVersion>().ToList();
            context.Add(Entity); // EF Core will detect the entity type.
            context.SaveChanges();
        ```
    - Infrastructure.Data (Set as startup.) And as the default within Package Manager Console.
    ```javascript
        get-dbcontext
    ```
    - EF Core looks for a parameterless constructor or one that matches the scalar properties.
        - e.g.: Cannot bind 'initDate', 'authors', 'workingTitle'
        - SOLUTION: Add a private parameterless constructor without impacting the aggreggate/contract.
        - Various tools rely on parameterless constructors, not just EF Core. e.g.: JSON serialization.
    - EF COre can now build data model.

- TUNING DEFAULT MAPPINGS FOR THE DATA MODEL:
    - DbContext: The data model can be built.
    - "Value objects." EF Core: "Owned Entities."
        - Owned entities masquerade as entities in a data model. 
        - Part of EF Core's mechnasim for handling their change tracking and persistence.
    - Atypical properties recognized by convention:
        - Getter and setter. At least one is public.
        - IEnumerable collections. Public.
        - Enums. With or without value assignments. 
            - EF Core will use enum position as its stored database value *if* not explicit within code.