using System.Security.Cryptography.X509Certificates;
using ecommerce_db_api.Utilities;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("/api/v1/products")]

public class ProductController : ControllerBase
{

  private readonly IProductService _productService;

  public ProductController(IProductService productService)
  {
    _productService = productService;
  }

  //? GET => /api/products => Get all the products
  [HttpGet]
  public async Task<IActionResult> GetAllProducts([FromQuery] QueryParameters queryParameters)
  {
    try
        {
            var product =await _productService.GetProductsService(queryParameters);
            var response=new{Message="return all the product",Product=product};
        return Ok(response);
        }
        catch (ApplicationException ex)
        {
            
             return ApiResponse.ServerError("server error:"+ ex.Message);

        }
        catch (System.Exception ex){
            
             return ApiResponse.ServerError("server error:"+ ex.Message);
  }}

  //? GET => /api/products/{id} => Get a single product by Id
  [HttpGet("{productId}")]
public async Task<IActionResult> GetProductById(Guid productId)
{
    try
    {
        System.Console.WriteLine($"product id {productId}");
        var product =await _productService.GetProductByIdService(productId); 

        if (product == null)
        {
            return ApiResponse.NotFound("product not found" );
        }
        return ApiResponse.Success(product,"product is returned succcessfuly");
    }
    catch (ApplicationException ex)
    {
             return ApiResponse.ServerError("server error:"+ ex.Message);
    }
    catch (System.Exception ex)
    {
             return ApiResponse.ServerError("server error:"+ ex.Message);
    }
}

  // //? Delete => /api/products/{id} => delete a single product by Id
  [HttpDelete("{id}")]
public async Task<IActionResult> DeleteProduct(Guid id)
{
    try
    {
        var result = await _productService.DeleteProductByIdService(id);
        if (result==true)
        {
            return ApiResponse.Success("product deleted successfully" );
        }
        else
        {
            return ApiResponse.NotFound("product not found");
        }}
    catch (ApplicationException ex)
    {
             return ApiResponse.ServerError("server error:"+ ex.Message);
    }
    catch (System.Exception ex)
    {
             return ApiResponse.ServerError("server error:"+ ex.Message);
    }}

  [HttpPost]
public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto newProduct)
{
    try
    {
        var product = await _productService.CreateProductService(newProduct);
        var response = new { Message = "Product created successfully", Product = product };
        return ApiResponse.Created(product, response.Message);
    }
    catch (ApplicationException ex)
    {
        return ApiResponse.ServerError("server error: " + ex.Message);
    }
    catch (System.Exception ex)
    {
        return ApiResponse.ServerError("server error: " + ex.Message);
    }
}

[HttpPut("{id}")]
public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] UpdateProductDto updateProduct)
{
    try
    {
        if (updateProduct == null)
        {
            return ApiResponse.BadRequest("Invalid product data.");}

        var updatedProduct = await _productService.UpdateProductService(id, updateProduct);
        
        if (updatedProduct == null)
        {
            return ApiResponse.NotFound("product not found.");
        }

                return ApiResponse.Success(updateProduct,"product is updated successfuly");

    }
    catch (ApplicationException ex)
    {
             return ApiResponse.ServerError("server error:"+ ex.Message);
    }
    catch (System.Exception ex)
    {
             return ApiResponse.ServerError("server error:"+ ex.Message);
    }}
}