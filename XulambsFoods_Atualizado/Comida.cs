using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_Atualizado
{
    public abstract class Comida
    {
        private int _maxIngredientes;
        private string _descricao;
        private double _precoBase;
        private double _valorAdicional;
        protected int _quantidadeIngredientes;

        protected Comida(string _descricao, int _maxIngredientes, double _precoBase, double _valorAdicional)
        {
            this._descricao = _descricao;
            this._maxIngredientes = _maxIngredientes;
            this._precoBase = _precoBase;
            this._valorAdicional = _valorAdicional;
        }

        protected  bool PodeAdicionar(int quantos)
        {
           return quantos > 0 && quantos + _quantidadeIngredientes <= _maxIngredientes;
        }

        protected virtual double ValorAdicionais()
        {
            return _quantidadeIngredientes * _valorAdicional;
        }

        /// <summary>
        /// Método para adicionar os ingredientes adicionais ao sanduiche.
        /// </summary>
        /// <param name="quantos"></param>
        /// <returns>Total de ingredientes do sanduiche</returns>
        public int AdicionarIngredientes(int quantos)
        {
            if (PodeAdicionar(quantos))
            {
                _quantidadeIngredientes += quantos;
            }
            return _quantidadeIngredientes;
        }

        /// <summary>
        /// Nota simplificada de compra: descrição da pizza, dos ingredientes e do preço.
        /// </summary>
        /// <returns>String no formato "<DESCRICAO> <PRECO> com <QUANTIDADE> ingredientes <PRECO></PRECO>, no valor total de <VALOR>"</returns>
        public virtual string NotaDeCompra()
        {
            return $"{_descricao} ({_precoBase:C2}) com {_quantidadeIngredientes} ingredientes ({ValorAdicionais():C2}), no valor de {ValorFinal():C2}";
        }

        public abstract double ValorFinal();
    }
}
