using Shopping.ABP.Application.Contracts.Dtos.Category;
using Shopping.ABP.Application.Contracts.Services;
using Shopping.ABP.Exceptions;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shopping.ABP.Services;

public class CategoryAppService_Tests : ABPApplicationTestBase
{
    private readonly ICategoryAppService _categoryAppService;

    public CategoryAppService_Tests()
    {
        _categoryAppService = GetRequiredService<ICategoryAppService>();
    }

    [Fact]
    public async Task Should_Get_All_Categories_Without_Any_Filter()
    {
        var result = await _categoryAppService.GetListAsync(new GetCategoryListDto());

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(2);
        result.Items.ShouldContain(category => category.Name == "Digital Gadget");
        result.Items.ShouldContain(category => category.Name == "Laptops");
    }

    [Fact]
    public async Task Should_Get_Filtered_Categories()
    {
        var result = await _categoryAppService.GetListAsync(
            new GetCategoryListDto { Filter = "Digital" });

        result.TotalCount.ShouldBeGreaterThanOrEqualTo(1);
        result.Items.ShouldContain(category => category.Name == "Digital Gadget");
        result.Items.ShouldNotContain(category => category.Name == "Laptops");
    }

    [Fact]
    public async Task Should_Create_A_New_Category()
    {
        var categoryDto = await _categoryAppService.CreateAsync(
            new CreateCategoryDto
            {
                Name = "New Category",
                ParentId=null
            }
        );

        categoryDto.Id.ShouldNotBe(0);
        categoryDto.Name.ShouldBe("New Category");
    }

    [Fact]
    public async Task Should_Not_Allow_To_Create_Duplicate_Category()
    {
        await Assert.ThrowsAsync<CategoryAlreadyExistsException>(async () =>
        {
            await _categoryAppService.CreateAsync(
                new CreateCategoryDto
                {
                    Name = "Laptops"
                }
            );
        });
    }

    //TODO: Test other methods...
}
