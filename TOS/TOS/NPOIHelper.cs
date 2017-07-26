using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

namespace TOS
{
    class NPOIHelper
    {
        public static void outputExcel(ArrayList solutions, string filename)
        {
            HSSFWorkbook WorkBook = new HSSFWorkbook();
            ISheet Sheet = WorkBook.CreateSheet();

            for (int i = 262139; i < solutions.Count; i++)
            {
                Solution Solution = (Solution)solutions[i];
                IRow Row = Sheet.CreateRow(i - 262139);
                Row.CreateCell(0).SetCellValue(Solution.ob1);
                Row.CreateCell(1).SetCellValue(Solution.ob2);
                Row.CreateCell(2).SetCellValue(Solution.ob3);
            }

            using (FileStream File = new FileStream(@filename, FileMode.Create))
            {
                WorkBook.Write(File);
                File.Close();
            }
        }
    }
}
