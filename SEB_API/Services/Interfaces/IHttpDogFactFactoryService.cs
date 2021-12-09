using System.Threading.Tasks;

namespace SEB_API
{
    public interface IHttpDogFactFactoryService
    {
        public Task<DogFact> ExecuteAsync(int factNumber = 1);
    }
}
