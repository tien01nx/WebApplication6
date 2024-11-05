using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DemoContext _context;

        public HomeController(ILogger<HomeController> logger, DemoContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = _context.UserLogins.ToList();
            return Json(new { data = products });
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[HttpPost]
        //public async Task<bool> CreateOrUpdate([FromBody] UserLogin data)
        //{
        //    UserLogin material = _repo.GetByID(data.Id) ?? new Material();

        //    bool check = _repo.GetAll().Any(p => p.MaterialCode.ToLower() == data.MaterialCode.ToLower() && p.Id != material.Id);
        //    if (check) return false;

        //    material.MaterialGroupId = data.MaterialGroupId;
        //    material.FinishProductTypeId = data.FinishProductTypeId;
        //    material.MaterialCode = data.MaterialCode;
        //    material.MaterialName = data.MaterialName;
        //    material.ImportedMaterial = data.ImportedMaterial ?? "";
        //    material.CustomerMaterials = data.CustomerMaterials ?? "";
        //    material.SupplierMaterials = data.SupplierMaterials;
        //    material.ImportPrice = data.ImportPrice ?? 0;
        //    material.ExportPrice = data.ExportPrice ?? 0;
        //    material.Quantity = data.Quantity ?? 0;
        //    material.MinQuantity = data.MinQuantity ?? 0;
        //    material.Unit = data.Unit;
        //    material.Note = data.Note ?? "";
        //    material.UnitManufacture = data.UnitManufacture ?? "";

        //    if (material.Id > 0) await _repo.UpdateAsync(material);
        //    else await _repo.CreateAsync(material);

        //    return true;
        //}

        //public async Task<bool> Delete(int Id)
        //{
        //    bool isCheck = _productBOMRepo.GetAll().Any(p => p.MaterialId == Id && p.ProductGroupType == "Nguyên vật liệu");
        //    if (isCheck) return false;

        //    _repo.Delete(Id);
        //    return true;
        //}

        //public async Task<FileResult> ExportExcel()
        //{
        //    var result = _repo.GetAll();
        //    string[] colName = { "Mã nhóm vật liệu", "Mã sản phẩm hoàn thiện", "Mã vật liệu", "Tên vật liệu", "Nguyên liệu nhập gia công", "Tên NVL của khách hàng", "Tên NVL của NCC", "Giá Nhập", "Giá bán", "Số lượng", "Số lượng tối thiểu", "Đơn Vị", "Ghi chú" };
        //    string[] colValue = { "MaterialGroupId", "FinishProductTypeId", "MaterialCode", "MaterialName", "ImportedMaterial", "CustomerMaterials", "SupplierMaterials", "ImportPrice", "ExportPrice", "Quantity", "MinQuantity", "Unit", "Note" };
        //    var (contentFile, contentType, fileName) = Excel.GenerateExcel("Material.xlsx", result.ToList(), colName, colValue);
        //    return File(contentFile,
        //                contentType,
        //                fileName);
        //}

        //public JsonResult GetAllToList()
        //{
        //    return Json(_repo.GetAll());
        //}
        //public JsonResult GetById(int Id)
        //{
        //    Material result = _repo.GetByID(Id);
        //    if (result == null)
        //    {
        //        throw new Exception("Id does not exist!");
        //    }
        //    return Json(result);
        //}

    }
}
