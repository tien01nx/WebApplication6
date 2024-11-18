using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    public class ExcelController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ExcelController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { message = "Please upload a valid Excel file." });
            }

            // Tạo thư mục tạm nếu chưa tồn tại
            string tempFolder = Path.Combine(_webHostEnvironment.WebRootPath, "temp");
            if (!Directory.Exists(tempFolder))
            {
                Directory.CreateDirectory(tempFolder);
            }

            // Tạo tên file tạm và sessionId
            string sessionId = Guid.NewGuid().ToString();
            string tempFileName = $"{sessionId}{Path.GetExtension(file.FileName)}";
            string tempFilePath = Path.Combine(tempFolder, tempFileName);

            // Lưu file vào thư mục tạm
            using (var stream = new FileStream(tempFilePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Lưu đường dẫn file tạm vào Session
            HttpContext.Session.SetString(sessionId, tempFilePath);

            // Đọc dữ liệu cột cần thiết để gửi về client
            var previewData = ReadRequiredColumns(tempFilePath);

            // Trả về JSON cho client
            return Ok(new
            {
                sessionId,
                previewData
            });
        }


        [HttpPost]
        public IActionResult ConfirmSave(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
            {
                return BadRequest(new { message = "Invalid sessionId." });
            }

            // Lấy đường dẫn file từ Session
            string tempFilePath = HttpContext.Session.GetString(sessionId);
            if (string.IsNullOrEmpty(tempFilePath) || !System.IO.File.Exists(tempFilePath))
            {
                return BadRequest(new { message = "File not found or session expired." });
            }

            // Đọc dữ liệu từ file tạm
            var data = ReadRequiredColumns(tempFilePath);

            // Lưu dữ liệu vào database
            //SaveToDatabase(data);

            // Di chuyển file từ thư mục tạm sang thư mục lưu trữ chính thức
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Tạo tên file mới (ví dụ: thêm timestamp vào tên file)
            string newFileName = $"{Path.GetFileNameWithoutExtension(tempFilePath)}_{DateTime.Now:yyyyMMddHHmmss}{Path.GetExtension(tempFilePath)}";

            // Tạo đường dẫn mới cho file
            string finalPath = Path.Combine(uploadsFolder, newFileName);

            // Di chuyển và đổi tên file
            System.IO.File.Move(tempFilePath, finalPath);

            // Xóa session sau khi xử lý
            HttpContext.Session.Remove(sessionId);

            // Trả về thông báo kèm tên file mới (nếu cần)
            return Ok(new { message = "File processed and saved successfully.", newFileName = newFileName });
        }

        private DatabaseData ReadRequiredColumns(string filePath)
        {
            var data = new DatabaseData();
            using (var workbook = new XLWorkbook(filePath))
            {
                var worksheet = workbook.Worksheet("Sheet1"); // Đổi tên nếu cần

                // Đọc dữ liệu từ các ô C3 đến C8
                for (int row = 3; row <= 8; row++)
                {
                    data.A = worksheet.Cell(row, 1).GetValue<string>(); // Cột B
                    data.B = worksheet.Cell(row, 2).GetValue<string>(); // Cột B
                    data.C = worksheet.Cell(row, 3).GetValue<string>(); // Cột C
                    data.D = worksheet.Cell(row, 4).GetValue<string>(); // Cột D
                    data.X = worksheet.Cell(row, 5).GetValue<string>(); // Cột X
                    data.Y = worksheet.Cell(row, 6).GetValue<string>(); // Cột Y
                }
            }
            return data;
        }



    }
}
