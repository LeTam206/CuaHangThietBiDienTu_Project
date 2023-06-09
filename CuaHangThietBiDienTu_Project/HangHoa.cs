/* HangHoa.cs
 * Name: Luong Tien Thinh + Nguyen Le Tam
 * Date: 19/06/2022
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HangHoa
{
    //Fields
    private string maHang;
    private string tenHang;
    private string noiSX;
    private string mauSac;
    private int giaBan;
    private DateTime ngayNhapKho;
    private int soLuong;

    //Properties
    public string MaHang
    {
        get { return maHang; }
        set { maHang = value; }
    }
    public string TenHang
    {
        get { return tenHang; }
        set { tenHang = value; }
    }
    public string NoiSX
    {
        get { return noiSX; }
        set { noiSX = value; }
    }
    public string MauSac
    {
        get { return mauSac; }
        set { mauSac = value; }
    }
    public int GiaBan
    {
        get { return giaBan; }
        set { giaBan = value; }
    }
    public DateTime NgayNhapKho
    {
        get { return ngayNhapKho; }
        set { ngayNhapKho = value; }
    }
    public int SoLuong
    {
        get { return soLuong; }
        set { soLuong = value; }
    }

    //Constructors
    public HangHoa(string maHang, string tenHang, string noiSX, string mauSac, int giaBan, DateTime ngayNhapKho, int soLuong)
    {
        this.MaHang = maHang;
        this.TenHang = tenHang;
        this.NoiSX = noiSX;
        this.MauSac = mauSac;
        this.GiaBan = giaBan;
        this.NgayNhapKho = ngayNhapKho;
        this.SoLuong = soLuong;
    }

    //Methods
    public override string ToString()
    {
        return $"Mã hàng: {MaHang}\nTên hàng: {TenHang}\nNơi sản xuất: {NoiSX}\nMàu sắc: {MauSac}\nGiá bán: {GiaBan}\nNgày nhập kho: {NgayNhapKho.ToString("dd/MM/yyyy")}\nSố lượng: {SoLuong}\n";
    }
}