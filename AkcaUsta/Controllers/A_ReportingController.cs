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

        #region EXCEL Raporu - Son 1 Aylık Randevular

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

            var headerRange = ws.Range(1, 1, 1, 4);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.SkyBlue;
            headerRange.Style.Font.FontColor = XLColor.White;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            int row = 2;

            foreach (var item in data)
            {
                ws.Cell(row, 1).Value = item.Name;
                ws.Cell(row, 2).Value = item.Mail;
                ws.Cell(row, 3).Value = item.Service;
                ws.Cell(row, 4).Value = item.Date.ToString("dd.MM.yyyy");

                // Zebra stil: çift satırlara farklı arka plan rengi uygula
                if (row % 2 == 0)
                {
                    ws.Range(row, 1, row, 4).Style.Fill.BackgroundColor = XLColor.LightCyan;
                }
                else
                {
                    ws.Range(row, 1, row, 4).Style.Fill.BackgroundColor = XLColor.White;
                }

                ws.Range(row, 1, row, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Range(row, 1, row, 4).Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                row++;
            }

            // Kolon genişliklerini otomatik ayarla
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

            // Başlıklar
            ws.Cell(1, 1).Value = "Durum";
            ws.Cell(1, 2).Value = "Sayı";

            ws.Range("A1:B1").Style
                .Font.SetBold()
                .Font.SetFontColor(XLColor.White)
                .Fill.SetBackgroundColor(XLColor.DarkBlue)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            // Veri satırları
            ws.Cell(2, 1).Value = "Toplam";
            ws.Cell(2, 2).Value = stats.TotalCount;

            ws.Cell(3, 1).Value = "Onaylanmış";
            ws.Cell(3, 2).Value = stats.ConfirmedCount;

            ws.Cell(4, 1).Value = "Beklemede";
            ws.Cell(4, 2).Value = stats.PendingCount;

            // Zebra arka plan stili
            for (int i = 2; i <= 4; i++)
            {
                var row = ws.Row(i);
                if (i % 2 == 0)
                    row.Style.Fill.BackgroundColor = XLColor.LightBlue;
                else
                    row.Style.Fill.BackgroundColor = XLColor.WhiteSmoke;

                row.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                row.Style.Font.FontSize = 12;
            }

            // Tablonun kenarlıklarını ayarla
            ws.Range("A1:B4").Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            ws.Range("A1:B4").Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            // Kolon genişliklerini ayarla
            ws.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            wb.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "RandevuDurumRaporu.xlsx");
        }




        #endregion

        #region EXCEL Raporu - İş İstatistik Özeti Raporu

        public async Task<IActionResult> ExportBusinessSummaryToExcel()
        {
            var summary = await _serviceRandevuDal.GetBusinessSummaryReport();

            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("İş Özeti Raporu");

            int row = 1;

            // Başlık
            ws.Cell(row, 1).Value = "İş İstatistik Özeti";
            ws.Range(row, 1, row, 2).Merge();
            ws.Row(row).Style
                .Font.SetBold()
                .Font.SetFontSize(16)
                .Font.SetFontColor(XLColor.White)
                .Fill.SetBackgroundColor(XLColor.DarkBlue)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            row += 2;

            // Toplam randevu sayısı
            ws.Cell(row, 1).Value = "Toplam Randevu Sayısı";
            ws.Cell(row, 2).Value = summary.TotalAppointments;
            ws.Range(row, 1, row, 2).Style.Font.SetBold().Font.SetFontColor(XLColor.Black);
            row += 2;

            // Hizmet dağılımı başlığı
            ws.Cell(row, 1).Value = "Hizmetlere Göre Dağılım";
            ws.Range(row, 1, row, 2).Merge();
            ws.Row(row).Style
                .Font.SetBold()
                .Font.SetFontColor(XLColor.White)
                .Fill.SetBackgroundColor(XLColor.CornflowerBlue)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            row++;

            // Hizmet dağılımı tablo başlıkları
            ws.Cell(row, 1).Value = "Hizmet";
            ws.Cell(row, 2).Value = "Sayı";
            ws.Range(row, 1, row, 2).Style
                .Font.SetBold()
                .Fill.SetBackgroundColor(XLColor.LightGray);
            row++;

            // Hizmet dağılımı verileri (zebra stil)
            bool isEven = true;
            foreach (var item in summary.AppointmentsByService)
            {
                ws.Cell(row, 1).Value = item.Key;
                ws.Cell(row, 2).Value = item.Value;

                var bgColor = isEven ? XLColor.White : XLColor.LightBlue;
                ws.Range(row, 1, row, 2).Style.Fill.BackgroundColor = bgColor;
                isEven = !isEven;
                row++;
            }

            row += 2;

            // En popüler hizmet
            ws.Cell(row, 1).Value = "En Popüler Hizmet";
            ws.Cell(row, 2).Value = summary.MostPopularService;
            ws.Range(row, 1, row, 2).Style.Font.SetBold();
            row++;

            // En popüler gün
            ws.Cell(row, 1).Value = "En Tercih Edilen Gün";
            ws.Cell(row, 2).Value = summary.MostPopularDay;
            ws.Range(row, 1, row, 2).Style.Font.SetBold();

            // Kenarlıklar ve kolon ayarları
            ws.RangeUsed().Style.Border
                .OutsideBorder = XLBorderStyleValues.Thin;
            ws.RangeUsed().Style.Border
                .InsideBorder = XLBorderStyleValues.Thin;

            ws.Columns().AdjustToContents();

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
