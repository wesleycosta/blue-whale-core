using System.Threading.Tasks;

namespace PixelHotel.Core.Abstractions;

public interface IUnitOfWork
{
    Task<bool> Commit();
}
