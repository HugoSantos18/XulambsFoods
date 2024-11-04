using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_Atualizado
{
    public class PedidoXulambs
    {

        #region static/const
        /// <summary>
        /// Para gerar o id incremental automático
        /// </summary>
        private static int _ultimoPedido;
        /// <summary>
        /// Para criar um vetor de comidas de tamanho grande
        /// </summary>
        private const int Maxcomidas = 100;
        private const int MaxEntrega = 6;
        private const double TaxaServico = 0.1;
        private readonly double[] TaxasEntrega = { 0, 5, 8 };
        private readonly double[] DistanciasEntrega = { 4, 8, Double.MaxValue };
        #endregion

        #region atributos
        private int _idPedido;
        private DateTime _data;
        private List <Comida>  _comidas;
        private int _quantcomidas;
        private double _distanciaEntrega;
        private bool _paraEntrega;
        private bool _aberto;
        private IPedido categoria;

        #endregion

        #region construtores
        static PedidoXulambs()
        {
            _ultimoPedido = 0;
        }

        /// <summary>
        /// Cria um pedido com a data de hoje. Identificador é gerado automaticamente a partir do último identificador armazenado.
        /// </summary>
        public PedidoXulambs(bool paraEntrega, double distancia)
        {
            _quantcomidas = 0;
            _aberto = true;
            _comidas = new List<Comida>();
            _data = DateTime.Now;
            _idPedido = ++_ultimoPedido;
            _paraEntrega = paraEntrega;
            _distanciaEntrega = distancia;
        }
        #endregion

        #region métodos de negócio
        /// <summary>
        /// Verifica se uma pizza pode ser adicionada ao pedido, ou seja, se o pedido está aberto e há espaço na memória.
        /// </summary>
        /// <returns>TRUE se puder adicionar, FALSE caso contrário</returns>
        private bool PodeAdicionar()
        {
            bool resposta = _aberto;
            if (_paraEntrega)
                resposta &= (_quantcomidas < MaxEntrega);
            return resposta;
        }

        private double ValorTaxa()
        {
            double taxa = ValorItens() * TaxaServico;
            if (_paraEntrega)
            {
                for (int i = DistanciasEntrega.Length - 1; i >= 0; i--)
                {
                    if (_distanciaEntrega <= DistanciasEntrega[i])
                        taxa = TaxasEntrega[i];
                }
            }
            return taxa;
        }

        private double ValorItens()
        {
            double preco = 0d;
            for (int i = 0; i < _quantcomidas; i++)
            {
                preco += _comidas[i].ValorFinal();
            }
            return preco;
        }

        /// <summary>
        /// Adiciona uma pizza ao pedido, se for possível. Caso não seja, a operação é ignorada.Retorna a quantidade de comidas do pedido após a execução. 
        /// </summary>
        /// <param name="pizza">Pizza a ser adicionada</param>
        /// <returns>A quantidade de comidas do pedido após a execução.</returns>
        public int Adicionar(Comida comida)
        {
            if (PodeAdicionar())
            {
                _comidas[_quantcomidas] = comida;
                _quantcomidas++;
            }
            return _quantcomidas;
        }

        /// <summary>
        ///  Fecha um pedido, desde que ele contenha pelo menos 1 pizza. Caso esteja vazio, a operação é ignorada.
        /// </summary>
        public void FecharPedido()
        {
            if (_quantcomidas > 0)
                _aberto = false;
        }

        /// <summary>
        /// Calcula o preço a ser pago pelo pedido (no momento, a soma dos preços de todas as comidas contidas no pedido)
        /// </summary>
        /// <returns>Double com o valor a ser pago pelo pedido(> 0)</returns>
        public double PrecoAPagar()
        {
            return ValorItens() + ValorTaxa();
        }

        /// <summary>
        /// Cria um relatório para o pedido, contendo seu número, sua data(DD/MM/AAAA), detalhamento de cada pizza e o preço final a ser pago.
        /// </summary>
        /// <returns>String com os detalhes especificados: 
	    ///<br/><pre>
	    ///PEDIDO - NÚMERO - DD/MM/AAAA
	    ///01 - DESCRICAO DA PIZZA
	    ///02 - DESCRICAO DA PIZZA
	    ///03 - DESCRICAO DA PIZZA
        ///
	    ///TOTAL A PAGAR: R$ VALOR
	    ///</pre></returns>
        public string Relatorio()
        {
            StringBuilder relat = new StringBuilder($"XULAMBS PIZZA - Pedido {categoria.ToString()} ");
            relat.Append($"{_idPedido:D2} - {_data.ToShortDateString()}");
            relat.AppendLine("=============================");

            for (int i = 0; i < _quantcomidas; i++)
            {
                relat.AppendLine($"{(i + 1):D2} - {_comidas[i].NotaDeCompra()}");
            }
            relat.AppendLine($"VALOR TAXA: {categoria.Relatorio()}");
            relat.AppendLine($"\nTOTAL A PAGAR: {PrecoAPagar():C2}");
            relat.AppendLine("=============================");
            return relat.ToString();
        }
        #endregion
    }
}
