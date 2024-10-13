using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_Atualizado
{
    public abstract class Pedido
    {

        #region static/const
        /// <summary>
        /// Para gerar o id incremental automático
        /// </summary>
        private static int _ultimoPedido;
        #endregion

        #region atributos
        protected int _maxComidas;
        protected int _idPedido;
        protected DateTime _data;
        protected Comida [] _comidas;
        protected int _quantComidas;
        protected bool _aberto;
        #endregion

        #region construtores
        static Pedido()
        {
            _ultimoPedido = 0;
        }

        /// <summary>
        /// Cria um pedido com a data de hoje. Identificador é gerado automaticamente a partir do último identificador armazenado.
        /// </summary>
        protected Pedido(int maxComidas)
        {
            if (maxComidas < 1) maxComidas = 1;
            _maxComidas = maxComidas;
            _quantComidas = 0;
            _aberto = true;
            _comidas = new Comida[maxComidas];
            _data = DateTime.Now;
            _idPedido = ++_ultimoPedido;
        }
        #endregion

        #region métodos de negócio

        protected abstract bool PodeAdicionar();
        protected abstract double ValorTaxa();
        public abstract string Relatorio();

        protected double ValorItens()
        {
            double preco = 0d;
            for (int i = 0; i < _quantComidas; i++)
            {
                preco += _comidas[i].ValorFinal();
            }
            return preco;
        }
        /// <summary>
        /// Adiciona uma comida ao pedido, se for possível. Caso não seja, a operação é ignorada.Retorna a quantidade de comidas do pedido após a execução. 
        /// </summary>
        /// <param name="comida">Pizza a ser adicionada</param>
        /// <returns>A quantidade de comida do pedido após a execução.</returns>
        public int Adicionar(Comida comida)
        {
            if (PodeAdicionar())
            {
                _comidas[_quantComidas] = comida;
                _quantComidas++;
            }
            return _quantComidas;
        }

        /// <summary>
        ///  Fecha um pedido, desde que ele contenha pelo menos 1 pizza. Caso esteja vazio, a operação é ignorada.
        /// </summary>
        public void FecharPedido()
        {
            if (_quantComidas > 0)
                _aberto = false;
        }

        /// <summary>
        /// Calcula o preço a ser pago pelo pedido (no momento, a soma dos preços de todas as pizzas contidas no pedido)
        /// </summary>
        /// <returns>Double com o valor a ser pago pelo pedido(> 0)</returns>
        public double PrecoAPagar()
        {
            return ValorItens() + ValorTaxa();
        }
        #endregion
    }
}
