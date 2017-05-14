using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace TOS
{
    class Find
    {
        //min ob3 + a * (ob2 + ob1)
        public static Solution z3Min(ArrayList solutions)
        {
            Solution pareto = new Solution(200, 21, 2);
            foreach (Solution i in solutions)
                pareto = i.z3 < pareto.z3 ? i : pareto;
            return pareto;
        }

        //用于epslon
        public static Solution z3Min(ArrayList solutions, ArrayList paretos)
        {
            Solution pareto = new Solution(200, 21, 2);
            foreach (Solution i in solutions)
            {
                bool flag = true;
                foreach (Solution j in paretos)
                {
                    if (!(i.ob1 < j.ob1 || i.ob2 < j.ob2))
                    //if (j.dominate(i)||j.equal(i))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                    pareto = i.z3 < pareto.z3 ? i : pareto;
            }
            return pareto;
        }

        //min ob1 + a * (ob2 + ob3)
        public static Solution z1Min(ArrayList solutions)
        {
            Solution pareto = new Solution(200, 21, 2);
            foreach (Solution i in solutions)
                pareto = i.z1 < pareto.z1 ? i : pareto;
            return pareto;
        }

        //min ob2 + a * (ob1 + ob3)
        public static Solution z2Min(ArrayList solutions)
        {
            Solution pareto = new Solution(200, 21, 2);
            foreach (Solution i in solutions)
                pareto = i.z2 < pareto.z2 ? i : pareto;
            return pareto;
        }
    }
}
