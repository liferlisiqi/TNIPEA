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
namespace TNIPEA
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
            allSolutions.Clear();
            string M = M_ComBox.Text;
            string N = N_ComBox.Text;
            string ins = ins_ComBox.Text;
            DateTime beginTime = System.DateTime.Now;
            readSubData("[" + M + "-" + N + "-" + ins + "]", 0, 10000000);
            //readSubData("[5-18-5]", 5000000, 10000000);
            //readSubData(10000000, 15000000);
            DateTime endTime = System.DateTime.Now;
            Console.WriteLine("time: " + (endTime - beginTime).TotalSeconds);
            this.N_Text.Text = allSolutions.Count + "";
            //NPOIHelper.outputExcel(allSolution, "D:/源码/多目标精确算法/GAP benchmark 1/5-16-1.xls");
        }

        //epslon约束
        private void E_Click(object sender, EventArgs e)
        {
            ArrayList restSolutions = allSolutions;
            ArrayList ParetoSet = new ArrayList();
            DateTime beginTime = System.DateTime.Now;
            Solution Pareto = Find.min3Pareto(restSolutions);
            ParetoSet.Add(Pareto);
            while (true)
            {
                Pareto = Find.min3Pareto(restSolutions, ParetoSet);
                if (Pareto.ob3 == 1000)
                    break;
                ParetoSet.Add(Pareto);
            }
            DateTime endTime = System.DateTime.Now;
            this.E_P_Text.Text = ParetoSet.Count + "";
            this.E_T_Text.Text = (endTime - beginTime).TotalSeconds + "";
        }

        //epslon约束法，储存支配记录
        private void E2_Click(object sender, EventArgs e)
        {
            ArrayList restSolutions = allSolutions;
            ArrayList ParetoSet = new ArrayList();
            DateTime beginTime = System.DateTime.Now;
            Solution Pareto = Find.min3Pareto(restSolutions);
            ParetoSet.Add(Pareto);
            while (true)
            {
                Pareto = Find.min3Pareto2(restSolutions, Pareto);
                if (Pareto.ob3 == 1000)
                    break;
                ParetoSet.Add(Pareto);
            }
            DateTime endTime = System.DateTime.Now;
            this.E2_P_Text.Text = ParetoSet.Count + "";
            this.E2_T_Text.Text = (endTime - beginTime).TotalSeconds + "";
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
            this.PC_LS_Text.Text = restSolutions.Count + "";

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
            this.PC_P_Text.Text = ParetoSet.Count + "";
            this.PC_T_Text.Text = (endTime - beginTime).TotalSeconds + "";
        }

        private void IC_Click(object sender, EventArgs e)
        {
            ArrayList restSolutions = allSolutions;
            ArrayList ParetoSet = new ArrayList();
            DateTime beginTime = System.DateTime.Now;

            Solution idealPareto = Find.idealPareto(restSolutions);
            restSolutions = Find.ndSolutions(restSolutions, idealPareto);         
            this.IC_LS_Text.Text = restSolutions.Count + "";

            Solution Pareto = Find.min3Pareto(restSolutions);
            ParetoSet.Add(Pareto);

            while (true)
            {
                Pareto = Find.min3Pareto(restSolutions, ParetoSet);
                if (Find.min3Pareto(restSolutions, ParetoSet).ob3 == 1000)
                    break;
                ParetoSet.Add(Pareto);
            }
            ParetoSet.Add(idealPareto);

            DateTime endTime = System.DateTime.Now;
            this.IC_P_Text.Text = ParetoSet.Count + "";
            this.IC_T_Text.Text = (endTime - beginTime).TotalSeconds + "";
        }

        private void PIC_Click(object sender, EventArgs e)
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
           
            Solution idealPareto = Find.idealPareto(restSolutions);
            restSolutions = Find.ndSolutions(restSolutions, idealPareto);            

            this.PIC_LS_Text.Text = restSolutions.Count + "";

            while (true)
            {
                Pareto = Find.min3Pareto(restSolutions, ParetoSet);
                if (Find.min3Pareto(restSolutions, ParetoSet).ob3 == 1000)
                    break;
                ParetoSet.Add(Pareto);
            }

            ParetoSet.Add(min1Pareto);
            ParetoSet.Add(min2Pareto);
            ParetoSet.Add(idealPareto);

            DateTime endTime = System.DateTime.Now;
            this.PIC_P_Text.Text = ParetoSet.Count + "";
            this.PIC_T_Text.Text = (endTime - beginTime).TotalSeconds + "";
        }

        //每步剪切
        private void EC_Click(object sender, EventArgs e)
        {
            ArrayList restSolution = allSolutions;
            ArrayList ParetoSet = new ArrayList();
            Solution Pareto = new Solution();
            DateTime beginTime = System.DateTime.Now;
            while (restSolution.Count != 0)
            {
                Pareto = Find.min3Pareto(restSolution);
                restSolution = Find.ndSolutions(restSolution, Pareto);
                ParetoSet.Add(Pareto);
            }
            DateTime endTime = System.DateTime.Now;
            this.EC_P_Text.Text = ParetoSet.Count + "";
            this.EC_T_Text.Text = (endTime - beginTime).TotalSeconds + "";
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
            this.PEC_LS_Text.Text = restSolutions.Count + "";

            while (restSolutions.Count != 0)
            {
                Pareto = Find.min3Pareto(restSolutions);
                restSolutions = Find.ndSolutions(restSolutions, Pareto);
                ParetoSet.Add(Pareto);
            }

            DateTime endTime = System.DateTime.Now;
            this.PEC_P_Text.Text = ParetoSet.Count + "";
            this.PEC_T_Text.Text = (endTime - beginTime).TotalSeconds + "";
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
            this.IEC_LS_Text.Text = restSolutions.Count + "";

            while (restSolutions.Count != 0)
            {
                Pareto = Find.min3Pareto(restSolutions);
                restSolutions = Find.ndSolutions(restSolutions, Pareto);
                ParetoSet.Add(Pareto);
            }
            DateTime endTime = System.DateTime.Now;
            this.IEC_P_Text.Text = ParetoSet.Count + "";
            this.IEC_T_Text.Text = (endTime - beginTime).TotalSeconds + "";
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
            this.PIEC_LS_Text.Text = restSolutions.Count + "";

            while (restSolutions.Count != 0)
            {
                Pareto = Find.min3Pareto(restSolutions);
                restSolutions = Find.ndSolutions(restSolutions, Pareto);
                ParetoSet.Add(Pareto);
            }
            DateTime endTime = System.DateTime.Now;
            this.PIEC_P_Text.Text = ParetoSet.Count + "";
            this.PIEC_T_Text.Text = (endTime - beginTime).TotalSeconds + "";
        }

        private void readSubData(String tablename, int lo, int hi)
        {
            SqlConnection conn = new SqlConnection("Data Source=USER-20160720BD;Initial Catalog=MNGAPbenchmark;Integrated Security=True");
            DataTable dt = new DataTable();
            string sql = "select totaltime,totalcost,timeCV from " + tablename + " where ID > " + lo + " and ID <= " + hi;
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
                   Math.Floor(Convert.ToDouble(dt.Rows[i][0].ToString()) * 10000) / 10000,
                   Math.Floor(Convert.ToDouble(dt.Rows[i][1].ToString()) * 10000) / 10000,
                   Math.Floor(Convert.ToDouble(dt.Rows[i][2].ToString()) * 10000) / 10000);
                allSolutions.Add(solution);
            }
        }    
    }
}
