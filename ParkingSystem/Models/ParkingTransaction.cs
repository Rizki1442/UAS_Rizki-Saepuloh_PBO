using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.Models
{
    public class ParkingTransaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [Display(Name = "Kendaraan")]
        public Guid VehicleId { get; set; }

        [Required]
        [Display(Name = "Petugas")]
        public Guid OfficerId { get; set; }

        [Display(Name = "Waktu Masuk")]
        public DateTime EntryTime { get; set; } = DateTime.Now;

        [Display(Name = "Waktu Keluar")]
        public DateTime? ExitTime { get; set; }

        [Display(Name = "Durasi (Jam)")]
        public int Duration { get; set; }

        [Display(Name = "Tarif")]
        public decimal Rate { get; set; }

        [Display(Name = "Total Bayar")]
        public decimal TotalFee { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; } = "Masuk";

        // Relasi
        public Vehicle? Vehicle { get; set; }

        public Officer? Officer { get; set; }
    }
}