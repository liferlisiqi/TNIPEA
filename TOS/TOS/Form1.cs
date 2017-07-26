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
        ArrayList allSolutions = new ArrayList();
        public myForm()
        {
            InitializeComponent();
        }

        private void readDataBTN_Click(object sender, EventArgs e)
        {
            DateTime beginTime = System.DateTime.Now;
            readSubData("[5-15-1]", 0, 5000000);
            //readSubData("[5-16-1]", 5000000, 10000000);
            //readSubData(10000000, 15000000);
            DateTime endTime = System.DateTime.Now;
            readDataBox.Text = (endTime - beginTime).TotalSeconds.ToString();
            //NPOIHelper.outputExcel(allSolution, "D:/源码/多目标精确算法/GAP benchmark 1/5-16-1.xls");
            MessageBox.Show("ok");
        }

        //epslon约束法
        private void epslon_Click(object sender, EventArgs e)
        {
            ArrayList restSolutions = allSolutions;
            ArrayList ParetoSet = new ArrayList();
            DateTime beginTime = System.DateTime.Now;
            Solution Pareto = Find.min3Pareto(restSolutions);
            ParetoSet.Add(Pareto);
            while (true)
            {
                Pareto = Find.min3Pareto(restSolutions, ParetoSet);
                if (Find.min3Pareto(restSolutions, ParetoSet).ob3 == 1000)
                    break;
                ParetoSet.Add(Pareto);
            }
            DateTime endTime = System.DateTime.Now;
            epslonBox.Text = (endTime - beginTime).TotalSeconds.ToString();
            Console.WriteLine("epslon： " + ParetoSet.Count);
        }

        //每步剪切
        private void EC_Click(object sender, EventArgs e)
        {
            ArrayList restSolution = allSolutions;
            ArrayList paretos = new ArrayList();
            Solution pareto = new Solution();
            DateTime beginTime = System.DateTime.Now;
            while (restSolution.Count != 0)
            {
                pareto = Find.min3Pareto(restSolution);
                restSolution = Find.ndSolutions(restSolution, pareto);
                paretos.Add(pareto);
            }
            DateTime endTime = System.DateTime.Now;
            ECBox.Text = (endTime - beginTime).TotalSeconds.ToString();
            Console.WriteLine("epslonCUT： " + paretos.Count);
            //NPOIHelper.outputExcel(paretos, "D:/源码/多目标精确算法/多目标benchmark/GAP/3-8-11p.xls");
        }

        //极点Pareto剪切，epslon约束法
        private void PC_Click(object sender, EventArgs e)
        {
            ArrayList restSolutions = allSolutions;
            ArrayList ParetoSet = new ArrayList();
            Solution Pareto = null;
            DateTime beginTime = System.DateTime.Now;

            Solution min3Pareto = Find.min3Pareto(restSolutions);
            restSolutions = Find.ndSolutions(restSolutions, min3Pareto);
            ParetoSet.Add(min3Pareto);
            Solution min1Pareto = Find.min1Pareto(restSolutions);
            restSolutions = Find.ndSolutions(restSolutions, min1Pareto);
            Solution min2Pareto = Find.min2Pareto(restSolutions);
            restSolutions = Find.ndSolutions(restSolutions, min2Pareto);
            while (true)
            {
                Pareto = Find.min3Pareto(restSolutions, ParetoSet);
                if (Find.min3Pareto(restSolutions, ParetoSet).ob3 == 1000)
                    break;
                ParetoSet.Add(Pareto);
            }
            ParetoSet.Add(min1Pareto);
            ParetoSet.Add(min2Pareto);

            DateTime endTime = System.DateTime.Now;
            PCepslonBox.Text = (endTime - beginTime).TotalSeconds.ToString();
            Console.WriteLine("PCepslon： " + ParetoSet.Count);
        }

        //极点Pareto剪切，每步剪切
        private void PEC_Click(object sender, EventArgs e)
        {
            ArrayList restSolutions = allSolutions;
            ArrayList ParetoSet = new ArrayList();
            Solution Pareto = null;
            DateTime beginTime = System.DateTime.Now;

            Solution min3Pareto = Find.min3Pareto(restSolutions);
            restSolutions = Find.ndSolutions(restSolutions, min3Pareto);
            ParetoSet.Add(min3Pareto);
            Solution min1Pareto = Find.min1Pareto(restSolutions);
            restSolutions = Find.ndSolutions(restSolutions, min1Pareto);
            ParetoSet.Add(min1Pareto);
            Solution min2Pareto = Find.min2Pareto(restSolutions);
            restSolutions = Find.ndSolutions(restSolutions, min2Pareto);
            ParetoSet.Add(min2Pareto);

            while (restSolutions.Count != 0)
            {
                Pareto = Find.min3Pareto(restSolutions);
                restSolutions = Find.ndSolutions(restSolutions, Pareto);
                ParetoSet.Add(Pareto);
            }

            DateTime endTime = System.DateTime.Now;
            PCECBox.Text = (endTime - beginTime).TotalSeconds.ToString();
            Console.WriteLine("PCEC: " + ParetoSet.Count);
        }

        //理想Pareto剪切，每步剪切
        private void IEC_Click(object sender, EventArgs e)
        {
            ArrayList restSolutions = allSolutions;
            ArrayList ParetoSet = new ArrayList();
            Solution Pareto = null;
            DateTime beginTime = System.DateTime.Now;

            Solution idealPareto = Find.idealPareto(restSolutions);
            restSolutions = Find.ndSolutions(restSolutions, idealPareto);
            ParetoSet.Add(idealPareto);

            while (restSolutions.Count != 0)
            {
                Pareto = Find.min3Pareto(restSolutions);
                restSolutions = Find.ndSolutions(restSolutions, Pareto);
                ParetoSet.Add(Pareto);
            }
            DateTime endTime = System.DateTime.Now;
            ICECBox.Text = (endTime - beginTime).TotalSeconds.ToString();
            Console.WriteLine("ICEC: " + ParetoSet.Count);
        }

        //极点Pareto剪切，理想Pareto剪切，每步CUT
        private void PIEC_Click(object sender, EventArgs e)
        {
            ArrayList restSolutions = allSolutions;
            ArrayList ParetoSet = new ArrayList();
            Solution Pareto = null;
            DateTime beginTime = System.DateTime.Now;

            Solution min3Pareto = Find.min3Pareto(restSolutions);
            restSolutions = Find.ndSolutions(restSolutions, min3Pareto);
            ParetoSet.Add(min3Pareto);
            Solution min1Pareto = Find.min1Pareto(restSolutions);
            restSolutions = Find.ndSolutions(restSolutions, min1Pareto);
            ParetoSet.Add(min1Pareto);
            Solution min2Pareto = Find.min2Pareto(restSolutions);
            restSolutions = Find.ndSolutions(restSolutions, min2Pareto);
            ParetoSet.Add(min2Pareto);

            Solution idealPoint = new Solution(min1Pareto.ob1, min2Pareto.ob2, min3Pareto.ob3);
            Solution idealPareto = Find.idealPareto(restSolutions, idealPoint);
            restSolutions = Find.ndSolutions(restSolutions, idealPareto);
            ParetoSet.Add(idealPareto);

            while (restSolutions.Count != 0)
            {
                Pareto = Find.min3Pareto(restSolutions);
                restSolutions = Find.ndSolutions(restSolutions, Pareto);
                ParetoSet.Add(Pareto);
            }
            DateTime endTime = System.DateTime.Now;
            PIECBox.Text = (endTime - beginTime).TotalSeconds.ToString();
            Console.WriteLine("PIEC: " + ParetoSet.Count);
        }

        private void readSubData(String tablename, int lo, int hi)
        {
            SqlConnection conn = new SqlConnection("Data Source=USER-20160720BD;"
                + "Initial Catalog=MNGAPbenchmark;Integrated Security=True");
            DataTable dt = new DataTable();
            string sql = "select totaltime,totalcost,timeCV,costCV from " + tablename + " where ID > " + lo + " and ID <= " + hi;
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
                   Convert.ToDouble(dt.Rows[i][2].ToString()),);
                allSolutions.Add(solution);
            }
        }
    }
}
