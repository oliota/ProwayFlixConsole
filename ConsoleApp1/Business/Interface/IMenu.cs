using ConsoleApp1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Business
{
    interface IMenu 
    {
        void ExibirMenu();
        object Converter(string opcao);
        void ExibirOpcoes();
        void ExecutarEscolha(object opcao);
    }
}
