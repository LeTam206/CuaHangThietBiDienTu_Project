/* DonHang.cs
 * Name: Luong Tien Thinh + Nguyen Le Tam
 * Date: 19/06/2022
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DonHang
{
    //Fields
    private static int stt = 0;
    private string[] maHang;
    private int[] soLuong;
    private int tongTien;
    private string tenKhachHang;
    private string diaChiKhachHang;
    private string soDienThoai;
    private DateTime ngayDatHang;

    //Properties
    public int Stt 
    { 
        get { return stt; }
        set { stt = value; }
    }
    internal string[] MaHang
    {
        get { return maHang; }
        set { maHang = value; }
    }
    internal int[] SoLuong 
    { 
        get { return soLuong; }
        set { soLuong = value; }
    }
    public int TongTien
    {
        get { return tongTien; }
        set { tongTien = value; }
    }
    public string TenKhachHang
    {
        get { return tenKhachHang; }
        set { tenKhachHang = value; }
    }
    public string DiaChiKhachHang
    {
        get { return diaChiKhachHang; }
        set { diaChiKhachHang = value; }
    }
    public string SoDienThoai
    {
        get { return soDienThoai; }
        set { soDienThoai = value; }
    }
    public DateTime NgayDatHang
    {
        get { return ngayDatHang; }
        set { ngayDatHang = value; }
    }

    //Constructors
    public DonHang(string[] maHang, int[] soLuong, int tongTien, string tenKhachHang, string diaChiKhachHang, string soDienThoai, DateTime ngayDatHang)
    {
        this.MaHang = maHang;
        this.SoLuong = soLuong;
        this.TongTien = tongTien;
        this.TenKhachHang = tenKhachHang;
        this.DiaChiKhachHang = diaChiKhachHang;
        this.SoDienThoai = soDienThoai;
        this.NgayDatHang = ngayDatHang;
        this.Stt++;
    }

    //Methods
    public override string ToString()
    {
        string s = $"Stt đơn hàng: {Stt}\n";
        for (int i = 0; i < MaHang.Length; i++)
        {
            s += $"Mặt hàng {i+1}: Mã hàng: {MaHang[i]}\tSố lượng: {SoLuong[i]}\n";
        }
        s += $"Tổng tiền: {TongTien}\nTên khách hàng: {TenKhachHang}\nĐịa chỉ: {DiaChiKhachHang}\nSđt: {SoDienThoai}\nNgày đặt hàng: {NgayDatHang.ToString("dd/MM/yyyy")}\n";
        return s;
    }
}