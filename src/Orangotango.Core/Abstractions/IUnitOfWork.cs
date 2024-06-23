using System.Threading.Tasks;

namespace Orangotango.Core.Abstractions;

public interface IUnitOfWork
{
    Task<bool> Commit();
}
