using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stx.Shared.Interfaces.HRM;
using Stx.Shared.Models.HRM;
using Stx.Web.JP.Core;

namespace Stx.Web.JP.Pages.Candidate
{
    public class SkillsModel : BasePageModel
    {
        private readonly ICandidateDataService _candidateDataService;

        public SkillsModel(ICandidateDataService candidateDataService)
        {
            _candidateDataService = candidateDataService;
        }
        public string PageKey { get; set; } = "Skill";

        [BindProperty] public List<HrCandidateSkill> BaseEntryListSkill { get; set; } = new List<HrCandidateSkill>();
        [BindProperty] public List<HrCandidateLanguage> BaseEntryListLang { get; set; } = new List<HrCandidateLanguage>();

        public async Task OnGetAsync()
        {
            try
            {
                base.InitializePage();
                await LoadBaseData();
            }
            catch (Exception ex)
            {
                SetPageStatus(ex);
                StatusMessage.SetError(ex.Message);
            }
        }
              

        public async Task LoadBaseData(bool isThrow = true, string section = "all")
        {
            try
            {
                if (section == "all" || section == "skill")
                    BaseEntryListSkill = await _candidateDataService.GetSkills(CandidateID);
                if (section == "all" || section == "lang")
                    BaseEntryListLang = await _candidateDataService.GetLanguages(CandidateID);
            }
            catch
            {
                if (isThrow) throw;
            }
        }

        public async Task<IActionResult> ReloadPage()
        {
            await LoadBaseData();
            return Page();
        }

        #region SKILLS -------------------------------
        public async Task<IActionResult> OnPostSubmitEntrySkill()
        {
            try
            {
                StatusMessage.Reset();
                BaseEntryListSkill.ForEach(m => m.CandidateID = CandidateID);
                var result = await _candidateDataService.UpdateSkills(BaseEntryListSkill);
                StatusMessage.SetSuccess(AppHelpers.Messages.SuccessMessage);
                return await ReloadPage();
            }
            catch (Exception ex)
            {
                StatusMessage.SetError(ex.Message);
                await LoadBaseData(false, "lang");//reload other non-current-form data
                return Page();
            }
        }

        public async Task<IActionResult> OnPostPrepareEditPanelSkill(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //If any uncomplete record return back to Page
                    bool isContinue = true;
                    if (BaseEntryListSkill.Any(x => string.IsNullOrWhiteSpace(x.SkillName)))
                    {
                        isContinue = false;
                    }
                    StatusMessage.Reset();
                    await LoadBaseData();
                    if (!isContinue) return Page();

                    BaseEntryListSkill.Add(new HrCandidateSkill() { CandidateID = CandidateID, ID = -1 });
                }
                catch (Exception ex)
                {
                    StatusMessage.SetError(ex.Message);
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteEntitySkill(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    StatusMessage.Reset();
                    var result = await _candidateDataService.DeleteResumeComponent("SKIL", CandidateID, id);
                    StatusMessage.SetSuccess(AppHelpers.Messages.SuccessMessage);
                }
                catch (Exception ex)
                {
                    StatusMessage.SetError(ex.Message);
                }
            }
            return await ReloadPage();
        }

        #endregion

        #region LANGUAGES ----------------------------

        public async Task<IActionResult> OnPostSubmitEntryLang()
        {
            try
            {
                StatusMessage.Reset();
                BaseEntryListLang.ForEach(m => m.CandidateID = CandidateID);
                var result = await _candidateDataService.UpdateLanguages(BaseEntryListLang);
                StatusMessage.SetSuccess(AppHelpers.Messages.SuccessMessage);
                return await ReloadPage();
            }
            catch (Exception ex)
            {
                StatusMessage.SetError(ex.Message);
                await LoadBaseData(false, "skill"); //reload other non-current-form data
                return Page();
            }
        }

        public async Task<IActionResult> OnPostPrepareEditPanelLang(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //If any uncomplete record return back to Page
                    bool isContinue = true;
                    if (BaseEntryListLang.Any(x => string.IsNullOrWhiteSpace(x.Language)))
                    {
                        isContinue = false;
                    }
                    StatusMessage.Reset();
                    await LoadBaseData();
                    if (!isContinue) return Page();

                    BaseEntryListLang.Add(new HrCandidateLanguage() { CandidateID = CandidateID, ID = -1 });
                }
                catch (Exception ex)
                {
                    StatusMessage.SetError(ex.Message);
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteEntityLang(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    StatusMessage.Reset();
                    var result = await _candidateDataService.DeleteResumeComponent("LANG", CandidateID, id);
                    StatusMessage.SetSuccess(AppHelpers.Messages.SuccessMessage);
                }
                catch (Exception ex)
                {
                    StatusMessage.SetError(ex.Message);
                }
            }
            return await ReloadPage();
        }

        #endregion
    }
}
