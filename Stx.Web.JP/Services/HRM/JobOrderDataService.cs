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
    public class JobOrderDataService : IJobOrderDataService
    {
        private readonly IApiHelperDataService _apiHelperDataService;
        public JobOrderDataService(IApiHelperDataService apiHelperDataService)
        {
            _apiHelperDataService = apiHelperDataService;
        }

        public async Task<HrJobOrder> GetRecordByID(int id)
        {
            var response = await _apiHelperDataService.GetRecordByID(id, "JobOrder");
            if (response != null)
            {
                return await JsonSerializer.DeserializeAsync<HrJobOrder>(response);
            }
            else
            {
                return null;
            }
        }

        public async Task<HrJobOrder> UpdateRecord(HrJobOrder record, EntryState st, string userId)
        {
            var serializeJson = new StringContent(JsonSerializer.Serialize(record), Encoding.UTF8, "application/json");
            var response = await _apiHelperDataService.UpdateRecord(serializeJson, st, "JobOrder", "");

            if (response != null)
            {
                return await JsonSerializer.DeserializeAsync<HrJobOrder>(response);
            }

            return null;
        }

        public async Task<ReturnObj> DeleteRecord(int docnum, string userId)
        {
            var response = await _apiHelperDataService.DeleteRecord("JobOrder", docnum, "");
            if (response != null)
            {
                return new ReturnObj(true);
            }
            else
            {
                return new ReturnObj(false, await JsonSerializer.DeserializeAsync<string>(response));
            }
        }

    }
}
