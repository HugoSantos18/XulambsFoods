using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_Atualizado
{
    /// <summary>
    /// Classe Pizza para a Xulambs Pizza. Uma pizza tem um preço base e pode ter até 8 ingredientes adicionais. Cada ingrediente tem custo fixo.
    /// A pizza deve emitir uma nota de compra com os seus detalhes.
    /// </summary>
    public class Pizza : Comida
    {
        private const int MaxIngredientes = 8;
        private const string Descricao = "Pizza";
        private const double PrecoBase = 29d;
        private const double ValorAdicional = 5d;
        private const int AdicionaisParaDesconto = 5;
        private const double PctDesconto = 0.5;
        private double desconto;

        /// <summary>
        /// Inicializador privado da pizza: valida a quantidade de adicionais. Em caso de não validação, a pizza será criada sem adicionais.
        /// </summary>
        /// <param name="quantosAdicionais">Quantos adicionais para iniciar a pizza. Em caso de não validação, a pizza será criada sem adicionais.</param>
        private void init(int quantosAdicionais)
        {
            desconto = 0;
            if (PodeAdicionar(quantosAdicionais))
                _quantidadeIngredientes = quantosAdicionais;
        }

        /// <summary>
        /// Construtor padrão.Cria uma pizza sem adicionais.
        /// </summary>
        public Pizza():base(Descricao, MaxIngredientes, PrecoBase, ValorAdicional)
        {
            init(0);
        }

        /// <summary>
        /// Cria uma pizza com a quantidade de adicionais pré-definida.Em caso de valor inválido, a pizza será criada sem adicionais.
        /// </summary>
        /// <param name="quantosAdicionais">Quantidade de adicionais (entre 0 e 8, limites inclusivos)</param>
        public Pizza(int quantosAdicionais) : base(Descricao,MaxIngredientes,PrecoBase,ValorAdicional)
        {
            init(quantosAdicionais);
        }

        /// <summary>
        /// Calcula o valor dos adicionais para o preço final da pizza. Atualmente o valor dos adicionais é a multiplicação da quantidade de adicionais por seu valor unitário
        /// </summary>
        /// <returns>Double com o valor a ser cobrado pelos adicionais.</returns>
        protected override double ValorAdicionais()
        {
            if (_quantidadeIngredientes > AdicionaisParaDesconto)
            {
                return (_quantidadeIngredientes * ValorAdicional) - DescontoAdicional();
            }
            return _quantidadeIngredientes * ValorAdicional;
        }

        /// <summary>
        /// Retorna o valor final da pizza, incluindo seus adicionais.
        /// </summary>
        /// <returns>Double com o valor final da pizza.</returns>
        public override double ValorFinal()
        {
            return PrecoBase + ValorAdicionais();
        }

        /// <summary>
        /// Método para calcular o valor do desconto caso tenha mais de 5 ingredientes adicionais.
        /// </summary>
        /// <returns>Valor total do desconto a ser dado pela compra dos ingredientes adicionais a partir do 6° ingrediente.</returns>
        private double DescontoAdicional()
        {
            int ingredientesAdicionaisComDesconto = _quantidadeIngredientes - AdicionaisParaDesconto;

            if (ingredientesAdicionaisComDesconto >= 0)
            {
                return desconto = (ingredientesAdicionaisComDesconto * ValorAdicional) * PctDesconto;
            }
            return desconto;
        }

        /// <summary>
        /// Nota simplificada de compra: descrição da pizza, dos ingredientes e do preço.
        /// </summary>
        /// <returns>String no formato "<DESCRICAO> <PRECO> com <QUANTIDADE> ingredientes <PRECO></PRECO>, no valor total de <VALOR>"</returns>
        public override string NotaDeCompra()
        {
            return $"{Descricao} ({PrecoBase:C2}) com {_quantidadeIngredientes} ingredientes ({ValorAdicionais():C2}), no valor de {ValorFinal():C2}\nDesconto de {DescontoAdicional():C2}";
        }

    }
}
