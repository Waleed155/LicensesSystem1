using Microsoft.AspNetCore.Mvc;
using Mapster;
using Licenses.Services.ExcutivePositionServices;
using Licenses.ViewModels.ExcutivePositionViewModel;
using Licenses.Dto.ExcutivePositionDto;
using Licenses.ViewModels;
using Licenses.ViewModels.ClientViewModels;

namespace Licenses.Controllers
{
    public class ExcutivePositionController : Controller
    {
        IExcutivePositionService _excutivePositionService;
        public ExcutivePositionController(IExcutivePositionService excutivePositionService)
 
        {
            _excutivePositionService = excutivePositionService;

        }
        public async Task<IActionResult> Index()
        {
            try

            {
                var result = await _excutivePositionService.GetAllAsync();
                if (result.State)
                {
                    var resultViewModel = result.Result.Adapt<IEnumerable<ExcutivePositionReadViewModel>>();

                    return View(resultViewModel);
                }
                else
                {

                    return View();

                }


            }
            catch
            {
                return View("Error", new ErrorViewModelLicenses("positionExcutiveLastLayer", "There is problem in indexAction"));
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExcutivePositionAddViewModel excutivePositionAddViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var excutivePositionAddDto = excutivePositionAddViewModel.Adapt<ExcutivePositionAddDto>();
                    var result = await _excutivePositionService.AddAsync(excutivePositionAddDto);
                    if (result.State)
                    {
                        TempData["SavingSuccess"] = "تم إضافه النشاط بنجاح ";
                        return RedirectToAction("Index");
                    }
                    ModelState.AddModelError("", result.Message);
                    return View(excutivePositionAddViewModel);
                }
                else
                {
                    return View(excutivePositionAddViewModel);
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
        public async Task< IActionResult >Edit(int id )
        {
            var excutivePositionReadDto = await _excutivePositionService.GetByIdAsync(id);
            var excutivePositionReadViewModel = excutivePositionReadDto.Result.Adapt<ExcutivePositionReadViewModel>();
            return View(excutivePositionReadViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEditting(ExcutivePositionReadViewModel excutivePositionReadViewModel)
        {
            try
            {
                if (!ModelState.IsValid) { return View("Edit", excutivePositionReadViewModel); }
                var excutivePositionReadDto = excutivePositionReadViewModel.Adapt<ExcutivePositionReadDto>();
                var result = await _excutivePositionService.UpdateAsync(excutivePositionReadDto);
                if (!result.State)
                {
                    ModelState.AddModelError("", result.Message);
                    return View("Edit", excutivePositionReadViewModel);
                }

                TempData["SavingSuccess"] = $"تم التعديل بنجاح ";
                return RedirectToAction("Index");

            }
            catch
            {
                ModelState.AddModelError("", "there is problem in controller");
                return View(excutivePositionReadViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try 
            {
                var result = await _excutivePositionService.SoftDeleteAsync(id);
                if (!result.State)
                {
                    TempData["SavingSuccess"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }

                TempData["SavingSuccess"] = "تم حذف الموقف التنفيذي  بنجاح";
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                TempData["SavingSuccess"] = "عفوا لم يتم حذف الموقف التنفيذي" + "Problem In LastLayer";
                return RedirectToAction(nameof(Index));
            }

        }
    }
}
