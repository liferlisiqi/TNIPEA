using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;
namespace TOS
{
    public partial class myForm : Form
    {
        ArrayList allSolution = new ArrayList();
        public myForm()
        {
            InitializeComponent();
        }

        private void readDataBTN_Click(object sender, EventArgs e)
        {
            DateTime beginTime = System.DateTime.Now;
            
            readSubData(0, 5000000);
            readSubData(5000000, 10000000);
            DateTime endTime = System.DateTime.Now;
            readDataBox.Text = (endTime - beginTime).TotalMilliseconds.ToString();
            //NPOIHelper.outputExcel(allSolution, "D:/源码/多目标精确算法/多目标benchmark/GAP/3-8-11.xlsx");
            MessageBox.Show("ok");
        }

        //epslon约束法
        private void epslonBTN_Click(object sender, EventArgs e)
        {
            ArrayList restSolution = allSolution;
            ArrayList paretos = new ArrayList();
            DateTime beginTime = System.DateTime.Now;
            Solution pareto = Find.z3Min(restSolution);
            paretos.Add(pareto);
            while (true)
            {
                pareto = Find.z3Min(restSolution, paretos);
                if (Find.z3Min(restSolution, paretos).ob3 == 2)
                    break;
                paretos.Add(pareto);
            }
            DateTime endTime = System.DateTime.Now;
            epslonBox.Text = (endTime - beginTime).TotalMilliseconds.ToString();
            Console.WriteLine("epslon： " + paretos.Count);
        }

        //极点Pareto剪切，epslon约束法
        private void poleEpslonBTN_Click(object sender, EventArgs e)
        {
            ArrayList restSolution = allSolution;
            ArrayList paretos = new ArrayList();
            Solution pareto = new Solution();
            DateTime beginTime = System.DateTime.Now;

            pareto = Find.z3Min(restSolution);
            restSolution = TOS.Select.nondominates(restSolution, pareto);
            paretos.Add(pareto);
            Solution pareto1 = Find.z1Min(restSolution);
            restSolution = TOS.Select.nondominates(restSolution, pareto1);
            Solution pareto2 = Find.z2Min(restSolution);
            restSolution = TOS.Select.nondominates(restSolution, pareto2);
            while (true)
            {
                pareto = Find.z3Min(restSolution, paretos);
                if (Find.z3Min(restSolution, paretos).ob3 == 2)
                    break;
                paretos.Add(pareto);
            }
            paretos.Add(pareto1);
            paretos.Add(pareto2);

            DateTime endTime = System.DateTime.Now;
            PCepslonBox.Text = (endTime - beginTime).TotalMilliseconds.ToString();
            Console.WriteLine("PCepslon： " + paretos.Count);
        }

        //每步剪切
        private void epslonCUTBTN_Click(object sender, EventArgs e)
        {
            ArrayList restSolution = allSolution;
            ArrayList paretos = new ArrayList();
            Solution pareto = new Solution();
            DateTime beginTime = System.DateTime.Now;
            while (restSolution.Count != 0)
            {
                pareto = Find.z3Min(restSolution);
                restSolution = TOS.Select.nondominates(restSolution, pareto);
                paretos.Add(pareto);
            }
            DateTime endTime = System.DateTime.Now;
            epslonCUTBox.Text = (endTime - beginTime).TotalMilliseconds.ToString();
            Console.WriteLine("epslonCUT： " + paretos.Count);
            //NPOIHelper.outputExcel(paretos, "D:/源码/多目标精确算法/多目标benchmark/GAP/3-8-11p.xls");
        }

        //极点Pareto剪切，每步剪切
        private void PCECBTN_Click(object sender, EventArgs e)
        {
            ArrayList restSolution = allSolution;
            ArrayList paretos = new ArrayList();
            Solution pareto = new Solution();
            DateTime beginTime = System.DateTime.Now;

            pareto = Find.z3Min(restSolution);
            restSolution = TOS.Select.nondominates(restSolution, pareto);
            paretos.Add(pareto);

            pareto = Find.z1Min(restSolution);
            restSolution = TOS.Select.nondominates(restSolution, pareto);
            paretos.Add(pareto);

            pareto = Find.z2Min(restSolution);
            restSolution = TOS.Select.nondominates(restSolution, pareto);
            paretos.Add(pareto);

            while (restSolution.Count != 0)
            {
                pareto = Find.z3Min(restSolution);
                restSolution = TOS.Select.nondominates(restSolution, pareto);
                paretos.Add(pareto);
            }
            DateTime endTime = System.DateTime.Now;
            PCECBox.Text = (endTime - beginTime).TotalMilliseconds.ToString();
            Console.WriteLine("PCEC: " + paretos.Count);
        }

        //理想Pareto剪切，每步剪切
        private void ICECBTN_Click(object sender, EventArgs e)
        {
            ArrayList restSolution = allSolution;
            ArrayList paretos = new ArrayList();
            Solution pareto = new Solution();
            DateTime beginTime = System.DateTime.Now;

            Solution ideal = Find.ideal(restSolution);
            pareto = Find.nearest(restSolution, ideal);
            restSolution = TOS.Select.nondominates(restSolution, pareto);
            paretos.Add(pareto);

            while (restSolution.Count != 0)
            {
                pareto = Find.z3Min(restSolution);
                restSolution = TOS.Select.nondominates(restSolution, pareto);
                paretos.Add(pareto);
            }
            DateTime endTime = System.DateTime.Now;
            ICECBox.Text = (endTime - beginTime).TotalMilliseconds.ToString();
            Console.WriteLine("ICEC: " + paretos.Count);
        }

        //极点Pareto剪切，理想Pareto剪切，每步CUT
        private void PIECBTN_Click(object sender, EventArgs e)
        {
            ArrayList restSolution = allSolution;
            ArrayList paretos = new ArrayList();
            Solution pareto = new Solution();
            DateTime beginTime = System.DateTime.Now;

            pareto = Find.z3Min(restSolution);
            restSolution = TOS.Select.nondominates(restSolution, pareto);
            paretos.Add(pareto);

            pareto = Find.z1Min(restSolution);
            restSolution = TOS.Select.nondominates(restSolution, pareto);
            paretos.Add(pareto);

            pareto = Find.z2Min(restSolution);
            restSolution = TOS.Select.nondominates(restSolution, pareto);
            paretos.Add(pareto);

            Solution ideal = Find.ideal(restSolution);
            pareto = Find.nearest(restSolution, ideal);
            restSolution = TOS.Select.nondominates(restSolution, pareto);
            paretos.Add(pareto);

            while (restSolution.Count != 0)
            {
                pareto = Find.z3Min(restSolution);
                restSolution = TOS.Select.nondominates(restSolution, pareto);
                paretos.Add(pareto);
            }
            DateTime endTime = System.DateTime.Now;
            PIECBox.Text = (endTime - beginTime).TotalMilliseconds.ToString();
            Console.WriteLine("PIEC: " + paretos.Count);
        }

        private void readSubData(int lo, int hi)
        {
            SqlConnection conn = new SqlConnection("Data Source=USER-20160720BD;"
                + "Initial Catalog=MNGAPbenchmark;Integrated Security=True");
            DataTable dt = new DataTable();
            string sql = "select makespan,totaltime,CV from [5-17-1] where ID > " + lo + " and ID <= " + hi;
            try
            {
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter(new SqlCommand(sql, conn));
                sda.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Solution solution = new Solution(
                   Convert.ToDouble(dt.Rows[i][0].ToString()),
                   Convert.ToDouble(dt.Rows[i][1].ToString()),
                   Convert.ToDouble(dt.Rows[i][2].ToString()));
                allSolution.Add(solution);
            }
        }
    }
}
