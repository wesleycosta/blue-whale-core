namespace PixelHotel.Core.Abstractions;

public interface IUnitOfWork
{
    Task<bool> Commit();
    Task<int> SaveChanges();
}
