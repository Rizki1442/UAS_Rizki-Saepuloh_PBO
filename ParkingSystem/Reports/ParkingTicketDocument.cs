using ParkingSystem.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ParkingSystem.Reports
{
    public class ParkingTicketDocument : IDocument
    {
        private readonly ParkingTransaction _transaction;

        public ParkingTicketDocument(ParkingTransaction transaction)
        {
            _transaction = transaction;
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

                page.Content().Column(column =>
                {
                    column.Item().Text("PARKING TICKET")
                        .FontSize(20)
                        .Bold();

                    column.Item().Text("--------------------------------");

                    column.Item().Text($"No Polisi : {_transaction.Vehicle?.PlateNumber}");

                    column.Item().Text($"Pemilik : {_transaction.Vehicle?.OwnerName}");

                    column.Item().Text($"Petugas : {_transaction.Officer?.Name}");

                    column.Item().Text($"Masuk : {_transaction.EntryTime:dd/MM/yyyy HH:mm}");

                    column.Item().Text("--------------------------------");

                    column.Item().Text("Simpan tiket ini.");
                });
            });
        }
    }
}