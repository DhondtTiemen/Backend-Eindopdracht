namespace Eindopdracht.Repositories;

public interface IOrderRepository
{
    Task<List<Order>> GetAllOrders();
    Task<Order> GetOrderById(string OrderId);
    Task<Order> GetOrderByCustomerId(string CustomerId);
    Task<Order> GetOrderByCustomerEmail(string Email);
    Task<Order> AddOrder(Order newOrder);
    Task<Order> UpdateOrder(Order order);
    Task<Order> DeleteOrder(string OrderId);
}

public class OrderRepository : IOrderRepository
{
    private readonly IMongoContext _context;
    public OrderRepository(IMongoContext context)
    {
        _context = context;
    }

    //GET ORDERS
    public async Task<List<Order>> GetAllOrders()
    {
        return await _context.OrderCollection.Find(_ => true).ToListAsync();
    }

    //GET ORDERS BY ID
    public async Task<Order> GetOrderById(string OrderId)
    {
        return await _context.OrderCollection.Find<Order>(o => o.OrderId == OrderId).FirstOrDefaultAsync();
    }

    //GET ORDERS BY CUSTOMER ID
    public async Task<Order> GetOrderByCustomerId(string CustomerId)
    {
        return await _context.OrderCollection.Find<Order>(o => o.Customer.CustomerId == CustomerId).FirstOrDefaultAsync();
    }

    //GET ORDERS BY EMAIL
    public async Task<Order> GetOrderByCustomerEmail(string Email)
    {
        return await _context.OrderCollection.Find<Order>(o => o.Customer.Email == Email).FirstOrDefaultAsync();
    }

    //ADD ORDER
    public async Task<Order> AddOrder(Order newOrder)
    {
        await _context.OrderCollection.InsertOneAsync(newOrder);
        return newOrder;
    }

    //UPDATE ORDER
    public async Task<Order> UpdateOrder(Order order)
    {
        try
        {
            var filter = Builders<Order>.Filter.Eq("OrderId", order.OrderId);
            var update = Builders<Order>.Update.Set("Set", order.Set.SetNumber);
            var result = await _context.OrderCollection.UpdateOneAsync(filter, update);
            return await GetOrderById(order.OrderId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    //DELETE ORDER
    public async Task<Order> DeleteOrder(string OrderId)
    {
        try
        {
            var filter = Builders<Order>.Filter.Eq("OrderId", OrderId);
            var result = await _context.OrderCollection.DeleteOneAsync(filter);
            return await GetOrderById(OrderId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}