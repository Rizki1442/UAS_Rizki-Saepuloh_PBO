using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.Models
{
    public class Vehicle
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Nomor polisi wajib diisi")]
        [Display(Name = "Nomor Polisi")]
        [StringLength(15)]
        public string PlateNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nama pemilik wajib diisi")]
        [Display(Name = "Nama Pemilik")]
        [StringLength(100)]
        public string OwnerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Jenis kendaraan wajib dipilih")]
        [Display(Name = "Jenis Kendaraan")]
        public string VehicleType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Warna kendaraan wajib diisi")]
        [Display(Name = "Warna")]
        [StringLength(50)]
        public string Color { get; set; } = string.Empty;
    }
}