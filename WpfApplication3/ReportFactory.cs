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

        public static string RunReport(PrintInvoiceViewModel model, string FileName, string EmbeddedResource, string DataSource)
        {
            _model = model;

            using (var lr = new LocalReport())
            {
                lr.ReportEmbeddedResource = EmbeddedResource;
                lr.EnableExternalImages = true;

                lr.SubreportProcessing += Lr_SubreportProcessing;

                lr.DataSources.Add(new ReportDataSource(DataSource, new[] { _model }));

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
                string nm = FileName;

                var saveAs = Path.Combine(TempPath, nm + ".pdf");

                if (File.Exists(saveAs))
                {
                    File.Delete(saveAs);
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
            e.DataSources.Add(new ReportDataSource("InvoiceLineDataset", _model.Items));
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
