using GrapeCity.ActiveReports.Export.Excel.Section;
namespace Reports
{
    public class Program
    {
        public static int Main(string[] args)
        {
            CustomReport customReport = new CustomReport();
            var objXls = new XlsExport();
            objXls.FileFormat = FileFormat.Xlsx;

            customReport.Run();
            string strExportFN = string.Format("{0}{1}.xlsx", "C:\\CODE\\Report\\", "Custom Report");
            objXls.Export(customReport.Document, strExportFN);
            return 0;
        }
    }
}
