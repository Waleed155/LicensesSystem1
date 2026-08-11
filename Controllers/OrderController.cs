using Licenses.Dto;
using Licenses.Dto.ActivityTypeDto;
using Licenses.Dto.OrderDto;

using Licenses.Services.OrderServices;
using Licenses.ViewModels;
using Licenses.ViewModels.ActivityTypeViewModel;
using Licenses.ViewModels.ClientViewModels;
using Licenses.ViewModels.OrderViewModel;
using Licenses.ViewModels.StepViewModel;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Licenses.Controllers
{
    public class OrderController : Controller
    {
        IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
        _orderService = orderService;
        }
        public async Task <IActionResult> Index(int page =1)
        {
           try {
                var result=await _orderService.GetAllAsync(page,pageSize:15);
                if (!result.State)
                    return View("Error",new ErrorViewModelLicenses("OrderPage",result.Message));
                var resultViewModel = result.Result.Adapt<PagedResult<OrderReadViewModel>>();
                return View(resultViewModel);
            }
            catch 
            {
                return View("Error", new ErrorViewModelLicenses("OrderPage", "Error in Controller"));

            }
        }
        public async Task<IActionResult>GetAllDeleted(int page)
        {
            try
            {
                var result = await _orderService.GetAllDeletedAsync(page, pageSize: 15);
                if (!result.State)
                    return View("Error", new ErrorViewModelLicenses("OrderPage", result.Message));
                //bool b = result.Result.Items.First().IsDeleted;
                var resultViewModel = result.Result
                    .Adapt<PagedResult<OrderReadViewModel>>();
                //bool a = resultViewModel.Items.First().IsDeleted;

                return View(resultViewModel);
            }
            catch
            {
                return View("Error", new ErrorViewModelLicenses("OrderPage", "Error in Controller"));

            }
        }
        public async Task<IActionResult> SearchByDeletedName(string search, int page = 1, int pageSize = 15)
        {
            try
            {
                var result = await _orderService.SearchByDeletedNameAsync(search, page, pageSize);
                if (result.State && result.Result!.TotalPages > 0)
                {
                    var resultViewModel = result.Result.Adapt<PagedResult<OrderReadViewModel>>();
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
        public async Task<IActionResult> Create(OrderAddViewModel orderAddViewModel)
        {
            try 
            {

                if (!ModelState.IsValid)
                {

                    return View(orderAddViewModel);
                }
                var orderAddDto=orderAddViewModel.Adapt<OrderAddDto>();
                var result =await _orderService.AddAsync(orderAddDto);
                var resultViewModel=result.Result.Adapt<OrderReadViewModel>();
                if (!result.State)
                {
                    ModelState.AddModelError("", result.Message);
                    return View(orderAddViewModel);
                }
                else
                {
                    TempData["SavingSuccess"] = "تمت اضافه الطلب بنجاح";
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
        public async Task< IActionResult > Edit(int id)
         {
            var orderReadDto = await _orderService.GetByIdAsync(id);
            var orderReadViewModel=orderReadDto.Result.Adapt<OrderReadViewModel>();
            return View(orderReadViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEditting(OrderReadViewModel orderReadViewModel)
        {
            try
            {
                if (!ModelState.IsValid) { return View("Edit", orderReadViewModel); }
                var orderReadDto = orderReadViewModel.Adapt<OrderReadDto>();
                var result = await _orderService.UpdateAsync(orderReadDto);
                if (!result.State)
                {
                    ModelState.AddModelError("", result.Message);
                    return View("Edit", orderReadViewModel);
                }

                TempData["SavingSuccess"] = $"تم التعديل بنجاح ";
                if(orderReadViewModel.IsDeleted)
                    return RedirectToAction(nameof(GetAllDeleted));

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "there is problem in controller");
                return View(orderReadViewModel);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _orderService.SoftDeleteAsync(id);
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
                var result = await _orderService.Revive(id);
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
