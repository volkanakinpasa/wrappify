using System.Net.Http;
using System.Threading.Tasks;


namespace wrappify
{
    public class WebApiManager
    {
        public async Task<string> PerformRequest(WebApiRequest request)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage responseMessage = await client.GetAsync(request.Uri);
            return await responseMessage.Content.ReadAsStringAsync();
        }
    }
}