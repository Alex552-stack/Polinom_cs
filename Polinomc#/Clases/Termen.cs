using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polinomc_.Clases
{
    internal class Termen
    {
        public int Power;
        public int Const;
        public Termen(int conts, int power)
        {
            Power = power;
            Const = conts;
        }
        public override string ToString()
        {
            if (Const >= 0) return "+" + Const.ToString() + "x^" + Power.ToString();
            return Const.ToString() + "x^" + Power.ToString();
        }
    }
}
