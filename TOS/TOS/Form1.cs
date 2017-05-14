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
    public partial class Form1 : Form
    {
        ArrayList allSolution = new ArrayList();
        public Form1()
        {
            InitializeComponent();
        }

        private void readDataBTN_Click(object sender, EventArgs e)
        {
            DateTime beginTime = System.DateTime.Now;
            SqlConnection conn = new SqlConnection("Data Source=USER-20160720BD;" +
                "Initial Catalog=MO-benchmark-AP;Integrated Security=True");
            //SqlConnection conn = new SqlConnection("Data Source=USER-20160720BD;" +
            //    "Initial Catalog=MO-benchmark-AP;Integrated Security=True");
            DataTable dt = new DataTable();
            string sql = "select sum,makespan,cv from [3-8-10]";
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
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

            DateTime endTime = System.DateTime.Now;
            readDataBox.Text = (endTime - beginTime).TotalSeconds.ToString();
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
            epslonBox.Text = (endTime - beginTime).TotalSeconds.ToString();
            Console.WriteLine("epslon： " + paretos.Count);
        }

        //先用极点CUT，再用epslon约束法
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
            PCepslonBox.Text = (endTime - beginTime).TotalSeconds.ToString();
            Console.WriteLine("PCepslon： " + paretos.Count);
        }

        //每步都CUT的epslon约束法
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
            epslonCUTBox.Text = (endTime - beginTime).TotalSeconds.ToString();
            Console.WriteLine("epslonCUT： " + paretos.Count);
            //NPOIHelper.outputExcel(paretos, "D:/源码/多目标精确算法/多目标benchmark/GAP/3-8-11p.xls");
        }

        //先用极点CUT，再每步都CUT的epslon约束法
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
            PCECBox.Text = (endTime - beginTime).TotalSeconds.ToString();
            Console.WriteLine("PCECBTN: " + paretos.Count);
        }

    }
}
