using KASHOP.BLL.Services.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;

namespace KASHOP.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public class ReportsController : ControllerBase
    {
        private readonly ReportService _reportService; 
        public ReportsController(ReportService reportService)
        {
            _reportService = reportService;
        }
        [HttpGet("PdfProduct")]
        public IResult GetPdfProductReport()
        {
            var pdfReport = _reportService.GenerateProductReport().GeneratePdf();
            return Results.File(pdfReport, "application/pdf", "BakerShop_product.pdf");
        }
    }
}