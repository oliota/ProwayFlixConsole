using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Business
{
    public static class Utils
    {
        public static void Pausar(string mensagem)
        {
            Console.WriteLine(mensagem);
            Console.WriteLine("Digite ENTER para continuar");
            Console.ReadLine();
        }

        public static void Pausar()
        {
            Console.WriteLine("Digite ENTER para continuar");
            Console.ReadLine();
        }

        public static bool Perguntar(string pergunta)
        {
            Console.WriteLine($"{pergunta} (S/N)");
            var resposta= Console.ReadLine().ToLower();
            return resposta.Equals("s");
        }
    }
}
