namespace QuanLyDiemDaiHoc.Utilities
{
    public class GPAConverter
    {
        public static string ConvertToGPA(double score)
        {
            string diemHeChu = "";
            if (score >= 9.5)
            {
                diemHeChu = "A+";
            }
            else if (score >= 8.5)
            {
                diemHeChu = "A";
            }
            else if (score >= 8)
            {
                diemHeChu = "B+";
            }
            else if (score >= 7)
            {
                diemHeChu = "B";
            }
            else if (score >= 6)
            {
                diemHeChu = "C+";
            }
            else if (score >= 5.5)
            {
                diemHeChu = "C";
            }
            else if (score >= 4.5)
            {
                diemHeChu = "D+";
            }
            else if (score >= 4)
            {
                diemHeChu = "D";
            }
            else if (score >= 2)
            {
                diemHeChu = "F+";
            }
            else
            {
                diemHeChu = "F";
            }

            return diemHeChu;
        }
    }
}