using Polinomc_.Clases;

namespace Polinomc_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var str = Console.ReadLine();
            Polinom poli = new(str);
            var str2 = Console.ReadLine();
            Polinom polinom = new(str2);
            Console.WriteLine(poli.Divide(polinom));
        }
    }
}