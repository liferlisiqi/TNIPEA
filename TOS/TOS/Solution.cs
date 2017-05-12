using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOS
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

        public Solution() { }

        public Solution(double ob1, double ob2, double ob3)
        {
            this.ob1 = ob1;
            this.ob2 = ob2;
            this.ob3 = ob3;
            this.z1 = ob1 + 0.0000001 * (ob2 + ob3);
            this.z2 = ob2 + 0.0000001 * (ob1 + ob3);
            this.z3 = ob3 + 0.0000001 * (ob1 + ob2);
            this.sum = ob1 + ob2 + ob3;
        }

        //判断this是否支配solution，三个目标都>=，且其中至少一个是紧的
        public bool dominate(Solution solution)
        {
            if (this.ob1 <= solution.ob1 && this.ob2 <= solution.ob2 && this.ob3 <= solution.ob3 && this.sum < solution.sum)
                return true;
            else
                return false;
        }

        public bool equal(Solution solution)
        {
            if (this.ob1 == solution.ob1 && this.ob2 == solution.ob2 && this.ob3 == solution.ob3)
                return true;
            else
                return false;
        }
    }
}
