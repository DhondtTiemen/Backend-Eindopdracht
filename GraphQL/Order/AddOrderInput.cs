namespace Eindopdracht.GraphQL.Mutations;

public record AddOrderInput(string OrderId, Customer customer, Set set);