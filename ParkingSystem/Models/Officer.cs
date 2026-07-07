using System.ComponentModel.DataAnnotations;

namespace ParkingSystem.Models
{
    public class Officer
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Nama petugas wajib diisi")]
        [Display(Name = "Nama Petugas")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nomor telepon wajib diisi")]
        [Display(Name = "Nomor Telepon")]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Shift wajib dipilih")]
        [Display(Name = "Shift")]
        public string Shift { get; set; } = string.Empty;
    }
}