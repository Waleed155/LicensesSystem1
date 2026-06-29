
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
                if (result.State && result.Result.TotalPages > 0)
                {
                 
                   var resultViewModel= result.Result.Adapt<PagedResult<ClientReadViewModel>>(); 
                    return View(resultViewModel);
                }
                {
                    return NotFound(result.Message);
                }
            }catch
            {
                return NotFound("There is problem in controller");

            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>SearchByNameOrNationalId(string search, int page= 1, int pageSize = 15)
        {
            try
            {
                var result = await _clientService.GetByNameOrNationalId(search, page, pageSize);
                if (result.State&& result.Result.TotalPages>0) {
                    var resultViewModel = result.Result.Adapt<PagedResult<ClientReadViewModel>>();
                    ViewBag.search = search;
                    return View("Index",resultViewModel);

                }else
                {
                    TempData["NoClients"] = result.Message;
                     return RedirectToAction("Index");
                }

            }
            catch
            {
                TempData["NoClients"] = "there is problem in controller";
                return RedirectToAction("Index");
            }
        }

        // GET: ClientController/Details/5
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
                return RedirectToAction("Error", "Home");

            }
            
        }
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
                    return NotFound(result.Message);
                }
            }
            catch
            {
               return RedirectToAction("Error", "Home");
            }
        }

        // GET: ClientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientController/Create
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
                        TempData["SuccessMessage"] = clientAdded.Message;

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
                ModelState.AddModelError("", "there is Problem in controller");
                return RedirectToAction(nameof(Index));
            }

         }
        

        // GET: ClientController/Edit/5
        public   async Task< IActionResult> Edit(int id)
        {
            try
            {
                var clientEditDto =await  _clientService.GetByIdAsync(id);
                if (clientEditDto.State&& clientEditDto.Result!=null)
                {
                    var clientReadViewModel = clientEditDto.Result.Adapt<ClientReadViewModel>();
                    return View( clientReadViewModel);
                }
                else
                {
                    return NotFound();

                }
            }
            catch 
            {
                ModelState.AddModelError("", "there is Problem in controller");
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Edit(ClientReadViewModel clientReadViewModel)
        {
            try
            {
                var clientReadDto=clientReadViewModel.Adapt<ClientReadDto>();
                var clientEdited=await _clientService.UpdateAsync(clientReadDto);
                if (clientEdited.State)
                {
                    TempData["SuccessMessage"] = clientEdited.Message;

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(clientReadViewModel);
                }
            }
            catch
            {
                return View(clientReadViewModel);
            }
        }

        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Delete(int  id)
        {
            try
            {
              var resultDeleting=  await _clientService.SoftDeleteAsync(id);
                if (resultDeleting.State)
                {
                    TempData["DeleteMessage"] = "تم حذف العميل بنجاح";
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    TempData["DeleteMessage"] = resultDeleting.Message;   
                    return RedirectToAction(nameof(Index));

                }
            }
            catch
            {
                TempData["DeleteMessage"] = "there is problem in controller";
                return RedirectToAction(nameof(Index));
            }
        }

        
    }
}
