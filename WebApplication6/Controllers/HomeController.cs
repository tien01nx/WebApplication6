using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly DemoContext _context = new DemoContext();
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }


        public JsonResult Test()
        {
            var item = _context.Roles.ToList();
            return Json(item);
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


        //        string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        //        string productPath = @"images\products\product-" + productVM.Product.Id;
        //        string finalPath = Path.Combine(wwwRootPath, productPath);
        //                            if (!Directory.Exists(finalPath))
        //                            {
        //                                Directory.CreateDirectory(finalPath);
        //                            }
        //                            using (var fileStream = new FileStream(Path.Combine(finalPath, filename), FileMode.Create))
        //                            {
        //                                file.CopyTo(fileStream);

        //}
        //ProductImage productImage = new ProductImage()
        //{
        //    ImageUrl = @"\" + productPath + @"\" + filename,
        //    ProductId = productVM.Product.Id
        //};


        [HttpPost]
        public async Task<JsonResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("File", "Please upload a valid Excel file.");
                return null;
            }
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string userPath = @"uploads\files\username\852265\" + DateTime.Now.ToString("yyyyMMdd");
            string finalPath = Path.Combine(wwwRootPath, userPath);
            if (!Directory.Exists(finalPath))
            {
                Directory.CreateDirectory(finalPath);
            }
            using (var fileStream = new FileStream(Path.Combine(finalPath, filename), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            var demo = ReadAndWriteExcel(finalPath + "\\" + filename, userPath, filename);
            return Json(new { success = true, message = "File uploaded successfully!", data = demo });
        }

        public async Task<IActionResult> DownloadFile(string uniqueFilename, string originalFilename = "application_form.xlsx")
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string path = "uploads\\files\\username\\852265\\20241107";
            string filePath = Path.Combine(wwwRootPath, path, DateTime.Now.ToString("yyyyMMdd"), uniqueFilename);

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            // Trả về file với tên gốc khi tải về
            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, "application/octet-stream", originalFilename);
        }

        public ExcelData ReadAndWriteExcel(string filePath, string userPath, string fileName)
        {
            ExcelData result = new ExcelData();
            // Mở workbook từ file hiện có
            using (var workbook = new XLWorkbook(filePath))
            {
                // Chọn worksheet hiện có mà bạn muốn chỉnh sửa
                var worksheet = workbook.Worksheet("Sheet1"); // Đổi tên nếu cần

                // Đọc dữ liệu từ một số ô nhất định
                result.A = worksheet.Cell("B8").GetString();
                result.B = worksheet.Cell("B9").GetString();
                result.C = worksheet.Cell("B10").GetString();
                result.D = worksheet.Cell("B11").GetString();
                result.X = worksheet.Cell("B12").GetString();
                result.Y = worksheet.Cell("B13").GetString();
                result.Path = userPath;
                result.FileName = fileName;
                // Chỉ ghi vào ô mong muốn, dữ liệu ô khác không bị thay đổi
                worksheet.Cell("B14").Value = "Tiennv";
                worksheet.Cell("B15").Value = DateTime.Now;
                // Lưu workbook với các thay đổi
                workbook.SaveAs(filePath);
            }
            return result;
        }
        public class ExcelData
        {
            public string A { get; set; }
            public string B { get; set; }
            public string C { get; set; }
            public string D { get; set; }
            public string X { get; set; }
            public string Y { get; set; }
            public string Path { get; set; }
            public string FileName { get; set; }
        }
    }
}
