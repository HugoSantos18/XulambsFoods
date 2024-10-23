using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_Atualizado
{
    public class Sanduiche : Comida
    {
        private const string Descricao = "Sanduíche";
        private const int MaxIngredientes = 5;
        private const double PrecoBase = 15;
        private const double ValorAdicional = 3;
        private const double ValorCombo = 5;
        private bool _temFritas;

        #region Métodos construtores e inicializadores
        /// <summary>
        /// Método para inicializar os atributos para ser chamado no construtor da classe.
        /// </summary>
        /// <param name="quantidadeAdicionais"></param>
        /// <param name="_temFritas"></param>
        public void init(int quantosAdicionais)
        {
            this._temFritas = false;
            if (PodeAdicionar(quantosAdicionais))
                _quantidadeIngredientes = quantosAdicionais;
        }
        public Sanduiche(int quantidadeAdicionais) : base(Descricao, MaxIngredientes, PrecoBase, ValorAdicional)
        {
            init(quantidadeAdicionais);
        }
        public Sanduiche(bool temFritas) : base(Descricao, MaxIngredientes, PrecoBase, ValorAdicional)
        {
            if (temFritas)
            {
                _temFritas = TemCombo();
            }
        }
        #endregion


        #region Métodos de Negócio
        /// <summary>
        /// Método que recebe um valor inteiro de escolha(switch) e retorna true caso o usuário
        /// deseja combo com fritas e false caso não deseje.
        /// </summary>
        /// <param name="_temFritas"></param>
        /// <returns>Valor bool true se o cliente quiser combo com fritas e false caso não queira.</returns>
        private bool TemCombo()
        {
            return _temFritas = true;
        }

        /// <summary>
        /// Método para calcular o valor total dos adicionais, multiplicação da 
        /// quantidade de ingredientes e do valor base dos adicionais.
        /// </summary>
        /// <returns>Valor total dos adicionais</returns>
        protected override double ValorAdicionais()
        {
            return _quantidadeIngredientes * ValorAdicional;
        }

        /// <summary>
        /// Método para calcular e retornar o valor final do pedido, se houver combo adiciona o valor do combo.
        /// </summary>
        /// <returns>Valor total da compra do SANDUICHE. </returns>
        public override double ValorFinal()
        {
            if (TemCombo())
            {
                return PrecoBase + ValorAdicionais() + ValorCombo;
            }

            return PrecoBase + ValorAdicionais();
        }


        public override string NotaDeCompra()
        {
            return $"{Descricao} ({PrecoBase:C2}) com {_quantidadeIngredientes} ingredientes ({ValorAdicionais():C2}), no valor de {ValorFinal():C2}";
        }
        #endregion
    }
}
