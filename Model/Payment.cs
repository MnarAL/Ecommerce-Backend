//Model
public class Payment
{
  public Guid PaymentId{get;set;}
  public decimal Amount{get;set;}

// public Payment PaymentMethod {get; set;}
  public required Payment PaymentMethod {get;set;}

}