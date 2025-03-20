using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

public class CustomerService : ICustomerService
{
    private readonly AppDbContext _context;

    public CustomerService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await _context.Customers.ToListAsync();
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        return await _context.Customers.FindAsync(id);
    }

    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task<List<Customer?>> SearchCustomersByNameAsync(string name)
    {
        return await _context.Customers
            .Where(c => EF.Functions.Like(c.CustomerName, $"%{name}%"))
            .ToListAsync();
    }

    public async Task<List<Customer>> GetCustomersByPostalCodeAsync(string postalCode)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
            throw new ArgumentException("Postal code cannot be null or empty.", nameof(postalCode));

        var customers = new List<Customer>();

        using (var connection = _context.Database.GetDbConnection())
        {
            await connection.OpenAsync();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "GetCustomersByPostalCode";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var postalCodeParam = new SqlParameter("@PostalCode", postalCode);
                command.Parameters.Add(postalCodeParam);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        customers.Add(new Customer
                        {
                            CustomerId = reader.GetInt32(0),
                            CustomerName = reader.GetString(1),
                            // Map other fields as necessary
                        });
                    }
                }
            }
        }

        return customers;
    }
}
