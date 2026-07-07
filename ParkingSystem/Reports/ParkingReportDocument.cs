using ParkingSystem.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ParkingSystem.Reports
{
    public class ParkingReportDocument : IDocument
    {
        private readonly List<ParkingTransaction> _transactions;

        public ParkingReportDocument(List<ParkingTransaction> transactions)
        {
            _transactions = transactions;
        }

        public DocumentMetadata GetMetadata()
        {
            return DocumentMetadata.Default;
        }

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(20);

                page.Header().Text("LAPORAN PARKIR")
                    .FontSize(20)
                    .Bold()
                    .AlignCenter();

                page.Content().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    table.Header(header =>
                    {
                        header.Cell().Text("No Polisi").Bold();
                        header.Cell().Text("Petugas").Bold();
                        header.Cell().Text("Masuk").Bold();
                        header.Cell().Text("Keluar").Bold();
                        header.Cell().Text("Total").Bold();
                    });

                    foreach (var item in _transactions)
                    {
                        table.Cell().Text(item.Vehicle?.PlateNumber ?? "-");
                        table.Cell().Text(item.Officer?.Name ?? "-");
                        table.Cell().Text(item.EntryTime.ToString("dd/MM/yyyy HH:mm"));
                        table.Cell().Text(item.ExitTime?.ToString("dd/MM/yyyy HH:mm") ?? "-");
                        table.Cell().Text($"Rp {item.TotalFee:N0}");
                    }
                });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Generated : ");
                        x.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                    });
            });
        }
    }
}
