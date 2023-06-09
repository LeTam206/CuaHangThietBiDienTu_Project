/* Admin.cs
 * Name: Luong Tien Thinh
 * Date: 21/07/2022
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CTDL
{
    internal class Admin
    {
        //Fields
        private string taiKhoan;
        private string matKhau;

        //Properties
        public string TaiKhoan { get => taiKhoan; }
        public string MatKhau { get => matKhau; set => matKhau = value; }

        //Constructors
        public Admin(string taiKhoan, string matKhau)
        {
            this.taiKhoan = taiKhoan;
            this.matKhau = matKhau;
        }

        //Methods
    }
}
