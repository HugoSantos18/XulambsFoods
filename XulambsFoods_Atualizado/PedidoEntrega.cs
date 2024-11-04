using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_Atualizado
{
    internal class PedidoEntrega : IPedido
    {
        private const int MaxEntrega = 6;
        private readonly double[] TaxasEntrega = { 0, 5, 8 };
        private readonly double[] DistanciasEntrega = { 4, 8, double.MaxValue };
        private int _quantComidas;
        private bool _aberto;
        private List<Comida> _comidas;
        private double _distanciaEntrega;

        public PedidoEntrega(double distancia)
        {
            if (distancia < 0.1) distancia = 0.1;
            _distanciaEntrega = distancia;
            _aberto = true;
            _comidas = new List<Comida>();
        }
        public bool PodeAdicionar()
        {
            return _aberto && (_quantComidas < MaxEntrega);
        }

        public double ValorTaxa()
        {
            double taxa = 0d;
            for (int i = DistanciasEntrega.Length - 1; i >= 0; i--)
            {
                if (_distanciaEntrega <= DistanciasEntrega[i])
                    taxa = TaxasEntrega[i];
            }
            return taxa;
        }

       
        public string Relatorio()
        {
            StringBuilder relat = new StringBuilder("XULAMBS PIZZA - Pedido para entrega");
            relat.AppendLine("=============================");

            relat.AppendLine($"\nTAXA ENTREGA : {ValorTaxa():C2}");
            relat.AppendLine("=============================");
            return relat.ToString();
        }
  
    }
}
