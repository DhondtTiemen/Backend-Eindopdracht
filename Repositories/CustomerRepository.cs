namespace Eindopdracht.Repositories;

public interface ICustomerRepository
{
    Task<List<Customer>> GetAllCustomers();
    Task<Customer> GetCustomerById(string CustomerId);
    Task<Customer> GetCustomerByMail(string Email);
    Task<Customer> AddCustomer(Customer newCustomer);
    Task<Customer> UpdateCustomer(Customer customer);
    Task<Customer> DeleteCustomer(string customerId);
}

public class CustomerRepository : ICustomerRepository
{
    private readonly IMongoContext _context;
    public CustomerRepository(IMongoContext context)
    {
        _context = context;
    }

    //GET CUSTOMERS
    public async Task<List<Customer>> GetAllCustomers()
    {
        return await _context.CustomerCollection.Find(_ => true).ToListAsync();
    }

    //GET CUSTOMERS BY ID
    public async Task<Customer> GetCustomerById(string CustomerId)
    {
        return await _context.CustomerCollection.Find<Customer>(c => c.CustomerId == CustomerId).FirstOrDefaultAsync();
    }

    //GET CUSTOMER BY EMAIL
    public async Task<Customer> GetCustomerByMail(string Email)
    {
        return await _context.CustomerCollection.Find<Customer>(c => c.Email == Email).FirstOrDefaultAsync();
    }

    //ADD CUSTOMER
    public async Task<Customer> AddCustomer(Customer newCustomer)
    {
        try
        {
            await _context.CustomerCollection.InsertOneAsync(newCustomer);
            return newCustomer;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    //UPDATE CUSTOMER
    public async Task<Customer> UpdateCustomer(Customer customer)
    {
        try
        {
            var filter = Builders<Customer>.Filter.Eq("CustomerId", customer.CustomerId);
            var update = Builders<Customer>.Update.Set("Name", customer.Name);
            update = Builders<Customer>.Update.Set("Email", customer.Email);
            var result = await _context.CustomerCollection.UpdateOneAsync(filter, update);
            return await GetCustomerById(customer.CustomerId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    //DELETE CUSTOMER
    public async Task<Customer> DeleteCustomer(string customerId)
    {
        try
        {
            var filter = Builders<Customer>.Filter.Eq("CustomerId", customerId);
            var result = await _context.CustomerCollection.DeleteOneAsync(filter);
            return await GetCustomerById(customerId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}