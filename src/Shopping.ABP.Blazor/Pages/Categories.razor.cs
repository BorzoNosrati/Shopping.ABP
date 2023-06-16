using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Shopping.ABP.Application.Contracts.Dtos.Category;
using Shopping.ABP.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Shopping.ABP.Blazor.Pages;

public partial class Categories
{
    private IReadOnlyList<CategoryDto> CategoryList { get; set; }

    private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
    private int CurrentPage { get; set; }
    private string CurrentSorting { get; set; }
    private int TotalCount { get; set; }

    private bool CanCreateCategory { get; set; }
    private bool CanEditCategory { get; set; }
    private bool CanDeleteCategory { get; set; }

    private CreateCategoryDto NewCategory { get; set; }

    private int EditingCategoryId { get; set; }
    private UpdateCategoryDto EditingCategory { get; set; }

    private Modal CreateCategoryModal { get; set; }
    private Modal EditCategoryModal { get; set; }

    private Validations CreateValidationsRef;

    private Validations EditValidationsRef;

    public Categories()
    {
        NewCategory = new CreateCategoryDto();
        EditingCategory = new UpdateCategoryDto();
    }

    IReadOnlyList<CategoryLookupDto> categoryList = Array.Empty<CategoryLookupDto>();

    protected override async Task OnInitializedAsync()
    {
        await SetPermissionsAsync();
        await GetCategoriesAsync();
        await GetLookupCategoriesAsync();
    }

    private async Task SetPermissionsAsync()
    {
        CanCreateCategory = await AuthorizationService
            .IsGrantedAsync(ABPPermissions.Categories.Create);

        CanEditCategory = await AuthorizationService
            .IsGrantedAsync(ABPPermissions.Categories.Edit);

        CanDeleteCategory = await AuthorizationService
            .IsGrantedAsync(ABPPermissions.Categories.Delete);
    }

    private async Task GetCategoriesAsync()
    {
        var result = await CategoryAppService.GetListAsync(
            new GetCategoryListDto
            {
                MaxResultCount = PageSize,
                SkipCount = CurrentPage * PageSize,
                Sorting = CurrentSorting
            }
        );

        CategoryList = result.Items;
        TotalCount = (int)result.TotalCount;
    }
    private async Task GetLookupCategoriesAsync()
    {
        var result = await CategoryAppService.GetCategoryLookupAsync();

        categoryList = result.Items;
        
    }

    private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<CategoryDto> e)
    {
        CurrentSorting = e.Columns
            .Where(c => c.SortDirection != SortDirection.Default)
            .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
            .JoinAsString(",");
        CurrentPage = e.Page - 1;

        await GetCategoriesAsync();

        await InvokeAsync(StateHasChanged);
    }

    private void OpenCreateCategoryModal()
    {
        CreateValidationsRef.ClearAll();

        NewCategory = new CreateCategoryDto();
        CreateCategoryModal.Show();
    }

    private void CloseCreateCategoryModal()
    {
        CreateCategoryModal.Hide();
    }

    private void OpenEditCategoryModal(CategoryDto category)
    {
        EditValidationsRef.ClearAll();

        EditingCategoryId = category.Id;
        EditingCategory = ObjectMapper.Map<CategoryDto, UpdateCategoryDto>(category);
        EditCategoryModal.Show();
    }

    private async Task DeleteCategoryAsync(CategoryDto category)
    {
        var confirmMessage = L["CategoryDeletionConfirmationMessage", category.Name];
        if (!await Message.Confirm(confirmMessage))
        {
            return;
        }

        await CategoryAppService.DeleteAsync(category.Id);
        await GetCategoriesAsync();
    }

    private void CloseEditCategoryModal()
    {
        EditCategoryModal.Hide();
    }

    private async Task CreateCategoryAsync()
    {
        if (await CreateValidationsRef.ValidateAll())
        {
            await CategoryAppService.CreateAsync(NewCategory);
            await GetCategoriesAsync();
            await CreateCategoryModal.Hide();
        }
    }

    private async Task UpdateCategoryAsync()
    {
        if (await EditValidationsRef.ValidateAll())
        {
            await CategoryAppService.UpdateAsync(EditingCategoryId, EditingCategory);
            await GetCategoriesAsync();
            await EditCategoryModal.Hide();
        }
    }
}

