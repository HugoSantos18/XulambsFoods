using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_Atualizado
{
    internal class PedidoLocal : IPedido
    {
        /// <summary>
        /// Para criar um vetor de pizzas de tamanho grande
        /// </summary>
        private const int MaxPizzas = 100;
        private const double TaxaServico = 0.1;
        private List<Comida> _comidas;
        public PedidoLocal(List<Comida> comidas)
        {
            _comidas = comidas;
        }

        public bool PodeAdicionar()
        {
            return true;
        }

        public double ValorTaxa()
        {
            return ValorItens() * TaxaServico;
        }

        private double ValorItens()
        {
            double preco = 0d;
            for (int i = 0; i < _comidas.Count; i++)
            {
                preco += _comidas[i].ValorFinal();
            }
            return preco;
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
            StringBuilder relat = new StringBuilder("XULAMBS PIZZA - Pedido Local");
            relat.AppendLine("=============================");

            relat.AppendLine($"VALOR ITENS: {ValorItens():C2}");
            relat.AppendLine($"\nTAXA SERVIÇO : {ValorTaxa():C2}");
            relat.AppendLine("=============================");
            return relat.ToString();
        }
    }
}
