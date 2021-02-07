using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Business.Seguranca
{
    class Teste: IMenu
    {
        public int Numero { get; set; }

        public object Converter(string opcao)
        {
            throw new NotImplementedException();
        }

        public void ExecutarEscolha(object opcao)
        {
            throw new NotImplementedException();
        }

        public void ExibirMenu()
        {
            throw new NotImplementedException();
        }

        public void ExibirOpcoes()
        {
            throw new NotImplementedException();
        }
    }
}
