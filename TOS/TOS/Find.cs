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
        public static Solution z3Min(ArrayList solutions)
        {
            Solution pareto = new Solution(100, 100, 1);
            foreach (Solution i in solutions)
                pareto = i.z3 < pareto.z3 ? i : pareto;
            return pareto;
        }
    }
}
