using PixelHotel.Core.Abstractions;

namespace PixelHotel.Core.Services;

public abstract class DataServiceBase(IUnitOfWork _unitOfWork) : QueryServiceBase
{
    protected async Task<Result> SaveChanges(object responseData)
    {
        if (!await _unitOfWork.Commit())
            Notify(nameof(_unitOfWork.Commit), "There was an error while persisting");

        return new Result(ValidationResult, responseData);
    }
}
