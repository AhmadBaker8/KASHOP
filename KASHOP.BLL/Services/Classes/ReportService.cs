using KASHOP.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace KASHOP.BLL.Services.Classes
{
    public class ReportService
    {
        private readonly IProductRepository _productRepository;
        public ReportService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            QuestPDF.Settings.License = LicenseType.Community;
        }
        public QuestPDF.Infrastructure.IDocument GenerateProductReport()
        {
            return Document.Create(container =>
            {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(2, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(20));

                        page.Header()
                        .Text("Baker Shop - Products")
                        .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium);

                        page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(20);
                            var items = _productRepository.GetAllProductsAsync().Result;
                            foreach (var item in items)
                            {
                                x.Item().Text($"{item.Name} - {item.Price} - {item.Quantity} units")
                                .FontSize(20);
                            }
                        });

                        page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                    });
            });

        }
    }
}
