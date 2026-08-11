using Licenses.Dto.StepDto;
using Licenses.Dto;
using Licenses.Services.StageServices;
using Licenses.ViewModels.StageViewModel;
using Mapster;
using Licenses.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Licenses.Dto.StageDto;

namespace Licenses.Controllers
{
    public class StageController : Controller
    {
        IStageService _stageService;
        public StageController(IStageService stageService)
        {
            _stageService = stageService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            try
            {
                var result = await _stageService.GetAllAsync(page, pageSize: 15);
                if (!result.State)
                    return View("Error", new ErrorViewModelLicenses("StagePage", result.Message));
                var resultViewModel = result.
                    Result.
                    Adapt<PagedResult<StageReadViewModel>>();
                return View(resultViewModel);
            }
            catch
            {
                return View("Error", new ErrorViewModelLicenses("StagePage", "Error in Controller"));

            }
        }
        public async Task<IActionResult> GetAllDeleted(int page)
        {
            try
            {
                var result = await _stageService.
                    GetAllDeletedAsync(page, pageSize: 10);
                if (!result.State)
                    return View("Error", new ErrorViewModelLicenses("StagePage", result.Message));
                var resultViewModel = result.
                    Result.
                    Adapt<PagedResult<StageReadViewModel>>();
                return View(resultViewModel);
            }
            catch
            {
                return View("Error", new ErrorViewModelLicenses("StagePage", "Error in Controller"));

            }
        }
        public async Task<IActionResult>
            SearchByDeletedName(string search, int page = 1)
        {
            try
            {
                var result = await _stageService.
                    SearchByDeletedNameAsync(search, page, pageSize: 10);
                if (result.State && result.Result!.TotalPages > 0)
                {
                    var resultViewModel = result.
                        Result.
                        Adapt<PagedResult<StageReadViewModel>>();
                    ViewBag.search = search;
                    return View("GetAllDeleted", resultViewModel);

                }
                else
                {
                    TempData["SavingSuccess"] = result.Message;
                    return RedirectToAction("GetAllDeleted");
                }

            }
            catch
            {
                TempData["SavingSuccess"] = "there is problem in controller";
                return RedirectToAction("GetAllDeleted");
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StageAddViewModel stageAddViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return View(stageAddViewModel);
                }
                var stageAddDto = stageAddViewModel.Adapt<StageAddDto>();
                var result = await _stageService.AddAsync(stageAddDto);
                var resultViewModel = result.Result.Adapt<StageReadViewModel>();
                if (!result.State)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(stageAddViewModel);
                }
                else
                {
                    TempData["SavingSuccess"] = "تمت اضافه الخطوه بنجاح";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                ModelState.AddModelError("", "there is Problem in controller");
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var stageReadDto = await _stageService.GetByIdAsync(id);
            var stageReadViewModel = stageReadDto.Result.Adapt<StageReadViewModel>();
            return View(stageReadViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEditting(StageReadViewModel stageReadViewModel)
        {
            try
            {
                if (!ModelState.IsValid) { return View("Edit", stageReadViewModel); }
                var stageReaDto = stageReadViewModel.Adapt<StageReadDto>();
                var result = await _stageService.UpdateAsync(stageReaDto);
                if (!result.State)
                {
                    ModelState.AddModelError("", result.Message);
                    return View("Edit", stageReadViewModel);
                }
                TempData["SavingSuccess"] = $"تم التعديل بنجاح ";

                if (stageReadViewModel.IsDeleted)
                    return RedirectToAction("GetAllDeleted");
                else
                    return RedirectToAction("Index");

            }
            catch
            {
                ModelState.AddModelError("", "there is problem in controller");
                return View(stageReadViewModel);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _stageService.SoftDeleteAsync(id);
                if (!result.State)
                {
                    TempData["SavingSuccess"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }

                TempData["SavingSuccess"] = "تم حذف الخطوه  بنجاح";
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                TempData["SavingSuccess"] = "عفوا لم يتم حذف الخطوه" + "Problem In LastLayer";
                return RedirectToAction(nameof(Index));
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Revive(int id)
        {
            try
            {
                var result = await _stageService.Revive(id);
                if (!result.State)
                {
                    TempData["SavingSuccess"] = result.Message;
                    return RedirectToAction(nameof(GetAllDeleted));
                }

                TempData["SavingSuccess"] = "تم إسترجاع الخطوه  بنجاح";
                return RedirectToAction(nameof(GetAllDeleted));

            }
            catch
            {
                TempData["SavingSuccess"] = "عفوا لم يتم إسترجاع الخطوه" + "Problem In LastLayer";
                return RedirectToAction(nameof(GetAllDeleted));
            }

        }
    } 
    }
