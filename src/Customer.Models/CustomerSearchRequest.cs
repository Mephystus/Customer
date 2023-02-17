namespace Customer.Models;

public class CustomerSearchRequest
{
    #region Public Properties

    public string FistName { get; set; } = default!;

    public string LastName { get; set; } = default!;

    public string? MyProperty { get; set; }

    #endregion Public Properties
}