using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace TNIPEA
{
    class Solution
    {
        public double ob1;
        public double ob2;
        public double ob3;
        public double z1;
        public double z2;
        public double z3;
        public double sum;
        public bool isDominate = false;

        public Solution() { }

        public Solution(double ob1, double ob2, double ob3)
        {
            this.ob1 = ob1;
            this.ob2 = ob2;
            this.ob3 = ob3;
            this.z1 = ob1 + 0.000000001 * (ob2 + ob3);
            this.z2 = ob2 + 0.000000001 * (ob1 + ob3);
            this.z3 = ob3 + 0.000000001 * (ob1 + ob2);
            this.sum = ob1 + ob2 + ob3;
        }

        //判断this是否支配solution
        public bool dominate(Solution solution)
        {
            return (this.ob1 <= solution.ob1 && this.ob2 <= solution.ob2 && this.ob3 <= solution.ob3 && this.sum < solution.sum);
        }

        //判断两个解是否重合
        public bool equal(Solution solution)
        {
            return (this.ob1 == solution.ob1 && this.ob2 == solution.ob2 && this.ob3 == solution.ob3);
        }
    }
}
