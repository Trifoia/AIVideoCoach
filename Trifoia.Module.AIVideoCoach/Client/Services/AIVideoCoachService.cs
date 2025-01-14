using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Modules;
using Oqtane.Services;
using Oqtane.Shared;
using System.Net;

namespace Trifoia.Module.AIVideoCoach.Services
{
    public class AIVideoCoachService : ResponseServiceBase, IService
    {
        public AIVideoCoachService(IHttpClientFactory http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("AIVideoCoach");

        public async Task<(List<Models.AIVideoCoach>,HttpStatusCode)> GetAIVideoCoachsAsync()
        {
            var url = $"{Apiurl}";
            (var data, var response) = await GetJsonWithResponseAsync<List<Models.AIVideoCoach>>(url);
            return (data, response.StatusCode);      
        }

        public async Task<(Models.AIVideoCoach,HttpStatusCode)> GetAIVideoCoachAsync(int id)
        {
            var url = $"{Apiurl}/{id}";
            (var data, var response) = await GetJsonWithResponseAsync<Models.AIVideoCoach>(url);
            return (data, response.StatusCode);        
        }

        public async Task<(Models.AIVideoCoach,HttpStatusCode)> AddAIVideoCoachAsync(Models.AIVideoCoach item)
        {
            var url = $"{Apiurl}";
            (var data, var response) = await PostJsonWithResponseAsync<Models.AIVideoCoach>(url,item);
            return (data, response.StatusCode);        
        }

        public async Task<(Models.AIVideoCoach,HttpStatusCode)> UpdateAIVideoCoachAsync(Models.AIVideoCoach item)
        {
            var url = $"{Apiurl}/{item.AIVideoCoachId}";
            (var data, var response) = await PutJsonWithResponseAsync<Models.AIVideoCoach>(url,item);
            return (data, response.StatusCode);        
        }

        public async Task<HttpStatusCode> DeleteAIVideoCoachAsync(int id)
        {
            var url = $"{Apiurl}/{id}";
            var response  = await DeleteWithResponseAsync(url);
            return response.StatusCode;
        }
    }
}
