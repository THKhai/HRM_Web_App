using HRM_Web_App.Models;
using Microsoft.EntityFrameworkCore;

namespace HRM_Web_App.Data;

public class NhanVienContext : DbContext
{
    public NhanVienContext(DbContextOptions<NhanVienContext> options) : base(options) { }

    public DbSet<NhanVien> NhanViens { get; set; }
}