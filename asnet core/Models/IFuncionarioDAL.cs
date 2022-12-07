using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asnet_core.Models
{
    public  interface IFuncionarioDAL
    {
        IEnumerable<Funcionario> GetAllFuncionarios();
        void AddFuncionario(Funcionario funcionario);
        void UpdateFuncionario(Funcionario funcionario);
        Funcionario GetFuncionario(int? id);
        void DeleteFuncionario(int? id);
    }
}
