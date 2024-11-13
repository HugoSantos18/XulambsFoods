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

        public int RegistrarPedido(Pedido novo)
        {
            _todosPedidos.Enqueue(novo);

            return _todosPedidos.Count();
        }

        public string RelatorioPedidos()
        {
            foreach (Pedido p in _todosPedidos)
            {
                return p.Relatorio();
            }

            return null;
        }

        public double TotalGasto()
        {
            double gasto = 0;
            foreach (Pedido p in _todosPedidos)
            {
                gasto += p.PrecoAPagar();
            }
            return gasto;
        }
    
    }
}
