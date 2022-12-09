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

namespace Stx.Web.JP.Services.HRM
{
    public class JobOrderPreviewDataService : IJobOrderPreviewDataService
    {
        private readonly IApiHelperDataService _apiHelperDataService;
        public JobOrderPreviewDataService(IApiHelperDataService apiHelperDataService)
        {
            _apiHelperDataService = apiHelperDataService;
        }


        public async Task<HrJobOrderPreview> GetRecordByID(int id, int candidateID)
        {
            //Error Reason: (not supplied all the parms to api; [/JobOrderPreview/{id}/{candidateid}]
            string endpointWithParm = @$"JobOrderPreview/{id}/{candidateID}/";
            var response = await _apiHelperDataService.GetData(endpointWithParm);
            {
                response.EnsureSuccessStatusCode();
                using var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<HrJobOrderPreview>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }


        public async Task<HrJobOrderPreview> UpdateRecord(HrJobOrderPreview record, EntryState st, string userId)
        {
            var serializeJson = new StringContent(JsonSerializer.Serialize(record), Encoding.UTF8, "application/json");
            var response = await _apiHelperDataService.UpdateData(serializeJson, st, "JobOrderPreview", "");
            {
                response.EnsureSuccessStatusCode();
                using var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<HrJobOrderPreview>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }


    }
}
