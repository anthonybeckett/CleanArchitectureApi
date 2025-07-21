using CleanArchitectureApi.Domain.Customers.Entities;
using CleanArchitectureApi.Domain.Customers.Events;
using CleanArchitectureApi.Domain.Customers.ValueObjects;
using CleanArchitectureApi.Domain.Shared.ValueObjects;

namespace CleanArchitectureApi.DomainTests.Test.Customers;

public class DomainCustomerCreate
{
    private Title CreateTitle() => new Title("Test Title");

    private Address CreateAddress() => new Address(
        AddressLine1: "Address Line 1",
        AddressLine2: "Address Line 2",
        Town: "Test Town",
        County: "Test County",
        Postcode: "BB1 2AJ",
        Country: "UK"
    );
    
    [Fact]
    public void Customer_Create_ShouldReturnCustomer_Successfully()
    {
        var title = CreateTitle();
        var address = CreateAddress();

        var customer = Customer.Create(title, address);

        Assert.Equal("Test Title", customer.Title.Value);
        Assert.Equal("Address Line 1", customer.Address.AddressLine1);
        Assert.Equal("Address Line 2", customer.Address.AddressLine2);
        Assert.Equal("Test Town", customer.Address.Town);
        Assert.Equal("Test County", customer.Address.County);
        Assert.Equal("BB1 2AJ", customer.Address.Postcode);
        Assert.Equal("UK", customer.Address.Country);
    }
    
    [Fact]
    public void Customer_Create_ShouldRaiseDomainEvent_Successfully()
    {
        var title = CreateTitle();
        var address = CreateAddress();

        var customer = Customer.Create(title, address);
        var domainEvent = customer.GetDomainEvents().OfType<CustomerCreatedDomainEvent>().SingleOrDefault();

        Assert.Equal(customer.Id, domainEvent?.CustomerId);
    }
}