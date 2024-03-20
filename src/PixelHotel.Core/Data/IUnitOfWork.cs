namespace PixelHotel.Core.Data;

public interface IUnitOfWork
{
    Task<bool> Commit();
}
