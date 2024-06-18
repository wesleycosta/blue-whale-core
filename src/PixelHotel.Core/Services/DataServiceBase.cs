using PixelHotel.Core.Abstractions;
using System.Threading.Tasks;

namespace PixelHotel.Core.Services;

public abstract class DataServiceBase(IUnitOfWork _unitOfWork) : QueryServiceBase
{
    protected new Result SuccessfulResult(object responseData)
        => new(ValidationResult, responseData);

    protected async Task<bool> Commit()
    {
        if (!await _unitOfWork.Commit())
        {
            Notify(nameof(_unitOfWork.Commit), "There was an error while persisting");
            return false;
        }

        return true;
    }
}
