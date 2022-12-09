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
    public class CandidateDataService : ICandidateDataService
	{
        private readonly IApiHelperDataService _apiHelperDataService;
        public CandidateDataService(IApiHelperDataService apiHelperDataService)
        {
            _apiHelperDataService = apiHelperDataService;
        }

        public async Task<List<HrCandidate>> GetAllRecords()
        {
            throw new NotImplementedException();
        }

        public async Task<HrCandidate> GetRecordByID(int id)
        {
            using var response = await _apiHelperDataService.GetDataByID(id, "Candidate");
            {
                response.EnsureSuccessStatusCode();
                using var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<HrCandidate>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }

        public async Task<HrCandidate> GetRecordByCD(string code)
        {           
            using var response = await _apiHelperDataService.GetDataByCD(code, "Candidate");
            {
                response.EnsureSuccessStatusCode();
                using var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<HrCandidate>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }

        public async Task<HrCandidate> UpdateRecord(HrCandidate record, EntryState st, string userId)
        {
            var serializeJson = new StringContent(JsonSerializer.Serialize(record), Encoding.UTF8, "application/json");
            using var response = await _apiHelperDataService.UpdateData(serializeJson, st, "Candidate", "");
            {
                response.EnsureSuccessStatusCode();
                using var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<HrCandidate>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }
          
        public async Task<ReturnObj> DeleteRecord(int docnum, string userId)
        {
            using var response = await _apiHelperDataService.DeleteRecord("Candidate", docnum, "");
            {
                response.EnsureSuccessStatusCode();
                using var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<ReturnObj>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }

        public async Task<HrCandidate> GetRecordPublicByID(int id)
        {
            using var response = await _apiHelperDataService.GetDataByID(id, "Candidate/Public");
            {
                response.EnsureSuccessStatusCode();
                using var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<HrCandidate>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }

        public async Task<HrCandidateExperience> UpdateExperiences(HrCandidateExperience entry)
        {
            var serializeJson = new StringContent(JsonSerializer.Serialize(entry), Encoding.UTF8, "application/json");
            using var response = await _apiHelperDataService.PostData("Candidate/Experiences", serializeJson);
            {
                response.EnsureSuccessStatusCode();
                using var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<HrCandidateExperience>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }

        public async Task<HrCandidateEducation> UpdateEducations(HrCandidateEducation entry)
        {
            var serializeJson = new StringContent(JsonSerializer.Serialize(entry), Encoding.UTF8, "application/json");
            using var response = await _apiHelperDataService.PostData("Candidate/Educations", serializeJson);
            {
                response.EnsureSuccessStatusCode();
                using var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<HrCandidateEducation>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }

        public async Task<HrCandidateCertificate> UpdateCertificates(HrCandidateCertificate entry)
        {
            var serializeJson = new StringContent(JsonSerializer.Serialize(entry), Encoding.UTF8, "application/json");
            using var response = await _apiHelperDataService.PostData("Candidate/Certificates", serializeJson);
            {
                response.EnsureSuccessStatusCode();
                using var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<HrCandidateCertificate>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }

        public async Task<List<HrCandidateSkill>> UpdateSkills(List<HrCandidateSkill> entry)
        {
            var serializeJson = new StringContent(JsonSerializer.Serialize(entry), Encoding.UTF8, "application/json");
            using var response = await _apiHelperDataService.PostData("Candidate/Skills", serializeJson);
            {
                response.EnsureSuccessStatusCode();
                using var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<List<HrCandidateSkill>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }

        public async Task<List<HrCandidateLanguage>> UpdateLanguages(List<HrCandidateLanguage> entry)
        {
            var serializeJson = new StringContent(JsonSerializer.Serialize(entry), Encoding.UTF8, "application/json");
            using var response = await _apiHelperDataService.PostData("Candidate/Languages", serializeJson);
            {
                response.EnsureSuccessStatusCode();
                using var stream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<List<HrCandidateLanguage>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
        }
    }
}
