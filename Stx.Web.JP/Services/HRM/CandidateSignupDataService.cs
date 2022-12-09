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
    public class CandidateSignupDataService : ICandidateSignupDataService
    {
        private readonly IApiHelperDataService _apiHelperDataService;
        public CandidateSignupDataService(IApiHelperDataService apiHelperDataService)
        {
            _apiHelperDataService = apiHelperDataService;
        }


        public async Task<HrCandidate> UpdateRecord(HrCandidate record, EntryState st, string userId)
        {
            if (st == EntryState.Update)
            {
                var serializeJson = new StringContent(JsonSerializer.Serialize(record), Encoding.UTF8, "application/json");
                using var response = await _apiHelperDataService.UpdateData(serializeJson, st, "CandidateSignup", "");
                {
                    response.EnsureSuccessStatusCode();
                    using var stream = await response.Content.ReadAsStreamAsync();
                    return await JsonSerializer.DeserializeAsync<HrCandidate>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                }
            }
            else
            {
                return null;
            }
        }

    }
}
