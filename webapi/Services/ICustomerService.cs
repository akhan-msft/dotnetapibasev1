/// <summary>
/// Interface for customer-related operations.
/// </summary>
public interface ICustomerService
{
    /// <summary>
    /// Retrieves all customers asynchronously.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of customers.</returns>
    Task<List<Customer>> GetAllCustomersAsync();

    /// <summary>
    /// Retrieves a customer by their unique identifier asynchronously.
    /// </summary>
    /// <param name="id">The unique identifier of the customer.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the customer if found; otherwise, null.</returns>
    Task<Customer?> GetCustomerByIdAsync(int id);

    /// <summary>
    /// Creates a new customer asynchronously.
    /// </summary>
    /// <param name="customer">The customer object to create.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the created customer.</returns>
    Task<Customer> CreateCustomerAsync(Customer customer);

    /// <summary>
    /// Searches for customers by their name asynchronously.
    /// </summary>
    /// <param name="name">The name to search for.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of customers matching the name, which may include null entries.</returns>
    Task<List<Customer?>> SearchCustomersByNameAsync(string name);

    /// <summary>
    /// Retrieves customers by their postal code asynchronously.
    /// </summary>
    /// <param name="postalCode">The postal code to filter customers by.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a list of customers matching the postal code.</returns>
    Task<List<Customer>> GetCustomersByPostalCodeAsync(string postalCode);
}