namespace Polinomc_.Clases
{
    internal class Polinom
    {
        public List<Termen> Termeni;
        public Polinom(string StrPolinom)
        {
            StrPolinom = StrPolinom.Replace(" ", "");
            List<char> Signs = new List<char>();
            if (StrPolinom[0] != '-')
                Signs.Add('+');

            for(int i = 0;i <= StrPolinom.Length-1;i++)
                if (StrPolinom[i] == '-' || StrPolinom[i] == '+')
                    Signs.Add(StrPolinom[i]);

            string[] terms = StrPolinom.Split('+', '-');
            Termeni = new List<Termen>();
            for(int i = 0; i < terms.Length; i++)
            {
                var aux = terms[i].Replace("^", "");
                string[] variables;
                if (aux.Contains("x"))
                {
                    variables = aux.Split("x");
                    if (variables[0] == "")
                    {
                        variables[0] = "1";
                    }
                    if (variables[1] == "")
                        variables[1] = "1";
                }
                else
                {
                    variables = new string[] { aux, "" };
                }
                Termen t = new(TryParse(variables[0]), TryParse(variables[1]));
                if (Signs[i] == '-')
                    t.Const = -t.Const;
                Termeni.Add(t);
            }

        }

        public Polinom Divide(Polinom divisor)
        {
            List<Termen> qTerms = new List<Termen>();
            List<Termen> remainderTerms = new List<Termen>(Termeni);

            while (remainderTerms.Count > 0 && remainderTerms[0].Power >= divisor.Termeni[0].Power)
            {
                Termen leadingTerm = new Termen(remainderTerms[0].Const / divisor.Termeni[0].Const,
                                                remainderTerms[0].Power - divisor.Termeni[0].Power);

                qTerms.Add(leadingTerm);

                List<Termen> temp = new List<Termen>();
                foreach (var term in divisor.Termeni)
                {
                    Termen tempTerm = new Termen(term.Const * leadingTerm.Const, term.Power + leadingTerm.Power);
                    temp.Add(tempTerm);
                }

                remainderTerms = SubtractPolynomials(remainderTerms, temp);
            }

            return new Polinom(string.Join("", qTerms));
        }

        private List<Termen> SubtractPolynomials(List<Termen> minuend, List<Termen> subtrahend)
        {
            List<Termen> result = new List<Termen>(minuend);

            foreach (var term in subtrahend)
            {
                int index = result.FindIndex(t => t.Power == term.Power);
                if (index != -1)
                {
                    result[index].Const -= term.Const;
                    if (result[index].Const == 0)
                    {
                        result.RemoveAt(index);
                    }
                }
                else
                {
                    result.Add(new Termen(-term.Const, term.Power));
                }
            }

            result.Sort((x, y) => y.Power.CompareTo(x.Power)); 
            return result;
        }

        private int TryParse(string str)
        {
            try
            {
                return int.Parse(str);
            }
            catch { return 0; }
        }

        public override string ToString()
        {
            string output = "";
            foreach (var term in Termeni) { output += term; }
            return output;
        }

    }
}
