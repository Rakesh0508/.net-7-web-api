using WorldAPI.Models;

namespace WorldAPI.Repository
{
    public interface IStatesRepository : IGenericRepository<States>
    {
        Task Update(States entity);
    }
}
