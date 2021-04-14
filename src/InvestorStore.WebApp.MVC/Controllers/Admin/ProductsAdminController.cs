using System.Threading.Tasks;
using InvestorStore.Catalog.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace InvestorStore.WebApp.MVC.Controllers.Admin
{
    public class ProductsAdminController : Controller
    {
        private readonly IProductAppService _productAppService;

        public ProductsAdminController(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        [HttpGet("admin-products")]
        public async Task<IActionResult> Index()
        {
            return View(await _productAppService.GetAll());
        }
    }
}