/* Program.cs
 * Name: Luong Tien Thinh + Nguyen Le Tam
 * Date: 22/07/2022
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CTDL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Viết tiếng việt trong console
            Console.OutputEncoding = Encoding.UTF8;

            //Danh sách hàng hóa
            LinkedList<HangHoa> hangHoa;
            DocHangHoaTuFile(out hangHoa);

            //Danh sách đơn hàng
            LinkedList<DonHang> donHang;
            DocDonHangTuFile(out donHang);

            //Danh sách tài khoản quản lý
            LinkedList<Admin> admin;
            DocTaiKhoanTuFile(out admin);

            //Chương trình chính
            ConsoleKeyInfo ki;
            do
            {
                MenuChinh();
                Console.Write("Chọn chức năng: ");
                char menuChinh = char.Parse(Console.ReadLine());

                switch (menuChinh)
                {
                    //Hiển thị thông tin hàng hóa
                    case '1':
                        Console.Clear();
                        HienThiThongTinHangHoa(hangHoa);
                        break;

                    //Tìm hàng hóa theo tên
                    case '2':
                        Console.Clear();
                        TimKiemThongTinHangHoa(hangHoa);
                        break;

                    //Đặt hàng
                    case '3':
                        Console.Clear();
                        DatHang(ref hangHoa, ref donHang);
                        break;

                    //Quản lý
                    case '4':
                        Console.Clear();
                        QuanLy(ref hangHoa, ref donHang, ref admin);
                        break;
                    //Trường hợp nhập chức năng không hợp lệ
                    default:

                        Console.WriteLine($"Không có chức năng số {menuChinh}!!!");
                        break;
                }
                Console.Write("==>Nhấn (Y) để quay về menu chức năng, (Esc) để thoát chương trình: ");
                ki = Console.ReadKey();
                if (ki.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine();
                    Environment.Exit(0);
                }
                Console.ReadKey();
            } while (ki.KeyChar.ToString() == "y");
        }

        /// <summary>
        /// Menu chức năng chính
        /// </summary>
        static void MenuChinh()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t\t******************************");
            Console.WriteLine("\t\t\t*                            *");
            Console.WriteLine("\t\t\t*          CHỨC NĂNG         *");
            Console.WriteLine("\t\t\t*                            *");
            Console.WriteLine("\t\t\t******************************");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"\t\t\t1. Hiển thị thông tin hàng hóa");
            Console.WriteLine($"\t\t\t2. Tìm kiếm thông tin hàng hóa");
            Console.WriteLine($"\t\t\t3. Đặt hàng");
            Console.WriteLine($"\t\t\t4. Quản lý");
            Console.WriteLine();
        }
        
        /// <summary>
        /// Menu chức năng quản lý
        /// </summary>
        static void MenuQuanLy()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t\t--------------- QUẢN LÝ ---------------");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\t\t\t1. Xử lý đơn hàng");
            Console.WriteLine("\t\t\t2. Quản lý hàng hóa");
            Console.WriteLine("\t\t\t3. Xem danh sách đơn hàng");
            Console.WriteLine();
        }

        /// <summary>
        /// Menu chức năng quản lý hàng hóa
        /// </summary>
        static void MenuQuanLyHangHoa()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t\t******************************");
            Console.WriteLine("\t\t\t*                            *");
            Console.WriteLine("\t\t\t*      QUẢN LÝ HÀNG HÓA      *");
            Console.WriteLine("\t\t\t*                            *");
            Console.WriteLine("\t\t\t******************************");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();

            Console.WriteLine("\t\t\t1. Thêm hàng hóa");
            Console.WriteLine("\t\t\t2. Xóa hàng hóa theo mã");
            Console.WriteLine("\t\t\t3. Cập nhật số lượng hàng hóa");
            Console.WriteLine();
        }

        /// <summary>
        /// Hàm nhập hàng hóa từ file
        /// </summary>
        /// <param name="arrHangHoa">Danh sách hàng hóa</param>
        static void DocHangHoaTuFile(out LinkedList<HangHoa> arrHangHoa)
        {
            arrHangHoa = new LinkedList<HangHoa>();
            string[] hang = File.ReadAllLines("HangHoa.txt");
            for (int i = 0; i < hang.Length; i++)
            {
                string[] thuocTinh = hang[i].Split('#');
                string ma = thuocTinh[0];
                string ten = thuocTinh[1];
                string noiSX = thuocTinh[2];
                string mau = thuocTinh[3];
                int gia = int.Parse(thuocTinh[4]);
                DateTime ngayNhapKho = DateTime.ParseExact(thuocTinh[5], "dd/MM/yyyy", null);
                int soLuong = int.Parse(thuocTinh[6]);

                HangHoa newHH = new HangHoa(ma, ten, noiSX, mau, gia, ngayNhapKho, soLuong);
                Node<HangHoa> item = new Node<HangHoa>(newHH);
                arrHangHoa.AddLast(item);
            }
        }

        /// <summary>
        /// Hàm nhập đơn hàng từ file
        /// </summary>
        /// <param name="arrDonHang">Danh sách đơn hàng</param>
        static void DocDonHangTuFile(out LinkedList<DonHang> arrDonHang)
        {
            arrDonHang = new LinkedList<DonHang>();
            string[] don = File.ReadAllLines("DonHang.txt");
            for (int i = 0; i < don.Length; i++)
            {
                string[] thuocTinh = don[i].Split('#');
                string[] matHang = thuocTinh[0].Split('/');
                string[] ma = new string[matHang.Length];
                int[] soLuong = new int[matHang.Length];
                for (int j = 0; j < matHang.Length; j++)
                {
                    ma[j] = matHang[j].Substring(0, 4);
                    soLuong[j] = int.Parse(matHang[j].Substring(5));
                }
                int tongTien = int.Parse(thuocTinh[1]);
                string tenKH = thuocTinh[2];
                string diaChi = thuocTinh[3];
                string sdt = thuocTinh[4];
                DateTime ngayDatHang = DateTime.ParseExact(thuocTinh[5], "dd/MM/yyyy", null);
                DonHang newDH = new DonHang(ma, soLuong, tongTien, tenKH, diaChi, sdt, ngayDatHang);

                Node<DonHang> item = new Node<DonHang>(newDH);
                arrDonHang.AddLast(item);
            }
        }

        /// <summary>
        /// Hàm nhập tài khoản từ file
        /// </summary>
        /// <param name="arrTaiKhoan">Danh sách tài khoản</param>
        static void DocTaiKhoanTuFile(out LinkedList<Admin> arrTaiKhoan)
        {
            arrTaiKhoan = new LinkedList<Admin>();
            string[] tk = File.ReadAllLines("Admin.txt");
            for (int i = 0; i < tk.Length; i++)
            {
                string[] thuocTinh = tk[i].Split('#');
                string taiKhoan = thuocTinh[0];
                string matKhau = thuocTinh[1];
                Admin newTK = new Admin(taiKhoan, matKhau);

                Node<Admin> item = new Node<Admin>(newTK);
                arrTaiKhoan.AddLast(item);
            }
        }

        /// <summary>
        /// Hàm ghi hàng hóa vào file
        /// </summary>
        /// <param name="arrHangHoa">Danh sách hàng hóa</param>
        static void GhiHangHoaVaoFile(LinkedList<HangHoa> arrHangHoa)
        {
            Node<HangHoa> p = arrHangHoa.First;
            StreamWriter write = new StreamWriter("HangHoa.txt");
            using (write)
            {
                while (p != null)
                {
                    write.WriteLine($"{p.Data.MaHang}#{p.Data.TenHang}#{p.Data.NoiSX}#{p.Data.MauSac}#{p.Data.GiaBan}#{p.Data.NgayNhapKho.ToString("dd/MM/yyyy")}#{p.Data.SoLuong}");
                    p = p.Next;
                }
            }
        }

        /// <summary>
        /// Hàm ghi đơn hàng vào file
        /// </summary>
        /// <param name="arrDonHang">Danh sách đơn hàng</param>
        static void GhiDonHangVaoFile(LinkedList<DonHang> arrDonHang)
        {
            Node<DonHang> p = arrDonHang.First;
            StreamWriter write = new StreamWriter("DonHang.txt");
            using (write)
            {
                while (p != null)
                {
                    for (int i = 0; i < p.Data.MaHang.Length; i++)
                    {
                        if (i != p.Data.MaHang.Length - 1)
                        {
                            write.Write($"{p.Data.MaHang[i]}-{p.Data.SoLuong[i]}/");
                        }
                        else
                        {
                            write.Write($"{p.Data.MaHang[i]}-{p.Data.SoLuong[i]}");
                        }
                    }
                    write.WriteLine($"#{p.Data.TongTien}#{p.Data.TenKhachHang}#{p.Data.DiaChiKhachHang}#{p.Data.SoDienThoai}#{p.Data.NgayDatHang.ToString("dd/MM/yyyy")}");
                    p = p.Next;
                }
            }
        }

        /// <summary>
        /// Hàm thêm hàng hóa mới
        /// </summary>
        /// <param name="arrHangHoa">Danh sách hàng hóa</param>
        static void ThemHangHoa(ref LinkedList<HangHoa> arrHangHoa)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t\t******************************");
            Console.WriteLine("\t\t\t*                            *");
            Console.WriteLine("\t\t\t*        THÊM HÀNG HÓA       *");
            Console.WriteLine("\t\t\t*                            *");
            Console.WriteLine("\t\t\t******************************");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();

            string ma;
            string ten;
            string noiSX;
            string mau;
            int gia;
            DateTime ngayNhap = DateTime.Now;
            int soLuong;
            string chucNang;
            do
            {
                bool check = true;
                do
                {
                    Console.Write("Nhập mã hàng: ");
                    ma = Console.ReadLine();
                    if (ma.Length == 4)
                    {
                        check = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Mã hàng phải có 4 ký tự");
                        check = false;
                    }
                } while (check == false);

                Console.Write("Nhập tên hàng: ");
                ten = Console.ReadLine();
                Console.Write("Nhập nơi sản xuất: ");
                noiSX = Console.ReadLine();
                Console.Write("Nhập màu sắc: ");
                mau = Console.ReadLine();
                Console.Write("Nhập giá: ");
                gia = int.Parse(Console.ReadLine());
                Console.Write("Nhập số lượng: ");
                soLuong = int.Parse(Console.ReadLine());

                HangHoa newHH = new HangHoa(ma, ten, noiSX, mau, gia, ngayNhap, soLuong);
                Node<HangHoa> item = new Node<HangHoa>(newHH);
                arrHangHoa.AddLast(item);

                Console.Write("Tiếp tục nhập (Y): ");
                chucNang = Console.ReadLine();
            } while (chucNang.ToLower() == "y");
            GhiHangHoaVaoFile(arrHangHoa);

        }

        /// <summary>
        /// Hàm hiển thị thông tin hàng hóa
        /// </summary>
        /// <param name="arrHangHoa">Danh sách hàng hóa</param>
        static void HienThiThongTinHangHoa(LinkedList<HangHoa> arrHangHoa)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\t\t\t******************************");
            Console.WriteLine("\t\t\t*                            *");
            Console.WriteLine("\t\t\t*     THÔNG TIN HÀNG HÓA     *");
            Console.WriteLine("\t\t\t*                            *");
            Console.WriteLine("\t\t\t******************************");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            Console.WriteLine(arrHangHoa.ToString());
        }

        /// <summary>
        /// Hàm tìm kiếm hàng hóa theo tên hàng
        /// </summary>
        /// <param name="arrHangHoa">Danh sách hàng hóa</param>
        /// <param name="tenHang">Tên hàng</param>
        static void TimKiemThongTinHangHoa(LinkedList<HangHoa> arrHangHoa)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t\t***********************************");
            Console.WriteLine("\t\t\t*                                 *");
            Console.WriteLine("\t\t\t*   TÌM KIẾM THÔNG TIN HÀNG HÓA   *");
            Console.WriteLine("\t\t\t*                                 *");
            Console.WriteLine("\t\t\t***********************************");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();

            Console.Write("Nhập tên hàng cần tìm: ");
            string ten = Console.ReadLine();
            bool check = false;
            Node<HangHoa> p = arrHangHoa.First;
            while (p != null)
            {
                if (p.Data.TenHang == ten)
                {
                    Console.WriteLine(p.Data.ToString());
                    check = true;
                    break;
                }
                p = p.Next;
            }
            if (check == false)
            {
                Console.WriteLine("Hàng hóa cần tìm không có trong kho!!!");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Hàm đặt hàng
        /// </summary>
        /// <param name="arrHangHoa">Danh sách hàng hóa</param>
        /// <param name="arrDonHang">Danh sách đơn hàng</param>
        static void DatHang(ref LinkedList<HangHoa> arrHangHoa, ref LinkedList<DonHang> arrDonHang)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t\t******************************");
            Console.WriteLine("\t\t\t*                            *");
            Console.WriteLine("\t\t\t*           ĐẶT HÀNG         *");
            Console.WriteLine("\t\t\t*                            *");
            Console.WriteLine("\t\t\t******************************");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();

            //Mảng mã hàng hóa mà khách hàng muốn đặt           
            string[] arrMaHangHoa = new string[0];
            //Mảng số lượng tương ứng với từng mặt hàng mà khách hàng muốn đặt
            int[] arrSoLuong = new int[0];
            int tongTien = 0;
            bool checkMa;
            string chucNang;
            do
            {
                checkMa = false;
                Console.Write("Nhập mã hàng hóa: ");
                string ma = Console.ReadLine();
                Node<HangHoa> p = arrHangHoa.First;
                while (p != null)
                {
                    if (p.Data.MaHang == ma)
                    {
                        checkMa = true;
                        Console.Write("Nhập số lượng: ");
                        int sl = int.Parse(Console.ReadLine());
                        if (sl <= 0)
                        {
                            Console.WriteLine("Số lượng phải lớn hơn 0!");
                            break;
                        }
                        else if (p.Data.SoLuong >= sl)
                        {
                            Array.Resize(ref arrMaHangHoa, arrMaHangHoa.Length + 1);
                            arrMaHangHoa[arrMaHangHoa.Length - 1] = ma;
                            Array.Resize(ref arrSoLuong, arrSoLuong.Length + 1);
                            arrSoLuong[arrSoLuong.Length - 1] = sl;

                            tongTien += p.Data.GiaBan * sl;
                            Console.WriteLine($"==>Đặt thành công mặt hàng {ma}.");
                            break;
                        }
                    }
                    p = p.Next;
                }
                if (checkMa == false)
                {
                    Console.WriteLine($"Không tìm thấy mã hàng hóa {ma}!");
                }
                Console.Write("Bạn có muốn đặt thêm hàng hóa hay không? (Y) or (N): ");
                chucNang = Console.ReadLine();
            } while (chucNang.ToLower() == "y");

            if (arrMaHangHoa.Length >= 1)
            {
                Console.WriteLine("Xin mời bạn nhập thông tin cá nhân");
                Console.Write("Họ và tên: ");
                string ten = Console.ReadLine();
                Console.Write("Địa chỉ: ");
                string diaChi = Console.ReadLine();
                Console.Write("Số điện thoại: ");
                string sdt = Console.ReadLine();
                DateTime ngayDatHang = DateTime.Now;
                Console.WriteLine("==>Đặt hàng thành công! Đơn hàng của bạn đang được chờ để xử lý...");

                DonHang newDH = new DonHang(arrMaHangHoa, arrSoLuong, tongTien, ten, diaChi, sdt, ngayDatHang);
                Node<DonHang> item = new Node<DonHang>(newDH);
                arrDonHang.AddLast(item);
                GhiDonHangVaoFile(arrDonHang);
                Console.Write("Bạn có muốn xem thông tin đơn hàng của bạn hay không ? (Y) or (N): ");
                string xem = Console.ReadLine();
                if (xem.ToLower() == "y")
                {
                    XemThongTinDonHang(arrDonHang, newDH, arrMaHangHoa, arrSoLuong);
                }
            }
        }

        /// <summary>
        /// Hàm xem thông tin của 1 đơn hàng mà khách hàng vừa mới đặt
        /// </summary>
        /// <param name="newDH">Đơn hàng của khách hàng</param>
        /// <param name="arrMaHangHoa">Mảng mã hàng hóa của khách hàng</param>
        /// <param name="arrSoLuong">Mảng số lượng của khách hàng</param>
        static void XemThongTinDonHang(LinkedList<DonHang> arrDonHang, DonHang newDH, string[] arrMaHangHoa, int[] arrSoLuong)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("---THÔNG TIN ĐƠN HÀNG---");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(arrDonHang.Last.Data.ToString());
        }

        /// <summary>
        /// Hàm quản lý
        /// </summary>
        /// <param name="arrHangHoa">Danh sách hàng hóa</param>
        /// <param name="arrDonHang">Danh sách đơn hàng</param>
        /// <param name="arrTaiKhoan">Danh sách tài khoản</param>
        static void QuanLy(ref LinkedList<HangHoa> arrHangHoa, ref LinkedList<DonHang> arrDonHang, ref LinkedList<Admin> arrTaiKhoan)
        {
            bool check = false;
            int dem = 0;
            ConsoleKeyInfo ki;
            do
            {
                DangNhap(out string taiKhoan, out string matKhau);
                if (KiemTraDangNhap(taiKhoan, matKhau, arrTaiKhoan))
                {
                    check = true;
                    Console.WriteLine("Đăng nhập thành công");
                    do
                    {
                        MenuQuanLy();
                        Console.Write("Chọn chức năng: ");
                        char menuQuanLy = char.Parse(Console.ReadLine());
                        switch (menuQuanLy)
                        {
                            //Xử lý đơn hàng
                            case '1':
                                Console.Clear();
                                XuLyDonHang(ref arrDonHang, ref arrHangHoa);
                                break;

                            //Quản lý hàng hóa
                            case '2':
                                Console.Clear();
                                QuanLyHangHoa(ref arrHangHoa);
                                break;

                            case '3':
                                Console.Clear();
                                XemDonHang(arrDonHang);
                                break;
                            default:
                                break;
                        }
                        Console.Write("Nhập (Y) để quay về menu quản lý, (Esc) để kết thúc chương trình: ");
                        ki = Console.ReadKey();
                        if (ki.Key == ConsoleKey.Escape)
                        {
                            Console.WriteLine();
                            Environment.Exit(0);
                        }
                        Console.ReadKey();
                    } while (ki.KeyChar.ToString() == "y");
                }
                else
                {
                    dem++;
                    if (dem == 3)
                    {
                        Console.WriteLine("Bạn đã nhập sai 3 lần");
                        Console.ReadKey();
                        break;
                    }
                    Console.WriteLine("Sai tên đăng nhập hoặc mật khẩu");
                    Console.Write("==>Nhấn (Y) để nhập lại, (Esc) để thoát hệ thống: ");
                    ConsoleKeyInfo chucNang = Console.ReadKey();
                    if (chucNang.Key == ConsoleKey.Escape)
                    {
                        Console.WriteLine();
                        break;
                    }
                    Console.ReadKey();
                    check = false;
                }
            } while (check == false);
        }

        /// <summary>
        /// Hàm mã hóa mật khẩu
        /// </summary>
        /// <param name="matKhau">Mật khẩu</param>
        static void MaHoaMatKhau(out string matKhau)
        {
            matKhau = "";

            //Đọc từng nút nhập từ bàn phím
            ConsoleKeyInfo check = Console.ReadKey(true);
            do
            {
                //Nếu nút đó là backspace
                if (check.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    matKhau += check.KeyChar;
                }
                else
                {
                    Console.Write("\b \b");
                    matKhau = matKhau.Substring(0, matKhau.Length - 1);
                }
                check = Console.ReadKey(true);
            } while (check.Key != ConsoleKey.Enter);
        }

        /// <summary>
        /// Hàm kiểm tra đăng nhập
        /// </summary>
        /// <param name="taiKhoan">Tài khoản</param>
        /// <param name="matKhau">Mật khẩu</param>
        /// <param name="arrTaiKhoan">Danh sách tài khoản lưu trong file</param>
        /// <returns>
        ///     Return true: Nhập đúng tài khoản và mật khẩu
        ///     Return false: Nhập sai tài khoản hoặc mật khẩu
        /// </returns>
        static bool KiemTraDangNhap(string taiKhoan, string matKhau, LinkedList<Admin> arrTaiKhoan)
        {
            Node<Admin> p = arrTaiKhoan.First;
            while (p != null)
            {
                if (p.Data.TaiKhoan == taiKhoan)
                {
                    if (p.Data.MatKhau == matKhau)
                    {
                        return true;
                    }
                }
                p = p.Next;
            }
            return false;
        }

        /// <summary>
        /// Hàm đăng nhập
        /// </summary>
        /// <param name="taiKhoan">Tài khoản</param>
        /// <param name="matKhau">Mật khẩu</param>
        static void DangNhap(out string taiKhoan, out string matKhau)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\t\t\t*******************************");
            Console.WriteLine("\t\t\t*                             *");
            Console.WriteLine("\t\t\t*          ĐĂNG NHẬP          *");
            Console.WriteLine("\t\t\t*                             *");
            Console.WriteLine("\t\t\t*******************************");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();
            Console.Write("\t\t\tTài khoản: ");
            taiKhoan = Console.ReadLine();
            Console.Write("\t\t\tMật khẩu: ");
            MaHoaMatKhau(out matKhau);
            Console.WriteLine();
        }

        /// <summary>
        /// Hàm quản lý hàng hóa
        /// </summary>
        /// <param name="arrHangHoa">Danh sách hàng hóa</param>
        static void QuanLyHangHoa(ref LinkedList<HangHoa> arrHangHoa)
        {
            ConsoleKeyInfo ki;
            do
            {
                MenuQuanLyHangHoa();
                Console.Write("Chọn chức năng: ");
                char menuQuanLyHangHoa = char.Parse(Console.ReadLine());
                switch (menuQuanLyHangHoa)
                {
                    //Thêm hàng hóa mới
                    case '1':
                        Console.Clear();
                        ThemHangHoa(ref arrHangHoa);
                        break;

                    //Xóa hàng hóa theo mã hàng
                    case '2':
                        Console.Clear();
                        XoaHangHoa(ref arrHangHoa);
                        break;

                    //Cập nhật số lượng hàng hóa
                    case '3':
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("\t\t\t******************************");
                        Console.WriteLine("\t\t\t*                            *");
                        Console.WriteLine("\t\t\t*      CẬP NHẬT SỐ LƯỢNG     *");
                        Console.WriteLine("\t\t\t*                            *");
                        Console.WriteLine("\t\t\t******************************");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine();
                        Console.Write("Mã hàng cần cập nhật: ");
                        string ma = Console.ReadLine();
                        Console.Write("Nhập số lượng: ");
                        int soLuong = int.Parse(Console.ReadLine());
                        CapNhatSoLuong(ref arrHangHoa, ma, soLuong);
                        break;
                    default:
                        break;
                }
                Console.Write("Nhập (Y) để quay về menu quản lý hàng hóa, (Esc) để kết thúc chương trình: ");
                ki = Console.ReadKey();
                if (ki.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine();
                    Environment.Exit(0);
                }
                Console.ReadKey();
            } while (ki.KeyChar.ToString() == "y");
        }

        /// <summary>
        /// Hàm xử lý đơn hàng
        /// </summary>
        /// <param name="arrDonHang">Danh sách đơn hàng</param>
        static void XuLyDonHang(ref LinkedList<DonHang> arrDonHang, ref LinkedList<HangHoa> arrHangHoa)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t\t******************************");
            Console.WriteLine("\t\t\t*                            *");
            Console.WriteLine("\t\t\t*       XỬ LÝ ĐƠN HÀNG       *");
            Console.WriteLine("\t\t\t*                            *");
            Console.WriteLine("\t\t\t******************************");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();

            if (arrDonHang.First == null)
            {
                Console.WriteLine("Không có đơn hàng đang chờ xử lý");
                Console.WriteLine();
            }
            else
            {
                Node<DonHang> q = arrDonHang.First;

                for (int i = 0; i < q.Data.MaHang.Length; i++)
                {
                    Node<HangHoa> p = arrHangHoa.First;
                    while (p != null)
                    {
                        if (p.Data.MaHang == q.Data.MaHang[i])
                        {
                            p.Data.SoLuong -= q.Data.SoLuong[i];
                            CapNhatSoLuong(ref arrHangHoa, p.Data.MaHang, p.Data.SoLuong);
                            Console.WriteLine();
                            Console.WriteLine("Đơn hàng đã được xử lý.");
                            Console.WriteLine();
                        }
                        p = p.Next;
                    }
                }
                arrDonHang.RemoveFirst();
                GhiDonHangVaoFile(arrDonHang);
            }
        }

        /// <summary>
        /// Hàm xóa hàng hóa theo mã
        /// </summary>
        /// <param name="arrHangHoa">Danh sách hàng hóa</param>
        /// <param name="ma">Mã hàng hóa</param>
        static void XoaHangHoa(ref LinkedList<HangHoa> arrHangHoa)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t\t*********************************");
            Console.WriteLine("\t\t\t*                               *");
            Console.WriteLine("\t\t\t*      XÓA HÀNG HÓA THEO MÃ     *");
            Console.WriteLine("\t\t\t*                               *");
            Console.WriteLine("\t\t\t*********************************");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();

            Console.Write("Mã hàng cần xóa: ");
            string ma = Console.ReadLine();

            Node<HangHoa> p = arrHangHoa.First;
            while (p != null)
            {
                if (ma == p.Data.MaHang)
                {
                    arrHangHoa.Remove(p);
                    GhiHangHoaVaoFile(arrHangHoa);
                    return;
                }
                p = p.Next;
            }
            Console.WriteLine($"Mã hàng {ma} không có trong kho");
        }

        /// <summary>
        /// Hàm cập nhật số lượng
        /// </summary>
        /// <param name="arrHangHoa">Danh sách hàng hóa</param>
        static void CapNhatSoLuong(ref LinkedList<HangHoa> arrHangHoa, string ma, int soLuong)
        {
            Console.WriteLine();

            Node<HangHoa> p = arrHangHoa.First;
            while (p != null)
            {
                if (p.Data.MaHang == ma)
                {
                    p.Data.SoLuong = soLuong;
                    GhiHangHoaVaoFile(arrHangHoa);
                    Console.WriteLine($"Số lượng trong kho của hàng hóa mã {ma} đã được cập nhật");
                    break;
                }
                p = p.Next;
            }
            if (p == null)
            {
                Console.WriteLine("Hàng hóa không có trong kho");
            }
        }

        /// <summary>
        /// Hàm xem danh sách đơn hàng
        /// </summary>
        /// <param name="arrDonHang">Danh sách đơn hàng</param>
        static void XemDonHang(LinkedList<DonHang> arrDonHang)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t\t***********************************");
            Console.WriteLine("\t\t\t*                                 *");
            Console.WriteLine("\t\t\t*      XEM DANH SÁCH ĐƠN HÀNG     *");
            Console.WriteLine("\t\t\t*                                 *");
            Console.WriteLine("\t\t\t***********************************");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();

            if (arrDonHang.First == null)
            {
                Console.WriteLine("Không có đơn hàng nào !!!");
            }
            else
            {
                Console.WriteLine(arrDonHang.ToString());
            }
        }
    }
}