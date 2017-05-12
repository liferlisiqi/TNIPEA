using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace TOS
{
    class Select
    {
        public static ArrayList nondominates(ArrayList solutions, Solution pareto)
        {
            ArrayList ndSolutions = new ArrayList();
            foreach (Solution i in solutions)
            {
                if (!pareto.dominate(i) && !pareto.equal(i))
                    ndSolutions.Add(i);
            }
            return ndSolutions;
        }
    }
}
