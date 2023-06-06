using Shopping.ABP.Application.Contracts.Dtos.Product;
using Shopping.ABP.Application.Contracts.Services;
using Shouldly;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Validation;
using Xunit;

namespace Shopping.ABP.Services;

public class ProductAppService_Tests : ABPApplicationTestBase 
{
    private readonly IProductAppService _productAppService;

	public ProductAppService_Tests()
	{
		_productAppService = GetRequiredService<IProductAppService>();
	}

	[Fact]
	public async Task Should_Get_List_Of_Productss()
	{
        //Act
        var result = await _productAppService.GetListAsync(
            new PagedAndSortedResultRequestDto()
        );

        //Assert
        result.TotalCount.ShouldBeGreaterThan(0);
        result.Items.ShouldContain(b => b.Name == "Macbook Pro 14");
    }

    [Fact]
    public async Task Should_Create_A_Valid_Product()
    {
        //Act
        var name = $"test product {new Random().Next(1, 100)}";

        // moc category
        //

        var result = await _productAppService.CreateAsync(
            new CreateUpdateProductDto
            {
                Name = name,
                Price = 10,
                CategoryId = 1,
            }
        );

        //Assert
        result.Id.ShouldNotBe(0);
        result.Name.ShouldBe(name);
    }

    [Fact]
    public async Task Should_Not_Create_A_Product_Without_Name()
    {
        var exception = await Assert.ThrowsAsync<AbpValidationException>(async () =>
        {
            await _productAppService.CreateAsync(
                new CreateUpdateProductDto
                {
                    Name = "",
                    Price = 10,
                    CategoryId= 1,
                }
            );
        });

        exception.ValidationErrors
            .ShouldContain(err => err.MemberNames.Any(mem => mem == "Name"));
    }



}
