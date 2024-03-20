namespace PixelHotel.Core.Database;

public interface IUnitOfWork
{
    Task<bool> Commit();
}
