public record CategoryDto
{
  public Guid CategoryId {get;set;}
  public required string CategoryName { get; set; }
  public string Description { get; set; } 
}