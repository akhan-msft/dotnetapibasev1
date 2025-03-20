using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly CustomerService _customerService;

    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCustomer(int id)
    {
        var customer = await _customerService.GetCustomerByIdAsync(id);
        if (customer == null)
            return NotFound();

        return Ok(customer);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
    {
        var createdCustomer = await _customerService.CreateCustomerAsync(customer);
        return CreatedAtAction(nameof(GetCustomers), new { id = createdCustomer.CustomerId }, createdCustomer);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchCustomersByName([FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return BadRequest("Name parameter is required.");

        var customers = await _customerService.SearchCustomersByNameAsync(name);

        if (customers == null || customers.Count == 0)
            return NotFound("No customers found matching the given name pattern.");

        return Ok(customers);
    }

    [HttpGet("postalcode")]
    public async Task<IActionResult> GetCustomersByPostalCode([FromQuery] string postalCode)
    {
        if (string.IsNullOrWhiteSpace(postalCode))
            return BadRequest("Postal code parameter is required.");

        var customers = await _customerService.GetCustomersByPostalCodeAsync(postalCode);

        if (customers == null || customers.Count == 0)
            return NotFound("No customers found for the given postal code.");

        return Ok(customers);
    }
}
