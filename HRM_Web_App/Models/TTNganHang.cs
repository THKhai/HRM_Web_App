using System.ComponentModel.DataAnnotations;

namespace HRM_Web_App.Models;

public class TTNganHang
{
    [Required]
    public string STKNganHang { get; set; }

    [Required]
    public string TenNganHang { get; set; }

    [Required]
    public string ChiNhanh { get; set; }

    public bool Active { get; set; } = false;

    public string NhanVienID { get; set; }

    public NhanVien NhanVienSoHuu { get; set; }

}