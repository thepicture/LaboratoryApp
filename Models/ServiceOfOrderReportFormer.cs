using System.Collections.Generic;
using System.Linq;
using Word = Microsoft.Office.Interop.Word;

namespace LaboratoryApp.Models
{
    /// <summary>
    /// The type for forming a ServiceOfOrder report.
    /// </summary>
    public class ServiceOfOrderReportFormer : IReportFormer
    {
        public void Form(string destinationPath, bool isVisible)
        {
            Word.Application app = new Word.Application();
            Word.Document doc = app.Documents.Add();

            List<ServiceOfOrder> servicesOfOrder = AppContext.GetInstance().ServiceOfOrder.ToList();

            foreach (ServiceOfOrder currentServiceOfOrder in servicesOfOrder)
            {
                Word.Paragraph pGraph = doc.Paragraphs.Add();
                Word.Range pRange = pGraph.Range;
                pRange.Text = currentServiceOfOrder.Service.Name;
                pRange.InsertParagraphAfter();

                Word.Paragraph tableParagraph = doc.Paragraphs.Add();
                Word.Range tRange = tableParagraph.Range;
                Word.Table servicesTable = doc.Tables.Add(tRange, servicesOfOrder.Count + 1, 3);

                servicesTable.Borders.InsideLineStyle = servicesTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                servicesTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                Word.Range cellRange;

                cellRange = servicesTable.Cell(1, 1).Range;
                cellRange.Text = "Биоматериал";

                cellRange = servicesTable.Cell(1, 2).Range;
                cellRange.Text = "ФИО пациента";

                cellRange = servicesTable.Cell(1, 3).Range;
                cellRange.Text = "Общая цена";

                servicesTable.Rows[1].Range.Bold = 1;
                servicesTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                for (int i = 0; i < servicesOfOrder.Count(); i++)
                {
                    ServiceOfOrder currentForwardingOrder = servicesOfOrder[i];

                    cellRange = servicesTable.Cell(i + 2, 1).Range;
                    cellRange.Text = currentServiceOfOrder.Service.Name;
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                    cellRange = servicesTable.Cell(i + 2, 2).Range;
                    cellRange.Text = currentServiceOfOrder.Order.User.FullName;
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                    cellRange = servicesTable.Cell(i + 2, 3).Range;
                    cellRange.Text = currentServiceOfOrder.Order.GetPriceSum + " руб.";
                    cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                    if (currentForwardingOrder != servicesOfOrder.LastOrDefault())
                    {
                        doc.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);
                    }
                }

                if (isVisible)
                {
                    app.Visible = true;
                }

                doc.SaveAs(destinationPath);
                doc.SaveAs(destinationPath, Word.WdExportFormat.wdExportFormatPDF);
            }
        }
    }
}
