namespace Eindopdracht.Services;

public interface ILegoService
{
    Task<Customer> AddCustomer(Customer newCustomer);
    Task<Order> AddOrder(Order newOrder);
    Task<Set> AddSet(Set newSet);
    Task<Theme> AddTheme(Theme newTheme);
    Task<Customer> DeleteCustomer(string customerId);
    Task<Order> DeleteOrder(string OrderId);
    Task<Set> DeleteSet(int setNumber);
    Task<Theme> DeleteTheme(string themeId);
    Task<List<Customer>> GetAllCustomers();
    Task<List<Order>> GetAllOrders();
    Task<List<Set>> GetAllSets();
    Task<List<Theme>> GetAllThemes();
    Task<Customer> GetCustomerById(string CustomerId);
    Task<Customer> GetCustomerByMail(string Email);
    Task<List<Order>> GetOrderByCustomerEmail(string Email);
    Task<List<Order>> GetOrderByCustomerId(string CustomerId);
    Task<Order> GetOrderById(string OrderId);
    Task<Set> GetSet(int number);
    Task<List<Set>> GetSetsByAge(int age);
    Task<List<Set>> GetSetsByPrice(double price);
    Task<List<Set>> GetSetsByTheme(string theme);
    Task<Theme> GetTheme(string themeId);
    Task SetupDummyData();
    Task<Customer> UpdateCustomer(Customer customer);
    Task<Order> UpdateOrder(Order order);
    Task<Set> UpdateSet(Set set);
    Task<Theme> UpdateTheme(Theme theme);
}

public class LegoService : ILegoService
{
    public readonly ISetRepository _setRepository;
    public readonly IThemeRepository _themeRepository;
    public readonly ICustomerRepository _customerRepository;
    public readonly IOrderRepository _orderRepository;

    public LegoService(ISetRepository setRepository, IThemeRepository themeRepository, ICustomerRepository customerRepository, IOrderRepository orderRepository)
    {
        _setRepository = setRepository;
        _themeRepository = themeRepository;
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
    }

    //SET
    public async Task<List<Set>> GetAllSets() => await _setRepository.GetAllSets();
    public async Task<Set> GetSet(int number) => await _setRepository.GetSet(number);
    public async Task<List<Set>> GetSetsByTheme(string theme) => await _setRepository.GetSetsByTheme(theme);
    public async Task<List<Set>> GetSetsByAge(int age) => await _setRepository.GetSetsByAge(age);
    public async Task<List<Set>> GetSetsByPrice(double price) => await _setRepository.GetSetsByPrice(price);
    public async Task<Set> AddSet(Set newSet) => await _setRepository.AddSet(newSet);
    public async Task<Set> UpdateSet(Set set) => await _setRepository.UpdateSet(set);
    public async Task<Set> DeleteSet(int setNumber) => await _setRepository.DeleteSet(setNumber);

    //THEME
    public async Task<List<Theme>> GetAllThemes() => await _themeRepository.GetAllThemes();
    public async Task<Theme> GetTheme(string themeId) => await _themeRepository.GetTheme(themeId);
    public async Task<Theme> AddTheme(Theme newTheme) => await _themeRepository.AddTheme(newTheme);
    public async Task<Theme> UpdateTheme(Theme theme) => await _themeRepository.UpdateTheme(theme);
    public async Task<Theme> DeleteTheme(string themeId) => await _themeRepository.DeleteTheme(themeId);

    //CUSTOMER
    public async Task<List<Customer>> GetAllCustomers() => await _customerRepository.GetAllCustomers();
    public async Task<Customer> GetCustomerById(string CustomerId) => await _customerRepository.GetCustomerById(CustomerId);
    public async Task<Customer> GetCustomerByMail(string Email) => await _customerRepository.GetCustomerByMail(Email);
    public async Task<Customer> AddCustomer(Customer newCustomer) => await _customerRepository.AddCustomer(newCustomer);
    public async Task<Customer> UpdateCustomer(Customer customer) => await _customerRepository.UpdateCustomer(customer);
    public async Task<Customer> DeleteCustomer(string customerId) => await _customerRepository.DeleteCustomer(customerId);

    //ORDER
    public async Task<List<Order>> GetAllOrders() => await _orderRepository.GetAllOrders();
    public async Task<Order> GetOrderById(string OrderId) => await _orderRepository.GetOrderById(OrderId);
    public async Task<List<Order>> GetOrderByCustomerId(string CustomerId) => await _orderRepository.GetOrderByCustomerId(CustomerId);
    public async Task<List<Order>> GetOrderByCustomerEmail(string Email) => await _orderRepository.GetOrderByCustomerEmail(Email);
    public async Task<Order> AddOrder(Order newOrder) => await _orderRepository.AddOrder(newOrder);
    public async Task<Order> UpdateOrder(Order order) => await _orderRepository.UpdateOrder(order);
    public async Task<Order> DeleteOrder(string OrderId) => await _orderRepository.DeleteOrder(OrderId);

    //ADD DUMMY DATA
    public async Task SetupDummyData()
    {
        if (!(await _themeRepository.GetAllThemes()).Any())
        {
            var themes = new List<Theme>()
            {
                new Theme() {
                    Name = "Architecture"
                },
                new Theme() {
                    Name = "Batman"
                },
                new Theme() {
                    Name = "Boost"
                },
                new Theme() {
                    Name = "BrickHeadz"
                },
                new Theme() {
                    Name = "Brick Sketches"
                },
                new Theme() {
                    Name = "City"
                },
            };

            foreach (var theme in themes)
                await _themeRepository.AddTheme(theme);
        }

        if (!(await _setRepository.GetAllSets()).Any())
        {
            var themes = await _themeRepository.GetAllThemes();
            var sets = new List<Set>()
            {
                new Set(){
                    SetNumber = 21056,
                    Name = "Taj Mahal",
                    MinimalAge = 18,
                    Pieces = 2022,
                    Price = 119.99,
                    Theme = themes[0]
                },
                new Set(){
                    SetNumber = 21042,
                    Name = "Statue of Liberty",
                    MinimalAge = 16,
                    Pieces = 1685,
                    Price = 99.99,
                    Theme = themes[0]
                },
            };

            foreach (var set in sets)
                await _setRepository.AddSet(set);
        }
    }
}