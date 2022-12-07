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
            string[] fejl�cek = new string[]
            {
                "K�rd�s", "1. v�lasz", "2. v�lasz", "3. v�lasz", "Helyes v�lasz", "k�p"
            };

            for (int i = 0; i < fejl�cek.Length; i++)
            {
                xlSheet.Cells[i+1, 1] = fejl�cek[i];
            }

            Models.HajosContext context = new();
            var mindenK�rd�s = context.Questions.ToList();

            object[,] adatT�mb = new object[mindenK�rd�s.Count(), fejl�cek.Count()];

            for (int i = 0; i < mindenK�rd�s.Count(); i++)
            {
                adatT�mb[i, 0] = mindenK�rd�s[i].Question1;
                adatT�mb[i, 1] = mindenK�rd�s[i].Answer1;
                adatT�mb[i, 2] = mindenK�rd�s[i].Answer2;
                adatT�mb[i, 3] = mindenK�rd�s[i].Answer3;
                adatT�mb[i, 4] = mindenK�rd�s[i].CorrectAnswer;
                adatT�mb[i, 5] = mindenK�rd�s[i].Image;
            }

            int sorokSz�ma = adatT�mb.GetLength(0);
            int oszlopokSz�ma = adatT�mb.GetLength(1);

            Excel.Range adatRange = xlSheet.get_Range("A2", Type.Missing).get_Resize(sorokSz�ma, oszlopokSz�ma);

            adatRange.Value2 = adatT�mb;
            adatRange.Columns.AutoFit();

            Excel.Range fejl�cRange = xlSheet.get_Range("A1", Type.Missing).get_Resize(1, 6);

            fejl�cRange.Font.Bold = true;
            fejl�cRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            fejl�cRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            fejl�cRange.EntireColumn.AutoFit();
            fejl�cRange.RowHeight = 40;
            fejl�cRange.Interior.Color = Color.Fuchsia;
            fejl�cRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            adatRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            Excel.Range m�sodikOszlopRange = xlSheet.get_Range("A2", Type.Missing).get_Resize(sorokSz�ma, 1);
            m�sodikOszlopRange.Font.Bold = true;
            m�sodikOszlopRange.Interior.Color = Color.LightYellow;

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

            string xyz = GetCell(2, oszlopokSz�ma);

            Excel.Range uts�OszlopRange = xlSheet.get_Range(xyz, Type.Missing).get_Resize(sorokSz�ma,1);
            uts�OszlopRange.Interior.Color = Color.DarkGreen;

            string xyz2 = GetCell(2, oszlopokSz�ma-1);

            Excel.Range uts�EOszlopRange = xlSheet.get_Range(xyz2, Type.Missing).get_Resize(sorokSz�ma, 1);
            uts�EOszlopRange.NumberFormat = "0.00";
        }
    }
}