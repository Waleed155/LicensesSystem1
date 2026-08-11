using Licenses.Dto.ActivityTypeDto;
using Licenses.Services.ActivityTypeServices;
using Licenses.Services.ClientServices;
using Licenses.ViewModels;
using Licenses.ViewModels.ActivityTypeViewModel;
using Licenses.ViewModels.ClientViewModels;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Licenses.Controllers
{
    public class ActivityTypeController : Controller
    {
        IActivityTypeService _activityTypeService;
        public ActivityTypeController(IActivityTypeService activityTypeService)
        {
            _activityTypeService = activityTypeService;
        }
        public  async Task< IActionResult> Index()
        {
            try
            {
                var result= await _activityTypeService.GetAllAsync();
                if (result.State)
                {
                    var resultViewModel = result.Result.Adapt< IEnumerable< ActivityTypeReadViewModel>>();

                    return View(resultViewModel);
                }
                else
                {

                    return View();

                }


            }
            catch
            {
                return View("Error", new ErrorViewModelLicenses("ActivityLastLayer", "There is problem in indexAction"));
            }
        }
        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ActivityTypeAddViewModel activityTypeAddViewModel)
        {
            try {
                if (ModelState.IsValid)
                {
                    var activityTypeAddDto=activityTypeAddViewModel.Adapt<ActivityTypeAddDto>();
                    var result = await _activityTypeService.AddAsync(activityTypeAddDto);
                    if (result.State) {
                        TempData["SavingSuccess"] = "تم إضافه النشاط بنجاح ";
                    return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", result.Message);
                    return View(activityTypeAddViewModel);
                }
                else
                {
                    return View(activityTypeAddViewModel);
                }
                } catch
                {
                ModelState.AddModelError("", "there is Problem in controller");
                return View();
               }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<  IActionResult> Edit( int id)
        {
            var activityTypeReadDto = await _activityTypeService.GetByIdAsync(id);
            var activityTypeReadViewModel = activityTypeReadDto.Result.Adapt<ActivityTypeReadViewModel>();
            return View(activityTypeReadViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> SaveEditting(ActivityTypeReadViewModel activityTypeReadViewModel)
        {
            try
            {
                if (!ModelState.IsValid) { return View("Edit", activityTypeReadViewModel); }
                var activityTypeReaDto = activityTypeReadViewModel.Adapt<ActivityTypeReadDto>();
                var result = await _activityTypeService.UpdateAsync(activityTypeReaDto);
                if (!result.State)
                { 
                    ModelState.AddModelError("", result.Message);
                    return View("Edit",activityTypeReadViewModel);
                }
 
                 TempData["SavingSuccess"] = $"تم التعديل بنجاح ";
                 return RedirectToAction("Index");
                
            } 
            catch
            {
                ModelState.AddModelError("","there is problem in controller");
                return View(activityTypeReadViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id )
        {
            try
            {
                 var result=await _activityTypeService.SoftDeleteAsync(id);
                if (!result.State)
                {
                    TempData["SavingSuccess"]=result.Message;
                    return RedirectToAction(nameof(Index));
                }

                TempData["SavingSuccess"] = "تم حذف النشاط بنجاح";
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                TempData["SavingSuccess"] = "عفوا لم يتم حذف النشاط"+"Problem In LastLayer";
                return RedirectToAction(nameof(Index));
            }

}
    }
}
