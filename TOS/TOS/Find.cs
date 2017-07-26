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
        public static Solution min3Pareto(ArrayList solutions)
        {
            Solution pareto = new Solution(400, 200, 100);
            foreach (Solution i in solutions)
                pareto = i.z3 < pareto.z3 ? i : pareto;
            return pareto;
        }

        //用于epslon
        public static Solution min3Pareto(ArrayList solutions, ArrayList paretos)
        {
            Solution pareto = new Solution(400, 200, 100);
            foreach (Solution i in solutions)
            {
                bool flag = true;
                foreach (Solution j in paretos)
                {
                    if (!(i.ob1 < j.ob1 || i.ob2 < j.ob2))
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
        public static Solution min1Pareto(ArrayList solutions)
        {
            Solution pareto = new Solution(400, 200, 100);
            foreach (Solution i in solutions)
                pareto = i.z1 < pareto.z1 ? i : pareto;
            return pareto;
        }

        //min ob2 + a * (ob1 + ob3)
        public static Solution min2Pareto(ArrayList solutions)
        {
            Solution pareto = new Solution(400, 200, 100);
            foreach (Solution i in solutions)
                pareto = i.z2 < pareto.z2 ? i : pareto;
            return pareto;
        }

        //找理想点
        public static Solution idealPoint(ArrayList solutions)
        {
            Solution idealPoint = new Solution(400, 200, 100);
            foreach (Solution i in solutions)
            {
                if (idealPoint.ob1 > i.ob1) idealPoint.ob1 = i.ob1;
                if (idealPoint.ob2 > i.ob2) idealPoint.ob2 = i.ob2;
                if (idealPoint.ob3 > i.ob3) idealPoint.ob3 = i.ob3;
            }
            return idealPoint;
        }

        //找理想点Pareto
        public static Solution idealPareto(ArrayList solutions, Solution ideal)
        {
            Solution idealPareto = new Solution();
            double nearestD = double.MaxValue;
            foreach (Solution i in solutions)
            {
                double distance = Math.Pow(ideal.ob1 - i.ob1, 2)
                    + Math.Pow(ideal.ob2 - i.ob2, 2)
                    + Math.Pow(ideal.ob3 - i.ob3, 2);
                if (distance < nearestD)
                {
                    idealPareto = i;
                    nearestD = distance;
                }
            }
            return idealPareto;
        }

        //寻找不被给定Pareto支配的解
        public static ArrayList ndSolutions(ArrayList solutions, Solution pareto)
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
