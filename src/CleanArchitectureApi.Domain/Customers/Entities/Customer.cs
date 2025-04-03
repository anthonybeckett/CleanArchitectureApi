using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Customers.Events;
using CleanArchitectureApi.Domain.Customers.ValueObjects;
using CleanArchitectureApi.Domain.Invoices.Entities;
using CleanArchitectureApi.Domain.Shared.ValueObjects;

namespace CleanArchitectureApi.Domain.Customers.Entities;

public sealed class Customer : BaseEntity
{
    private Customer(Title title, Address address, Balance balance)
    {
        Title = title;
        Address = address;
        Balance = balance;
    }

    private Customer()
    {
        //
    }

    public Title Title { get; private set; }

    public Address Address { get; private set; }

    public Balance Balance { get; private set; }

    public ICollection<Invoice> Invoices { get; private set; }

    public static Customer Create(Title title, Address address)
    {
        var customer = new Customer(title, address, new Balance(0));

        customer.RaiseDomainEvent(new CustomerCreatedDomainEvent(customer.Id));

        return customer;
    }

    public void Update(Title title, Address address)
    {
        Title = title;
        Address = address;
    }

    public void UpdateBalance(decimal invoiceAmount)
    {
        Balance = new Balance(Balance.Value + invoiceAmount);
    }
}