# DocScanFilter

## 🧾 Overview
DocScanFilter is a C# application designed to parse and validate identity documents from scanned data. It applies business rules to discard invalid or duplicate records. The application follows **Clean Architecture** and **DDD** principles, making the solution modular and testable.

## 📊 Solution Architecture Diagram

```mermaid
graph TB
    subgraph "ConsoleApp Layer"
        Program[Program.cs<br/>Entry Point]
    end
    
    subgraph "Application Layer"
        DocService[DocumentService<br/>Orchestrates validation]
    end
    
    subgraph "Domain Layer - Core Business Logic"
        Entity[Document Entity<br/>Domain Model]
        Validator[DocumentValidator<br/>Business Rules]
        IParser[IDocumentParser<br/>Interface]
        Enum[DocumentType Enum<br/>P, DL, ID]
    end
    
    subgraph "Infrastructure Layer"
        Parser[DocumentParser<br/>CSV Parsing Implementation]
    end
    
    Input[(sample_input.txt<br/>CSV Data)]
    Output[Console Output<br/>Discarded IDs]
    
    Input --> Program
    Program --> Parser
    Parser -.implements.-> IParser
    Parser --> Entity
    Program --> DocService
    DocService --> Validator
    Validator --> Entity
    Entity --> Enum
    DocService --> Output
    
    style Program fill:#e1f5ff
    style DocService fill:#fff4e1
    style Entity fill:#e8f5e9
    style Validator fill:#e8f5e9
    style Parser fill:#f3e5f5
    style Input fill:#fce4ec
    style Output fill:#fce4ec
```

### Data Flow

```mermaid
sequenceDiagram
    participant User
    participant Program
    participant Parser
    participant Document
    participant DocumentService
    participant Validator
    
    User->>Program: Run Application
    Program->>Program: Read sample_input.txt
    
    loop For each line
        Program->>Parser: Parse(line)
        Parser->>Parser: Split CSV fields
        Parser->>Document: Create Document Entity
        Document-->>Parser: Validated Document
        Parser-->>Program: Document
    end
    
    Program->>DocumentService: GetDiscardedScanIds(documents)
    
    loop For each document
        DocumentService->>Validator: IsValid(document)
        Validator->>Validator: Check Nationality
        Validator->>Validator: Check Issuing Country
        Validator->>Validator: Check Expiry Date
        Validator->>Validator: Check Date Formats
        Validator-->>DocumentService: true/false
        
        alt Document Invalid
            DocumentService->>DocumentService: Add to discarded list
        end
    end
    
    DocumentService-->>Program: Sorted discarded IDs
    Program->>User: Print discarded IDs
```

### Business Rules Validation Flow

```mermaid
flowchart TD
    Start([Document Received]) --> CheckNationality{Nationality in<br/>ESP,FRA,POR,AND,MOR?}
    CheckNationality -->|No| Discard[❌ Discard Document]
    CheckNationality -->|Yes| CheckCountry{Issuing Country in<br/>ESP,FRA,POR,AND,MOR?}
    
    CheckCountry -->|No| Discard
    CheckCountry -->|Yes| CheckExpiry{Expiry Date<br/>Valid & Not Expired?}
    
    CheckExpiry -->|No| Discard
    CheckExpiry -->|Yes| CheckDOB{Date of Birth<br/>Valid Format?}
    
    CheckDOB -->|No| Discard
    CheckDOB -->|Yes| CheckExpiryFormat{Expiry Date<br/>Valid Format?}
    
    CheckExpiryFormat -->|No| Discard
    CheckExpiryFormat -->|Yes| Accept[✅ Accept Document]
    
    Discard --> End([Add ScanId to<br/>Discarded List])
    Accept --> End
    
    style Start fill:#e3f2fd
    style Discard fill:#ffebee
    style Accept fill:#e8f5e9
    style End fill:#f5f5f5
```

## 🏗️ Architecture Structure
```
DocScanFilter/
├── Domain/                   # Core business rules
│   ├── Entities/             # Document.cs
│   ├── Enums/                # DocumentType.cs
│   └── Services/             # DocumentValidator.cs
│
├── Application/             # Orchestration and use case logic
│   └── DocumentService.cs
│
├── Infrastructure/          # Adapters and external implementations
│   └── Parsers/             # DocumentParser.cs (implements IDocumentParser)
│
├── ConsoleApp/              # Entry point for the console app
│   └── Program.cs
│
├── Tests/                   # Unit tests (optional)
│   └── (future test files)
│
├── sample_input.txt         # Input file for testing
├── DocScanFilter.sln        # Solution file
└── README.md                # This file
```

## 🚀 How it works
The app reads input from a file called `sample_input.txt` at the root of the solution.
- The first line must contain an integer N: the number of document records to process.
- The next N lines must contain comma-separated document data in the format:

```
scanId,documentType,issuingCountry,lastName,firstName,documentNumber,nationality,dateOfBirth,dateOfExpiry
```

Example:
```
3
1,P,ESP,PEREZ,JAVIER,10011,ESP,021103,190510
2,DL,FRA,PEREZ,JAVIER,5454,ESP,021103,270510
3,DL,FRA,PEREZ,JAVIER,5454,ESP,021013,280510
```

## 🎯 Business Rules
A document is discarded if:
- It has an invalid date format.
- It is already expired.
- The issuing country or nationality is not among: `ESP`, `FRA`, `POR`, `AND`, `MOR`.
- It's a duplicate of an already scanned document.

## 🧪 Running the App
Make sure you are in the root of the solution and have .NET 8 installed.

```bash
# Build the solution
dotnet build DocScanFilter.sln

# Run the app
dotnet run --project ConsoleApp
```

It will read `sample_input.txt`, process the documents and print discarded IDs in ascending order.

## ✅ Example Output
```
2,3
```

This indicates that documents with ScanIds 2 and 3 were discarded.

---
Feel free to extend this project with logging, database storage, or a web interface!