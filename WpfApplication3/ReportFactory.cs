using System.Collections.Generic;
using WpfApplication3.ViewModel;
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Diagnostics;
using System.Windows;

namespace WpfApplication3
{
    public static class ReportFactory
    {
        private static PrintInvoiceViewModel _model;

        public static string RunReport(PrintInvoiceViewModel model, string fileName,string ReportEmbeddedResource)
        {
            _model = model;

            var lr = new LocalReport();

            lr.ReportEmbeddedResource = ReportEmbeddedResource;
            lr.EnableExternalImages = true;

            lr.SubreportProcessing += Lr_SubreportProcessing;

            lr.DataSources.Add(new ReportDataSource("DataSet1", new[] { _model }));

            var renderedBytes = lr.Render
            (
                "PDF",
                "<DeviceInfo><OutputFormat>PDF</OutputFormat></DeviceInfo>",
                out _,
                out _,
                out _,
                out _,
                out var warnings
            );

            foreach (var w in warnings)
                MessageBox.Show($"{w.Code}\n\n{w.Message}\n\n{w.ObjectName}\n\n{w.ObjectType}");

            var nm = _model.InvoiceNumber.ToString();

            var saveAs = Path.Combine(TempPath, nm + ".pdf");

            var idx = 0;
            while (File.Exists(saveAs))
            {
                idx++;
                saveAs = Path.Combine(TempPath, $"{nm}.{idx}.pdf");
            }

            if (File.Exists(saveAs))
                File.Delete(saveAs);

            using (var stream = new FileStream(saveAs, FileMode.Create, FileAccess.Write))
            {
                stream.Write(renderedBytes, 0, renderedBytes.Length);
                stream.Close();
            }

            //Process.Start(saveAs);

            return saveAs;
        }
        
        private static void Lr_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("DataSet1", _model.Items));
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
