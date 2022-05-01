namespace Eindopdracht.GraphQL.Mutations;

public class Mutation
{
    //SETS
    public async Task<AddSetPayload> AddSet([Service] ILegoService legoService, AddSetInput input, [Service] IValidator<AddSetInput> validator)
    {
        var validationResult = validator.Validate(input);
        if (validationResult.IsValid)
        {
            var newSet = new Set()
            {
                SetNumber = input.number,
                Name = input.name,
                MinimalAge = input.minimalAge,
                Pieces = input.pieces,
                Price = input.price,
                Theme = input.Theme,
            };

            var created = await legoService.AddSet(newSet);
            return new AddSetPayload(created);
        }
        else
        {
            string message = string.Empty;
            foreach (var error in validationResult.Errors)
            {
                message += error.ErrorMessage;
            }
            throw new Exception(message);
        }
    }

    public async Task<AddSetPayload> UpdateSet([Service] ILegoService legoService, AddSetInput input, [Service] IValidator<AddSetInput> validator)
    {
        var validationResult = validator.Validate(input);
        if (validationResult.IsValid)
        {
            var updateSet = new Set()
            {
                SetNumber = input.number,
                Name = input.name,
                MinimalAge = input.minimalAge,
                Pieces = input.pieces,
                Price = input.price,
                Theme = input.Theme,
            };

            var updated = await legoService.UpdateSet(updateSet);
            return new AddSetPayload(updated);
        }
        else
        {
            string message = string.Empty;
            foreach (var error in validationResult.Errors)
            {
                message += error.ErrorMessage;
            }
            throw new Exception(message);
        }
    }

    public async Task<Set> DeleteSet([Service] ILegoService legoService, AddSetInput input)
    {
        return await legoService.DeleteSet(input.number);
    }

    //THEMES
    public async Task<AddThemePayload> AddTheme([Service] ILegoService legoService, AddThemeInput input, [Service] IValidator<AddThemeInput> validator)
    {
        var validationResult = validator.Validate(input);
        if (validationResult.IsValid)
        {
            var newTheme = new Theme()
            {
                ThemeId = input.themeId,
                Name = input.name
            };

            var created = await legoService.AddTheme(newTheme);
            return new AddThemePayload(created);
        }
        else
        {
            string message = string.Empty;
            foreach (var error in validationResult.Errors)
            {
                message += error.ErrorMessage;
            }
            throw new Exception(message);
        }
    }

    public async Task<AddThemePayload> UpdateTheme([Service] ILegoService legoService, AddThemeInput input, [Service] IValidator<AddThemeInput> validator)
    {
        var validationResult = validator.Validate(input);
        if (validationResult.IsValid)
        {
            var updateTheme = new Theme()
            {
                ThemeId = input.themeId,
                Name = input.name
            };

            var updated = await legoService.UpdateTheme(updateTheme);
            return new AddThemePayload(updated);
        }
        else
        {
            string message = string.Empty;
            foreach (var error in validationResult.Errors)
            {
                message += error.ErrorMessage;
            }
            throw new Exception(message);
        }
    }

    public async Task<Theme> DeleteTheme([Service] ILegoService legoService, AddThemeInput input)
    {
        return await legoService.DeleteTheme(input.themeId);
    }

    //CUSTOMERS
    public async Task<AddCustomerPayload> AddCustomer([Service] ILegoService legoService, AddCustomerInput input, [Service] IValidator<AddCustomerInput> validator)
    {
        var validationResult = validator.Validate(input);
        if (validationResult.IsValid)
        {
            var newCustomer = new Customer()
            {
                Name = input.Name,
                Email = input.Email
            };

            var created = await legoService.AddCustomer(newCustomer);
            return new AddCustomerPayload(created);
        }
        else
        {
            string message = string.Empty;
            foreach (var error in validationResult.Errors)
            {
                message += error.ErrorMessage;
            }
            throw new Exception(message);
        }
    }

    public async Task<AddCustomerPayload> UpdateCustomer([Service] ILegoService legoService, AddCustomerInput input, [Service] IValidator<AddCustomerInput> validator)
    {
        var validationResult = validator.Validate(input);
        if (validationResult.IsValid)
        {
            var updateCustomer = new Customer()
            {
                CustomerId = input.CustomerId,
                Name = input.Name,
                Email = input.Email
            };

            var updated = await legoService.UpdateCustomer(updateCustomer);
            return new AddCustomerPayload(updated);
        }
        else
        {
            string message = string.Empty;
            foreach (var error in validationResult.Errors)
            {
                message += error.ErrorMessage;
            }
            throw new Exception(message);
        }
    }

    public async Task<Customer> DeleteCustomer([Service] ILegoService legoService, AddCustomerInput input)
    {
        return await legoService.DeleteCustomer(input.CustomerId);
    }

    //ORDERS
    public async Task<AddOrderPayload> AddOrder([Service] ILegoService legoService, AddOrderInput input, [Service] IValidator<AddOrderInput> validator)
    {
        var validationResult = validator.Validate(input);
        if (validationResult.IsValid)
        {
            var newOrder = new Order()
            {
                Customer = input.customer,
                Set = input.set
            };

            var created = await legoService.AddOrder(newOrder);
            return new AddOrderPayload(created);
        }
        else
        {
            string message = string.Empty;
            foreach (var error in validationResult.Errors)
            {
                message += error.ErrorMessage;
            }
            throw new Exception(message);
        }
    }

    public async Task<AddOrderPayload> UpdateOrder([Service] ILegoService legoService, AddOrderInput input, [Service] IValidator<AddOrderInput> validator)
    {
        var validationResult = validator.Validate(input);
        if (validationResult.IsValid)
        {
            var updateOrder = new Order()
            {
                OrderId = input.OrderId,
                Customer = input.customer,
                Set = input.set
            };

            var updated = await legoService.UpdateOrder(updateOrder);
            return new AddOrderPayload(updated);
        }
        else
        {
            string message = string.Empty;
            foreach (var error in validationResult.Errors)
            {
                message += error.ErrorMessage;
            }
            throw new Exception(message);
        }
    }
}