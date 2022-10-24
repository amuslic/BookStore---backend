namespace BookStore.Domain;

public enum OperationResult
{
    UnknownError = 0,
    Succeeded = 1,
    ValidationError = 2,
    EnqueuingError = 3,
    NotFound = 4,
}
