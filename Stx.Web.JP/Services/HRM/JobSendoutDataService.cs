using Stx.Shared.Common;
using Stx.Shared.Interfaces;
using Stx.Shared.Interfaces.HRM;
using Stx.Shared.Models.HRM;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Stx.Web.JP.Services.HRM
{
    public class JobSendoutDataService : IJobSendoutDataService
    {
        private readonly IApiHelperDataService _apiHelperDataService;

        public JobSendoutDataService(IApiHelperDataService apiHelperDataService)
        {
            _apiHelperDataService = apiHelperDataService;
        }

        public async Task<ReturnObj> Submit(HrJobSendout jobSendout)
        {
            var serializeJson = new StringContent(JsonSerializer.Serialize(jobSendout), Encoding.UTF8, "application/json");
            var response = await _apiHelperDataService.UpdateData(serializeJson, Shared.Status.EntryState.New, "JobSendout", "");

            response.EnsureSuccessStatusCode();
            using var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<ReturnObj>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

    }
}
