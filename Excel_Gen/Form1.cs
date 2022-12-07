using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace Excel_Gen
{

    public partial class Form1 : Form
    {
        Excel.Application xlApp;
        Excel.Workbook xlWB;
        Excel.Worksheet xlSheet;

        public Form1()
        {
            InitializeComponent();
            CreateExcel();
        }

        public void CreateExcel()
        {
            try
            {
                xlApp = new Excel.Application();
                xlWB = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlWB.ActiveSheet;

                CreateTable();

                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }

        public void CreateTable()
        {
            string[] fejlécek = new string[]
            {
                "Kérdés", "1. válasz", "2. válasz", "3. válasz", "Helyes válasz", "kép"
            };

            for (int i = 0; i < fejlécek.Length; i++)
            {
                xlSheet.Cells[i+1, 1] = fejlécek[i];
            }

            Models.HajosContext context = new();
            var mindenKérdés = context.Questions.ToList();

            object[,] adatTömb = new object[mindenKérdés.Count(), fejlécek.Count()];

            for (int i = 0; i < mindenKérdés.Count(); i++)
            {
                adatTömb[i, 0] = mindenKérdés[i].Question1;
                adatTömb[i, 1] = mindenKérdés[i].Answer1;
                adatTömb[i, 2] = mindenKérdés[i].Answer2;
                adatTömb[i, 3] = mindenKérdés[i].Answer3;
                adatTömb[i, 4] = mindenKérdés[i].CorrectAnswer;
                adatTömb[i, 5] = mindenKérdés[i].Image;
            }

            int sorokSzáma = adatTömb.GetLength(0);
            int oszlopokSzáma = adatTömb.GetLength(1);

            Excel.Range adatRange = xlSheet.get_Range("A2", Type.Missing).get_Resize(sorokSzáma, oszlopokSzáma);

            adatRange.Value2 = adatTömb;
            adatRange.Columns.AutoFit();

            Excel.Range fejlécRange = xlSheet.get_Range("A1", Type.Missing).get_Resize(1, 6);

            fejlécRange.Font.Bold = true;
            fejlécRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            fejlécRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            fejlécRange.EntireColumn.AutoFit();
            fejlécRange.RowHeight = 40;
            fejlécRange.Interior.Color = Color.Fuchsia;
            fejlécRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            adatRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            Excel.Range másodikOszlopRange = xlSheet.get_Range("A2", Type.Missing).get_Resize(sorokSzáma, 1);
            másodikOszlopRange.Font.Bold = true;
            másodikOszlopRange.Interior.Color = Color.LightYellow;

            string GetCell(int x, int y)
            {
                string ExcelCoordinate = "";
                int dividend = y;
                int modulo;

                while (dividend > 0)
                {
                    modulo = (dividend - 1) % 26;
                    ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                    dividend = (int)((dividend - modulo) / 26);
                }
                ExcelCoordinate += x.ToString();

                return ExcelCoordinate;
            }

            string xyz = GetCell(2, oszlopokSzáma);

            Excel.Range utsóOszlopRange = xlSheet.get_Range(xyz, Type.Missing).get_Resize(sorokSzáma,1);
            utsóOszlopRange.Interior.Color = Color.DarkGreen;

            string xyz2 = GetCell(2, oszlopokSzáma-1);

            Excel.Range utsóEOszlopRange = xlSheet.get_Range(xyz2, Type.Missing).get_Resize(sorokSzáma, 1);
            utsóEOszlopRange.NumberFormat = "0.00";
        }
    }
}