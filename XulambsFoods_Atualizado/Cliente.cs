using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_Atualizado
{
    class Cliente
    {
        private string _nome;
        private string _cpf;
        private Queue<Pedido> _todosPedidos;


        public Cliente(string nome, string cpf)
        {
            this._nome = nome;
            this._cpf = cpf;
            _todosPedidos = new Queue<Pedido>();
        }

        public string GetNome()
        {
            return _nome;
        }

        public string GetCPF()
        {
            return _cpf;
        }

        
    }
}
