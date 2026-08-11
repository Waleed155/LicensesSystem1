
using Licenses.Services.ClientServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Licenses.ViewModels;
using Licenses.ViewModels.ClientViewModels;
using Mapster;
using Licenses.Dto.ClientsDto;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Licenses.Dto.LotDto;
using Licenses.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Licenses.Dto;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Licenses.ViewModels.OrderViewModel;

namespace Licenses.Controllers
{
    public class ClientController : Controller
    {
        readonly IClientService _clientService;
        public ClientController (IClientService clientService)
        {
            _clientService = clientService;
        }
        public async  Task< IActionResult>  Index(int page=1,int pageSize=15)
        {

            try
            {
                var result = await _clientService.GetAllAsync(page, pageSize);
                if (result.State )
                {
                 
                   var resultViewModel= result.Result.Adapt<PagedResult<ClientReadViewModel>>(); 
                    return View(resultViewModel);
                }
                {
                    return View();
                }
            }
            catch
            {
                return View("Error" , new ErrorViewModelLicenses("ClientLastLayer ","There is problem in controller"));

            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>SearchByNameOrNationalId(string search, int page= 1, int pageSize = 15)
        {
            try 
            {
                var result = await _clientService.GetByNameOrNationalId(search, page, pageSize);
                if (result.State&& result.Result!.TotalPages>0) {
                    var resultViewModel = result.Result.Adapt<PagedResult<ClientReadViewModel>>();
                    ViewBag.search = search;
                    return View("Index",resultViewModel);

                }else
                {
                    TempData["SavingSuccess"] = result.Message;
                     return RedirectToAction("Index");
                }

            }
            catch
            {
                TempData["SavingSuccess"] = "there is problem in controller";
                return RedirectToAction("Index");
            }
        }

        public async Task< ActionResult> Details(int id)
        {
            try
            {
                var clientDto = await _clientService.GetByIdAsync(id);
                if (!clientDto.State )
                {
                    return NotFound("لا يوجد عميل بهذه الهويه");
                }

                var clientReadViewModel = clientDto.Result.Adapt<ClientReadViewModel>();
                return View(clientReadViewModel);
            }catch
            {
                return View("Error",new ErrorViewModelLicenses("Client Controller","problem occuerd in Details Action"));

            }
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsWithLots(int id)
        {
            try
            {
                  var result=await _clientService.GetByIdWithLotsAsync(id);
                if (result.State) 
                {
                    TypeAdapterConfig< ClientWithLotDto,ClientWithLotsViewModel>.
                   NewConfig().
                   Map(dst => dst.LotsViewModel,
                   src => src.LotReadDtos.Adapt<IEnumerable<LotReadDto>>());
                    var clientWithLotReadViewModel= result.Result.
                        Adapt<ClientWithLotsViewModel>();
                    return View( clientWithLotReadViewModel);
                }
                else
                {
                    return View("Error", new ErrorViewModelLicenses("Client Service", result.Message));
                }
            }
            catch
            {
                return View("Error", new ErrorViewModelLicenses("Client Last Layer", "problem occuerd in DetailsWithLlots Action"));
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(ClientAddViewModel clientAddViewModel)
        {
            try
            {
                var clientExistDto = await _clientService.GetByNationalIdAsync(clientAddViewModel.NationalId);
                if (ModelState.IsValid)
                {
                    var clientDto = clientAddViewModel.Adapt<ClientAddDto>();

                    var clientAdded = await _clientService.AddAsync(clientDto);
                    if (clientAdded.State && clientAdded.Result!.Id != 0)
                    {
                        TempData["SavingSuccess"] = clientAdded.Message;

                        return RedirectToAction(nameof(Index));
                    }
                    ModelState.AddModelError("", clientAdded.Message);

                    return View(viewName: "Create", clientAddViewModel);

                }
                else
                {
                    return View(viewName: "Create", clientAddViewModel);
                }
            }
            catch
            {
                ModelState.AddModelError("", "problem in saving in controller");
                return View();
            }

         }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task< IActionResult> Edit(int  id)
        {
            var clientReadDto = await _clientService.GetByIdAsync(id);
            var clientReadViewModel = clientReadDto.Result.Adapt<ClientReadViewModel>();
            return View(clientReadViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> SaveEditting(ClientReadViewModel clientReadViewModel)
        {
            try
            {
                if(!ModelState.IsValid) return View("Edit", clientReadViewModel);

                var clientReadDto =clientReadViewModel.Adapt<ClientReadDto>();
                var clientEdited=await _clientService.UpdateAsync(clientReadDto);
                if (clientEdited.State)
                {
                    TempData["SavingSuccess"] = "تم التعديل ع بيانات العميل بنجاح";

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", clientEdited.Message);
                    return View("Edit",clientReadViewModel);
                }
            }
            catch
            {
                ModelState.AddModelError("", "problem in saving in controller");

                return View("Edit",clientReadViewModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Delete(int  id)
        {
            try
            {
              var resultDeleting=  await _clientService.SoftDeleteAsync(id);
                if (resultDeleting.State)
                {
                    TempData["SavingSuccess"] = "تم حذف العميل بنجاح";
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    TempData["SavingSuccess"] ="عفوا الحذف لم يتم"+ resultDeleting.Message;   
                    return RedirectToAction(nameof(Index));

                }
            }
            catch
            {
                TempData["SavingSuccess"] = "عفوا الحذف لم يتم" + "there is problem in controller";
                return RedirectToAction(nameof(Index));
            }
        }

        
    }
}
