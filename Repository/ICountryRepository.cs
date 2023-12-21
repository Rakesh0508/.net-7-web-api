using WorldAPI.Models;

namespace WorldAPI.Repository
{
    public interface ICountryRepository : IGenericRepository<Country>
    {
        Task Update(Country entity);

    }
}
