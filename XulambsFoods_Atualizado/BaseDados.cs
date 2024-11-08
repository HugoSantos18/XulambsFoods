using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_Atualizado
{
    class BaseDados<T> where T : IComparable<T>, IEquatable<T>
    {
        private Dictionary<int, T> _baseDados;

        public BaseDados()
        {
            _baseDados = new Dictionary<int, T>();
        }

        public T Add(int id, T dados)
        {
            _baseDados.Add(id, dados);
            return dados;
        }

        public T Localizar(T dadosProcurado)
        {
            foreach (T dados in _baseDados.Values)
            {
                if (dados.Equals(dadosProcurado))
                {
                    return dados;
                }

            }
            return default(T);
        }

        public void Remover(T dadosAserRemovido)
        {
            foreach (T dados in _baseDados.Values)
                if (dados.Equals(dadosAserRemovido))
                {
                    _baseDados.Remove(dadosAserRemovido.GetHashCode());
                }
        }
    }
}
