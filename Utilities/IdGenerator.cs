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

    }
}
