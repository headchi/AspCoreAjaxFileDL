using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;

namespace WebAppAjaxDL.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        [Display(Name = "たいとる")]
        public string Title { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            ViewData["FileName"] = "aiueo.xlsx";
        }

        public void OnPost()
        {

        }



        public FileResult OnPostDownLoad()
        {
            var byteArray = Encoding.ASCII.GetBytes("abbccc");
            return this.File(byteArray, "text/plain", "test.dat");
        }


        public FileResult OnGetDownloadEncryptedFile(string data)
        {
            var byteArray = Encoding.ASCII.GetBytes(data);
            return this.File(byteArray, "text/plain", "test.dat");
        }
        public IActionResult OnGetUpdateCorrectedCount(int memorizeID)
        {
            return new JsonResult(new { Content = "jsonデータですよ" });
        }
        public FileResult OnGetFileDownload(int memorizeID)
        {
            //var byteArray = Encoding.ASCII.GetBytes("abbccc");
            //return this.File(byteArray, System.Net.Mime.MediaTypeNames.Application.Octet, "test.dat");

            var stream = new System.IO.MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Subscribers");

                worksheet.Cells[1, 1].Value = "Name";
                worksheet.Cells[1, 2].Value = "Email";
                worksheet.Cells[1, 3].Value = "Date Subscribed";
                worksheet.Row(1).Style.Font.Bold = true;

                package.Save();
            }

            //string fileName = "Subscribers.xlsx";
            string fileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            stream.Position = 0;
            //return File(stream, fileType, fileName);
            return File(stream, fileType);
        }
    }
}
