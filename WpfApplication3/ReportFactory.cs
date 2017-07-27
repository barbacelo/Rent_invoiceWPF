using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApplication3.ViewModel;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Diagnostics;

namespace WpfApplication3
{
    public static class ReportFactory
    {
        private static PrintInvoiceViewModel _model;

        private static string _mimeType;
        private static string _encoding;
        private static string _fileNameExtension;
        private static Warning[] _warnings;
        private static string[] _streams;

        public static string RunReport(PrintInvoiceViewModel model)
        {
            _model = model;

            using (var lr = new LocalReport())
            {
                lr.ReportEmbeddedResource = "WpfApplication3.Reports.InvoiceReport.rdlc";
                lr.EnableExternalImages = true;

                lr.SubreportProcessing += Lr_SubreportProcessing;

                lr.DataSources.Add(new ReportDataSource("DataSource", new[] { _model }));

                var renderedBytes = lr.Render
                    (
                        "PDF",
                        "<DeviceInfo><OutputFormat>PDF</OutputFormat></DeviceInfo>",
                        out _mimeType,
                        out _encoding,
                        out _fileNameExtension,
                        out _streams,
                        out _warnings
                    );
                const string nm = "Invoice Report";

                var saveAs = Path.Combine(TempPath, nm + ".pdf");

                var idx = 0;
                while (File.Exists(saveAs))
                {
                    idx++;
                    saveAs = Path.Combine(TempPath, string.Format("{0}.{1}.pdf", nm, idx));
                }

                using (var stream = new FileStream(saveAs, FileMode.Create, FileAccess.Write))
                {
                    stream.Write(renderedBytes, 0, renderedBytes.Length);
                    stream.Close();
                }

                Process.Start(saveAs);

                return saveAs;
            }
        }
        private static void Lr_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            var invoiceId = int.Parse(e.Parameters[0].Values[0]);
            e.DataSources.Add(new ReportDataSource("SubDataSet", _model.Items));
        }

        private static string TempPath
        {
            get
            {
                var dir = Path.Combine(Path.GetTempPath(), "Stampa");

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                return dir;
            }
        }
    }
}
