using Stx.Shared.Interfaces;
using Stx.Shared.Interfaces.CRM;
using Stx.Shared.Models.CRM;
using Stx.Shared.Models.HRM;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Stx.Shared;
using Stx.Shared.Models.Parm;
using System.Text;
using System.Net.Http;
using Stx.Shared.Extensions.Http;

namespace Stx.Web.JP.Services.CRM
{
    public class CorporatePublicDataService : ICorporatePublicDataService
    {
        private readonly IApiHelperDataService _apiHelperDataService;
        public CorporatePublicDataService(IApiHelperDataService apiHelperDataService)
        {
            _apiHelperDataService = apiHelperDataService;
        }


        public async Task<CorporatePublicDTO> GetRecordByID(int id, int candidateID)
        {
            string endpointWithParm = @$"CorporatePublic/{id}/{candidateID}";
            var response = await _apiHelperDataService.GetData(endpointWithParm);
            await response.EnsureSuccessStatusCode2();
            using var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<CorporatePublicDTO>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        //public async Task<List<CorporatePublicDTO>> Search(string keyword, string location, int candidateID)
        //{
        //    string endpointWithParm = @$"CorporatePublic/Search/{keyword}/{location}/{candidateID}";
        //    var response = await _apiHelperDataService.GetData(endpointWithParm);
        //    await response.EnsureSuccessStatusCode2();
        //    using var stream = await response.Content.ReadAsStreamAsync();
        //    return await JsonSerializer.DeserializeAsync<List<CorporatePublicDTO>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        //}

        public async Task<List<CorporatePublicDTO>> Search(HrJobSearchParmDTO entry)
        {
            var serializeJson = new StringContent(JsonSerializer.Serialize(entry), Encoding.UTF8, "application/json");
            var response = await _apiHelperDataService.PostData(@$"CorporatePublic", serializeJson);
            await response.EnsureSuccessStatusCode2();
            using var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<List<CorporatePublicDTO>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}
