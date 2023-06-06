using Volo.Abp;

namespace Shopping.ABP.Exceptions;

public class CategoryAlreadyExistsException : BusinessException
{
    public CategoryAlreadyExistsException(string name)
        : base(ABPDomainErrorCodes.CategoryAlreadyExists)
    {
        WithData("name", name);
    }
}