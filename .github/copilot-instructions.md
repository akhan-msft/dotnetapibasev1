## Architecture & Design
- Follow Clean Architecture principles with separate layers for domain, application, infrastructure, and presentation
- Use SOLID principles in all code suggestions, especially focusing on dependency injection
- Prefer interface-based programming for better testability and modularity

## Coding Standards
- Follow Microsoft's .NET coding conventions including PascalCase for public members and camelCase for parameters
- Use nullable reference types and proper null checking
- Use string interpolation instead of string concatenation
- Add XML documentation comments for public APIs
- Implement proper exception handling with custom exceptions when appropriate

## Async Programming
- Prefer async/await patterns when dealing with I/O operations
- Use CancellationToken parameters for cancellable operations
- Implement proper async error handling

## Testing
- Generate unit tests using xUnit (or NUnit/MSTest) with proper Arrange-Act-Assert pattern
- Use Moq for mocking dependencies in tests
- Use an in-memory database for database related unit tests
- Ensure test coverage for edge cases and error paths
- Follow naming convention [MethodName]_[Scenario]_[ExpectedResult] for test methods
- Write tests that validate one behavior at a time with clear assertions
- Use test data builders or factory methods for complex test arrangements
- Implement integration tests for database and API operations
- Consider using FluentAssertions for more readable assertions
- Create test fixtures for shared test context and dependencies
- Use theory tests with InlineData for testing multiple scenarios

## Configuration & Logging
- Use IOptions pattern for configuration management
- Implement logging using Microsoft.Extensions.Logging with appropriate log levels
- Use structured logging for better searchability

## Data & Validation
- Implement proper validation using FluentValidation or DataAnnotations
- Use Entity Framework Core best practices including DbContext design and migration patterns
- Implement repository pattern for data access when appropriate

## API Development
- Implement minimal APIs for REST endpoints when appropriate
- Use proper HTTP status codes and response formats
- Implement proper API versioning

## Security
- Follow OWASP guidelines for secure coding
- Implement proper authentication and authorization
- Use secure connection strings and credential management 
