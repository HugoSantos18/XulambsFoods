using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_Atualizado
{
    public class Pedido : IEquatable<int>, IComparable
    {

        #region static/const
        /// <summary>
        /// Para gerar o id incremental automático
        /// </summary>
        private static int _ultimoPedido;
        #endregion

        #region atributos
        protected int _idPedido;
        protected DateTime _data;
        private List<Comida> _comidas;
        protected int _quantComidas;
        protected bool _aberto;
        private const double TaxaServico = 0.1;
        private IPedido categoria;
        #endregion

        #region construtores
        static Pedido()
        {
            _ultimoPedido = 0;
        }

        /// <summary>
        /// Cria um pedido com a data de hoje. Identificador é gerado automaticamente a partir do último identificador armazenado.
        /// </summary>
        public Pedido(double distancia)
        {
            _quantComidas = 0;
            _aberto = true;
            _comidas = new List<Comida>();
            _data = DateTime.Now;
            _idPedido = ++_ultimoPedido;
        }
        #endregion

        #region métodos de negócio

        /// <summary>
        /// Método para calcular o valor de cada item individualmente.
        /// </summary>
        /// <returns>Valor total dos itens adicionados (double)</returns>
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
        /// Método para calcular o valor da taxa de serviço do pedido
        /// </summary>
        /// <returns>Valor total da taxa de serviço a ser somada no pedido. (double)</returns>
        public double ValorTaxa()
        {
            return categoria.ValorTaxa();
        }

        public bool PodeAdicionar()
        {
            return categoria.PodeAdicionar();
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


        public string Relatorio()
        {
            StringBuilder relat = new StringBuilder($"XULAMBS PIZZA - Pedido {categoria.ToString()} ");
            relat.Append($"{_idPedido:D2} - {_data.ToShortDateString()}");
            relat.AppendLine("=============================");

            for (int i = 0; i < _comidas.Count; i++)
            {
                relat.AppendLine($"{(i + 1):D2} - {_comidas[i].NotaDeCompra()}");
            }
            relat.AppendLine($"VALOR TAXA: {categoria.Relatorio()}");
            relat.AppendLine($"\nTOTAL A PAGAR: {PrecoAPagar():C2}");
            relat.AppendLine("=============================");
            return relat.ToString();
        }
    }
}
