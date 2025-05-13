using AkcaUsta.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using QuestPDF.Infrastructure;


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

            // QuestPDF lisansı burada tanımlanabilir (özellikle test/prod ayrımı yoksa)
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region PDF Raporu - Son 1 Aylık Randevular

        public async Task<IActionResult> ExportRandevuReportExcel()
        {
            var data = await _serviceRandevuDal.GetLastMonthRandevus();

            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Son 1 Ay Randevular");

            // Başlıklar
            ws.Cell(1, 1).Value = "Ad";
            ws.Cell(1, 2).Value = "Mail";
            ws.Cell(1, 3).Value = "Hizmet";
            ws.Cell(1, 4).Value = "Tarih";

            ws.Range(1, 1, 1, 4).Style.Font.Bold = true;
            ws.Range(1, 1, 1, 4).Style.Fill.BackgroundColor = XLColor.LightGray;

            int row = 2;

            foreach (var item in data)
            {
                ws.Cell(row, 1).Value = item.Name;
                ws.Cell(row, 2).Value = item.Mail;
                ws.Cell(row, 3).Value = item.Service;
                ws.Cell(row, 4).Value = item.Date.ToString("dd.MM.yyyy");
                row++;
            }

            // Kolon genişliklerini ayarla
            ws.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            wb.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "RandevuRapor.xlsx");
        }

        #endregion

        #region EXCEL Raporu - Toplam Randevu Sayısı ve Durumlarına Göre Dağılım

        public async Task<IActionResult> ExportRandevuStatusReportExcel()
        {
            var stats = await _serviceRandevuDal.GetRandevuStatistics();

            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Randevu Durumları");

            ws.Cell(1, 1).Value = "Durum";
            ws.Cell(1, 2).Value = "Sayı";

            ws.Cell(2, 1).Value = "Toplam";
            ws.Cell(2, 2).Value = stats.TotalCount;

            ws.Cell(3, 1).Value = "Onaylanmış";
            ws.Cell(3, 2).Value = stats.ConfirmedCount;

            ws.Cell(4, 1).Value = "Beklemede";
            ws.Cell(4, 2).Value = stats.PendingCount;

            using var stream = new MemoryStream();
            wb.SaveAs(stream);
            stream.Position = 0;
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RandevuDurumRaporu.xlsx");
        }


        #endregion

        #region Excel Raporu - İş İstatistik Özeti Raporu

        public async Task<IActionResult> ExportBusinessSummaryToExcel()
        {
            var summary = await _serviceRandevuDal.GetBusinessSummaryReport();

            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("İş Özeti Raporu");

            int row = 1;

            // Başlık
            ws.Cell(row, 1).Value = "İş İstatistik Özeti";
            ws.Cell(row, 1).Style.Font.Bold = true;
            ws.Cell(row, 1).Style.Font.FontSize = 14;
            row += 2;

            // Toplam randevu sayısı
            ws.Cell(row, 1).Value = "Toplam Randevu Sayısı";
            ws.Cell(row, 2).Value = summary.TotalAppointments;
            row += 2;

            // Hizmet dağılımı başlığı
            ws.Cell(row, 1).Value = "Hizmetlere Göre Dağılım";
            ws.Cell(row, 1).Style.Font.Bold = true;
            row++;

            // Hizmet dağılımı verileri
            ws.Cell(row, 1).Value = "Hizmet";
            ws.Cell(row, 2).Value = "Sayı";
            ws.Range(row, 1, row, 2).Style.Font.Bold = true;
            row++;

            foreach (var item in summary.AppointmentsByService)
            {
                ws.Cell(row, 1).Value = item.Key;
                ws.Cell(row, 2).Value = item.Value;
                row++;
            }

            row += 2;

            // En popüler hizmet
            ws.Cell(row, 1).Value = "En Popüler Hizmet";
            ws.Cell(row, 2).Value = summary.MostPopularService;
            row++;

            // En popüler gün
            ws.Cell(row, 1).Value = "En Tercih Edilen Gün";
            ws.Cell(row, 2).Value = summary.MostPopularDay;

            // Otomatik kolon genişliği
            ws.Columns().AdjustToContents();

            // Excel dosyasını stream'e yaz
            using var stream = new MemoryStream();
            wb.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "IsOzetiRaporu.xlsx");
        }


        #endregion
    }
}
