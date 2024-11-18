using WebApplication6.Models.db;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<Demo2Context>();
builder.Services.AddAuthentication("Cookies")
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });
//builder.Services.AddSession();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian timeout của Session
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();
// Lấy đường dẫn thư mục tạm
string tempFolderPath = Path.Combine(app.Environment.WebRootPath, "temp");

// Xóa tất cả các file trong thư mục tạm khi ứng dụng khởi động
ClearTempFiles(tempFolderPath);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
// Hàm xóa các file trong thư mục tạm
void ClearTempFiles(string tempFolder)
{
    if (Directory.Exists(tempFolder))
    {
        var tempFiles = Directory.GetFiles(tempFolder);

        foreach (var file in tempFiles)
        {
            try
            {
                File.Delete(file); // Xóa file
            }
            catch (Exception ex)
            {
                // Ghi log lỗi nếu cần
                Console.WriteLine($"Error deleting file {file}: {ex.Message}");
            }
        }
    }
}