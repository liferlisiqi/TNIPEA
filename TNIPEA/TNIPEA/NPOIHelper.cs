using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

namespace TNIPEA
{
    class NPOIHelper
    {
        public static void outputExcel(ArrayList solutions, string filename)
        {
            HSSFWorkbook WorkBook = new HSSFWorkbook();
            ISheet Sheet = WorkBook.CreateSheet();

            for (int i = 0; i < solutions.Count; i++)
            {
                Solution Solution = (Solution)solutions[i];
                IRow Row = Sheet.CreateRow(i);
                Row.CreateCell(0).SetCellValue(1 - Solution.ob1);
                Row.CreateCell(1).SetCellValue(1 - Solution.ob2);
                Row.CreateCell(2).SetCellValue(1 - Solution.ob3);
            }

            using (FileStream File = new FileStream(@filename, FileMode.Create))
            {
                WorkBook.Write(File);
                File.Close();
            }
        }
    }
}
