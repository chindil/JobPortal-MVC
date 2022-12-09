using Stx.Shared.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using System.Data.Common;
using Stx.Shared.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Stx.Shared.Models.HRM;
using Stx.Shared.Status;
using Stx.Web.JP.Core;
using System.Net.Http.Headers;
using Stx.Shared.Interfaces.HRM;
using Stx.Shared.Models.Parm;

namespace Stx.Web.JP.Services.HRM
{
    public class JobSearchDataService : IJobSearchDataService
    {
        private readonly IApiHelperDataService _apiHelperDataService;
        public JobSearchDataService(IApiHelperDataService apiHelperDataService)
        {
            _apiHelperDataService = apiHelperDataService;
        }

        public async Task<List<HrJobOrderSearch>> Search(string keyword, string location, string jobindustry, int candidateid)
        {
            HrJobSearchParmDTO record = new HrJobSearchParmDTO(keyword, location, new List<string>() { jobindustry }, candidateid); 
            var serializeJson = new StringContent(JsonSerializer.Serialize(record), Encoding.UTF8, "application/json");
            //string endpointWithParm = @$"JobSearch/keyword={keyword}/location={location}/jobindustry={jobindustry}/candidateid={candidateid}/";

            using var response = await _apiHelperDataService.GetData("JobSearch", serializeJson);
            {
                response.EnsureSuccessStatusCode();
                using var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<List<HrJobOrderSearch>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }

    }
}
