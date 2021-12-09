using System.Threading.Tasks;

namespace SEB_API
{
    public class HttpDogFactFactoryService : IHttpDogFactFactoryService
    {
        private readonly DogFactClient _dogFactClient;
        public HttpDogFactFactoryService(DogFactClient apiHelper)
        {
            _dogFactClient = apiHelper;
        }

        public async Task<DogFact> ExecuteAsync(int factNumber = 1)
        {
            return await _dogFactClient.GetDogFactsFromClientAsync(factNumber);
        }
    }
}
