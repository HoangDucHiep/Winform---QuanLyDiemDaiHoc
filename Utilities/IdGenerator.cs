using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDiemDaiHoc.Utilities
{
    public class IdGenerator
    {

        private static QLDDataContext db = new QLDDataContext();
        private static Random random = new Random();

        public static string GetGvID()
        {
            string newId;
            do
            {
                newId = random.Next(10000, 99999).ToString();
            } while (db.GiangViens.Any(gv => gv.MaGiangVien == newId));

            return newId;
        }

        public static string GetSvID()
        {
            string newId;
            do
            {
                newId = random.Next(100000000, 999999999).ToString();
            }while (db.SinhViens.Any(sv => sv.MaSinhVien == newId));

            return newId;
        }
    }
}
