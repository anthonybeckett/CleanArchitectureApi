namespace CleanArchitectureApi.Application.Customers.DTO;

public class UpdateCustomerDto : BaseCustomerDto
{
    public Guid CustomerId { get; set; }
}