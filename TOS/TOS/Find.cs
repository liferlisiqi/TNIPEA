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
            Solution pareto = new Solution(100, 100, 1);
            foreach (Solution i in solutions)
                pareto = i.z3 < pareto.z3 ? i : pareto;
            return pareto;
        }

        //min ob1 + a * (ob2 + ob3)
        public static Solution z1Min(ArrayList solutions)
        {
            Solution pareto = new Solution(100, 100, 1);
            foreach (Solution i in solutions)
                pareto = i.z1 < pareto.z1 ? i : pareto;
            return pareto;
        }

        //min ob2 + a * (ob1 + ob3)
        public static Solution z2Min(ArrayList solutions)
        {
            Solution pareto = new Solution(100, 100, 1);
            foreach (Solution i in solutions)
                pareto = i.z2 < pareto.z2 ? i : pareto;
            return pareto;
        }
    }
}
