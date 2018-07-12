using GrapeCity.ActiveReports.Document.Section;
using GrapeCity.ActiveReports.SectionReportModel;
using System.Collections;
using System.Data;
using System.Drawing;

namespace Reports
{
    /// <summary>
    /// Summary description for SectionReport1.
    /// </summary>
    public partial class CustomReport : GrapeCity.ActiveReports.SectionReport
    {
        private DataSet _dsSource;
        public string RptFileName = "";
        DataTable dtGLID;

        public CustomReport()
        {
            InitializeComponent();
            getReportData();
            CreateColumnsAndDetailSection();
        }

        private void CreateColumnsAndDetailSection()
        {
            int intCnt = 1;
            float fltLastControlXPosition = 0;
            Hashtable hshColTypes = new Hashtable();
            DataTable dtMaster = _dsSource.Tables[0].Copy();
            dtGLID = _dsSource.Tables[_dsSource.Tables.Count - 1]; // 
            DataView dvMaster;

            dvMaster = dtMaster.DefaultView;
            float fltTotalWidth = 0f;

            SizeF ControlSize;
            Page objPage = new Page();
            objPage.Units = Units.Inches;
            objPage.Font = new Font("Arial", 10, FontStyle.Regular);

            foreach (DataColumn dc in dtMaster.Columns)
            {
                Label lblHeader = new Label();
                Label Header2 = new Label();
                TextBox tbData = new TextBox();
                TextBox tbTotal = new TextBox();

                lblHeader.Text = dc.ColumnName.Trim();
                Header2.Text = "Column Heading Name- Just making it larger than 15 to test the things.";

                ControlSize = objPage.MeasureText(dc.ColumnName);

                ControlSize.Width = 1.5f;

                lblHeader.Width = ControlSize.Width;
                Header2.Width = ControlSize.Width;
                tbData.Width = ControlSize.Width;
                tbTotal.Width = ControlSize.Width;

                lblHeader.Border.BottomStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid;
                lblHeader.Border.LeftStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid;
                lblHeader.Border.RightStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid;
                lblHeader.Border.TopStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid;
                lblHeader.Style = "font-weight: bold; text-align: center; background-color: LightGrey";
                lblHeader.WrapMode = WrapMode.WordWrap;
                lblHeader.Height = 0.78f;


                Header2.Border.BottomStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid;
                Header2.Border.LeftStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid;
                Header2.Border.RightStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid;
                Header2.Border.TopStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid;
                Header2.Style = "font-weight: bold; text-align: center; background-color: LightGrey";
                Header2.WrapMode = WrapMode.WordWrap;
                Header2.Height = 0.78f;


                tbTotal.Border.BottomStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid;
                tbTotal.Border.LeftStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid;
                tbTotal.Border.RightStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid;
                tbTotal.Border.TopStyle = GrapeCity.ActiveReports.BorderLineStyle.Solid;
                tbTotal.Style = "font-weight: bold; text-align: right; background-color: LightGrey";

                lblHeader.Font = new Font("Arial", 10, FontStyle.Bold);
                Header2.Font = new Font("Arial", 10, FontStyle.Italic);
                tbData.Font = new Font("Arial", 10, FontStyle.Regular);
                tbTotal.Font = new Font("Arial", 10, FontStyle.Bold);

                if (intCnt == 1)
                {
                    lblHeader.Location = new PointF(.21f, .08f);
                    Header2.Location = new PointF(.21f, lblHeader.Height);
                    fltLastControlXPosition = .21f + ControlSize.Width;
                    tbData.Location = new PointF(.21f, .06f);
                    fltTotalWidth = ControlSize.Width + .21f;
                }
                else
                {
                    lblHeader.Location = new PointF(fltLastControlXPosition, .08f);
                    Header2.Location = new PointF(fltLastControlXPosition, lblHeader.Height);
                    tbData.Location = new PointF(fltLastControlXPosition, .06f);
                    tbTotal.Location = new PointF(fltLastControlXPosition, .06f);
                    fltLastControlXPosition = lblHeader.Location.X + ControlSize.Width;

                }

                fltTotalWidth += ControlSize.Width;

                tbData.DataField = dc.ColumnName;
                tbTotal.DataField = dc.ColumnName;

                pageHeader.Controls.Add(lblHeader);
                pageHeader.Controls.Add(Header2);

                detail.Controls.Add(tbData);

                tbTotal.SummaryFunc = SummaryFunc.Sum;
                tbTotal.SummaryRunning = SummaryRunning.All;
                tbTotal.SummaryType = SummaryType.GrandTotal;

                tbTotal.OutputFormat = "#,##0.00";
                intCnt++;

            }
            //this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.PageSettings.PaperWidth = fltTotalWidth + 5.0f;
            this.PrintWidth = fltTotalWidth;

            DataView dvdtMaster = dtMaster.DefaultView;
            this.DataSource = dvdtMaster;

        }

        private void getReportData()
        {
            _dsSource = new DataSet();
            DataTable dt = new DataTable();
            for (int i = 0; i < 600; i++)
            {
                dt.Columns.Add("This is a column name greater than 15 charcaters. " + i);
            }
            for (int i = 0; i < 10; i++)
            {
                DataRow dr = dt.NewRow();
                for (int j = 0; j < 600; j++)
                {
                    dr[j] = $"Data {i} {j}";
                }
                dt.Rows.Add(dr);
            }
            _dsSource.Tables.Add(dt);
        }
    }
}
