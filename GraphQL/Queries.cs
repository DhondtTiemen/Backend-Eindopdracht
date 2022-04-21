namespace Eindopdracht.GraphQL.Queries;

public class Queries
{
    //SETS
    public async Task<List<Set>> GetSets([Service] ILegoService legoService) => await legoService.GetAllSets();
    public async Task<Set> GetSetsByNumber([Service] ILegoService legoService, int setNumber) => await legoService.GetSetByNumber(setNumber);
    public async Task<List<Set>> GetSetsByTheme([Service] ILegoService legoService, string themeName) => await legoService.GetSetsByTheme(themeName);
    public async Task<List<Set>> GetSetsByAge([Service] ILegoService legoService, int age) => await legoService.GetSetsByAge(age);
    public async Task<List<Set>> GetSetsByPrice([Service] ILegoService legoService, double price) => await legoService.GetSetsByPrice(price);

    //THEMES
    public async Task<List<Theme>> GetThemes([Service] ILegoService legoService) => await legoService.GetAllThemes();
    public async Task<Theme> GetThemeById([Service] ILegoService legoService, string themeId) => await legoService.GetThemeById(themeId);

    //CUSTOMERS
    public async Task<List<Customer>> GetCustomers([Service] ILegoService legoService) => await legoService.GetAllCustomers();
    public async Task<Customer> GetCustomersById([Service] ILegoService legoService, string customerId) => await legoService.GetCustomerById(customerId);
    public async Task<Customer> GetCustomersByEmail([Service] ILegoService legoService, string customerMail) => await legoService.GetCustomerByMail(customerMail);

    //ORDERS
    public async Task<List<Order>> GetOrders([Service] ILegoService legoService) => await legoService.GetAllOrders();
    public async Task<Order> GetOrderById([Service] ILegoService legoService, string orderId) => await legoService.GetOrderById(orderId);
    public async Task<List<Order>> GetOrdersByCustomerId([Service] ILegoService legoService, string customerId) => await legoService.GetOrderByCustomerId(customerId);
    public async Task<List<Order>> GetOrdersByCustomerEmail([Service] ILegoService legoService, string customerEmail) => await legoService.GetOrderByCustomerEmail(customerEmail);

}