using Licenses.Dto.StepDto;
using Licenses.Dto;
using Licenses.Services.StepServices;
using Licenses.Services.OrderServices;
using Licenses.ViewModels.OrderViewModel;
using Licenses.ViewModels.StepViewModel;
using Mapster;
using Licenses.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Licenses.ViewModels.ClientViewModels;

namespace Licenses.Controllers
{
    public class StepController : Controller
    {
        IStepService _stepService;
        public StepController(IStepService stepService)
        {
            _stepService = stepService;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            try
            {
                var result = await _stepService.GetAllAsync(page, pageSize: 15);
                if (!result.State)
                    return View("Error", new ErrorViewModelLicenses("StepPage", result.Message));
                var resultViewModel = result.
                    Result.
                    Adapt<PagedResult<StepReadViewModel>>();
                return View(resultViewModel);
            }
            catch
            {
                return View("Error", new ErrorViewModelLicenses("StepPage", "Error in Controller"));

            }
        }
        public async Task<IActionResult> GetAllDeleted(int page)
        {
            try
            {
                var result = await _stepService.
                    GetAllDeletedAsync(page, pageSize: 15);
                if (!result.State)
                    return View("Error", new ErrorViewModelLicenses("StepPage", result.Message));
                var resultViewModel = result.
                    Result.
                    Adapt<PagedResult<StepReadViewModel>>();
                return View(resultViewModel);
            }
            catch
            {
                return View("Error", new ErrorViewModelLicenses("StepPage", "Error in Controller"));

            }
        }
        public async Task<IActionResult>
            SearchByDeletedName(string search, int page = 1)
        {
            try
            {
                var result = await _stepService.
                    SearchByDeletedNameAsync(search, page, pageSize:15);
                if (result.State && result.Result!.TotalPages > 0)
                {
                    var resultViewModel = result.
                        Result.
                        Adapt<PagedResult<StepReadViewModel>>();
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
        public async Task<IActionResult> Create(StepAddViewModel stepAddViewModel)
        {
            try
            {
                if(!ModelState.IsValid) {
                    
                    return View(stepAddViewModel); }
                var stepAddDto = stepAddViewModel.Adapt<StepAddDto>();
                var result = await _stepService.AddAsync(stepAddDto);
                var resultViewModel = result.Result.Adapt<StepReadViewModel>();
                if (!result.State)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(stepAddViewModel);
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
        public async Task< IActionResult> Edit(int  id)
        {
            var stepReadDto = await _stepService.GetByIdAsync(id);
            var stepReadViewModel = stepReadDto.Result.Adapt<StepReadViewModel>();
            return View(stepReadViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEditting(StepReadViewModel stepReadViewModel)
        {
            try
            {
                if (!ModelState.IsValid) { return View("Edit", stepReadViewModel); }
                var stepReaDto = stepReadViewModel.Adapt<StepReadDto>();
                var result = await _stepService.UpdateAsync(stepReaDto);
                if (!result.State)
                {
                    ModelState.AddModelError("", result.Message);
                    return View("Edit", stepReadViewModel);
                }
                TempData["SavingSuccess"] = $"تم التعديل بنجاح ";

                if (stepReadViewModel.IsDeleted)
                    return RedirectToAction("GetAllDeleted"); 
                else
                return RedirectToAction("Index");

            }
            catch
            {
                ModelState.AddModelError("", "there is problem in controller");
                return View(stepReadViewModel);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _stepService.SoftDeleteAsync(id);
                if (!result.State)
                {
                    TempData["SavingSuccess"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }

                TempData["SavingSuccess"] = "تم حذف الطلب  بنجاح";
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                TempData["SavingSuccess"] = "عفوا لم يتم حذف الطلب" + "Problem In LastLayer";
                return RedirectToAction(nameof(Index));
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Revive(int id)
        {
            try
            {
                var result = await _stepService.Revive(id);
                if (!result.State)
                {
                    TempData["SavingSuccess"] = result.Message;
                    return RedirectToAction(nameof(GetAllDeleted));
                }

                TempData["SavingSuccess"] = "تم إسترجاع الطلب  بنجاح";
                return RedirectToAction(nameof(GetAllDeleted));

            }
            catch
            {
                TempData["SavingSuccess"] = "عفوا لم إسترجاع الطلب" + "Problem In LastLayer";
                return RedirectToAction(nameof(GetAllDeleted));
            }

        }
    }
}
