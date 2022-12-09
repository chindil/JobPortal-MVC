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
    public class CandidateProfileDataService : ICandidateProfileDataService
    {
        private readonly IApiHelperDataService _apiHelperDataService;
        public CandidateProfileDataService(IApiHelperDataService apiHelperDataService)
        {
            _apiHelperDataService = apiHelperDataService;
        }

        public async Task<HrCandidateProfile> GetRecordByID(int id)
        {
            using var response = await _apiHelperDataService.GetDataByID(id, "CandidateProfile");
            {
                response.EnsureSuccessStatusCode();
                using var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<HrCandidateProfile>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }

        public async Task<HrCandidateProfile> GetRecordByCD(string code)
        {           
            using var response = await _apiHelperDataService.GetDataByCD(code, "CandidateProfile");
            {
                response.EnsureSuccessStatusCode();
                using var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<HrCandidateProfile>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }

        public async Task<HrCandidateProfile> UpdateRecord(HrCandidateProfile record, EntryState st, string userId)
        {
            if (st == EntryState.Update)
            {
                var serializeJson = new StringContent(JsonSerializer.Serialize(record), Encoding.UTF8, "application/json");
                using var response = await _apiHelperDataService.UpdateData(serializeJson, st, "CandidateProfile", "");
                {
                    response.EnsureSuccessStatusCode();
                    using var stream = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<HrCandidateProfile>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
            else
            {
                return null;
            }
        }

    }
}
