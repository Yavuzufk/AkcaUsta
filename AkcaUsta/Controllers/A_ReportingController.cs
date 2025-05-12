using AkcaUsta.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using QuestPDF.Fluent;

namespace AkcaUsta.Controllers
{
    public class A_ReportingController : Controller
    {
        private readonly IServiceRandevuDal _serviceRandevuDal;
        private readonly IMapper _mapper;

        public A_ReportingController(IServiceRandevuDal serviceRandevuDal, IMapper mapper)
        {
            _serviceRandevuDal = serviceRandevuDal;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        // EXCEL: Son 1 Aylık Randevular
        public async Task<IActionResult> ExportRandevuReportExcel()
        {
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Randevu Raporu");

            // Başlıkları yazıyoruz
            ws.Cell(1, 1).Value = "Ad";
            ws.Cell(1, 2).Value = "Mail";
            ws.Cell(1, 3).Value = "Hizmet";
            ws.Cell(1, 4).Value = "Tarih";

            // Asenkron veri çekme
            var data = await _serviceRandevuDal.GetLastMonthRandevus(); // Asenkron metod

            // Verileri yazıyoruz
            for (int i = 0; i < data.Count; i++)
            {
                ws.Cell(i + 2, 1).Value = data[i].Name;
                ws.Cell(i + 2, 2).Value = data[i].Mail;
                ws.Cell(i + 2, 3).Value = data[i].Service;
                ws.Cell(i + 2, 4).Value = data[i].Date.ToString("dd.MM.yyyy");
            }

            // MemoryStream oluşturuluyor
            using (var stream = new MemoryStream())
            {
                wb.SaveAs(stream);
                stream.Position = 0; // Stream pozisyonunu sıfırlıyoruz
                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RandevuRapor.xlsx");
            }
        }

        public async Task<FileResult> ExportRandevuReportWord()
        {
            using var stream = new MemoryStream();
            using (var wordDoc = WordprocessingDocument.Create(stream, DocumentFormat.OpenXml.WordprocessingDocumentType.Document, true))
            {
                var mainPart = wordDoc.AddMainDocumentPart();
                mainPart.Document = new DocumentFormat.OpenXml.Wordprocessing.Document();
                var body = new Body();

                body.Append(new Paragraph(new Run(new Text("Randevu Raporu (Son 1 Ay)"))));

                var data = await _serviceRandevuDal.GetLastMonthRandevus();

                foreach (var item in data)
                {
                    var paragraph = new Paragraph();
                    paragraph.Append(new Run(new Text($"Ad: {item.Name}, Mail: {item.Mail}, Hizmet: {item.Service}, Tarih: {item.Date:dd.MM.yyyy}")));
                    body.Append(paragraph);
                }

                mainPart.Document.Append(body);
                mainPart.Document.Save();
            }

            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "RandevuRapor.docx");
        }

        public async Task<FileResult> ExportRandevuReportPdf()
        {
            var data = await _serviceRandevuDal.GetLastMonthRandevus();

            var document = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Content()
                        .Column(col =>
                        {
                            col.Item().Text("Randevu Raporu (Son 1 Ay)").Bold().FontSize(16).Underline();
                            col.Item().Text("");

                            foreach (var item in data)
                            {
                                col.Item().Text($"Ad: {item.Name}, Mail: {item.Mail}, Hizmet: {item.Service}, Tarih: {item.Date:dd.MM.yyyy}");
                            }
                        });
                });
            });

            var stream = new MemoryStream();
            document.GeneratePdf(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/pdf", "RandevuRapor.pdf");
        }
    }
}
