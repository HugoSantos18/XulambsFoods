using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_Atualizado
{
    public class XulambsFoods
    {
        static List<Pedido> _pedidos= new List<Pedido>();

        static void Cabecalho()
        {
            Console.Clear();
            Console.WriteLine("XULAMBS FOODS");
            Console.WriteLine("=============");
        }

        static void Pausa()
        {
            Console.WriteLine("\nDigite ENTER para continuar.");
            Console.ReadKey();
        }
        static int ExibirMenu()
        {
            Cabecalho();
            Console.WriteLine("1 - Abrir Pedido");
            Console.WriteLine("2 - Alterar Pedido");
            Console.WriteLine("3 - Relatório de Pedido");
            Console.WriteLine("4 - Encerrar Pedido");
            Console.WriteLine("0 - Finalizar");
            Console.Write("Digite sua escolha: ");
            return int.Parse(Console.ReadLine());
        }

        static Comida EscolherTipoComida()
        {
            Comida comida;
            int escolha;

            Cabecalho();

            Console.WriteLine("Escolhendo uma comida.");
            Console.WriteLine("1 - Pizza.");
            Console.WriteLine("2 - Sanduiche.");
            Console.Write("Escolha um tipo de comida: ");
            escolha = int.Parse(Console.ReadLine());

            if (escolha == 1)
            {
                comida = new Pizza();
            }
            else if (escolha == 2)
            {
                comida = CriarSanduiche();
            }
            else
            {
                throw new InvalidOperationException("Opção inválida!");
            }

            return comida;
        }

        static Sanduiche CriarSanduiche()
        {
            Console.WriteLine($"Deseja adicionar o combo de fritas por mais R$5,00?");
            Console.WriteLine("1 - SIM");
            Console.WriteLine("2 - NÃO");
            Console.WriteLine("Digite a opção que deseja: ");
            int escolha = int.Parse(Console.ReadLine());

            if (escolha == 1)
            {
                return new Sanduiche(true);
            }
            else if (escolha == 2)
            {
                return new Sanduiche(false);
            }

            throw new InvalidOperationException("Operação inválida!");
        }

        static Pedido EscolherTipoPedido()
        {
            Pedido novoPedido;
            int escolha;
            Cabecalho();

            Console.WriteLine("Abrindo um novo Pedido.");
            Console.WriteLine("1 - Pedido Local.");
            Console.WriteLine("2 - Pedido para Entrega.");
            Console.Write("Escolha um tipo de pedido: ");
            escolha = int.Parse(Console.ReadLine());

            if (escolha == 1)
            {
                novoPedido = new PedidoLocal();
            }
            else if (escolha == 2)
            {
                novoPedido = CriarPedidoEntrega();
            }
            else
            {
                throw new InvalidOperationException("Opção inválida!");
            }

            return novoPedido;
        }

        private static Pedido CriarPedidoEntrega()
        {
            double dist;
            Console.WriteLine("\nPedido para Entrega.");
            Console.Write("Qual a distância da entrega? ");
            dist = double.Parse(Console.ReadLine());
            
            return new Pedido(dist);
        }

        static Pedido AbrirPedido()
        {
            Cabecalho();
            Pedido novoPedido = EscolherTipoPedido();
            Comida comida = EscolherTipoComida();
            Console.WriteLine(novoPedido.Relatorio());
            Pausa();
            AdicionarComida(novoPedido);
            return novoPedido;
        }

        private static void AdicionarComida(Pedido procurado)
        {
            String escolha = "n";
            do
            {
                RelatorioPedido(procurado);
                Comida novaComida = ComprarComida();
                procurado.Adicionar(novaComida);
                Console.Write("\nDeseja outra comida? (s/n) ");
                escolha = Console.ReadLine();
            } while (escolha.ToLower().Equals("s"));
        }

        static Comida ComprarComida()
        {
            Console.WriteLine("\nComprando uma nova comida:");
            Comida novaComida = EscolherTipoComida();
            EscolherIngredientes(novaComida);
            MostrarNota(novaComida);

            return novaComida;
        }

        static void EscolherIngredientes(Comida comida)
        {
            Console.Write("Quantos adicionais você deseja? (máx. 8): ");
            int adicionais = int.Parse(Console.ReadLine());
            comida.AdicionarIngredientes(adicionais);
        }

        static void MostrarNota(Comida comida)
        {
            Console.WriteLine("\n" + comida.NotaDeCompra());
        }
        static void RelatorioPedido(Pedido pedido)
        {
            Cabecalho();
            Console.WriteLine(pedido.Relatorio() + "\n");
        }
        static Pedido LocalizarPedido(List<Pedido> pedidos)
        {
            Cabecalho();
            int id;
            Console.WriteLine("Localizando um pedido.");
            Console.Write("ID do pedido: ");
            id = int.Parse(Console.ReadLine());

            foreach (Pedido ped in pedidos)
            {
                if (ped.Relatorio().Contains("Pedido " + id.ToString("D2")))
                    return ped;
            }
            return null;
        }



        static void Main(string[] args)
        {
            List<Pedido> todosOsPedidos = new List<Pedido>();
            Pedido pedido;
            int opcao = -1;
            do
            {
                opcao = ExibirMenu();
                switch (opcao)
                {
                    case 1:
                        pedido = AbrirPedido();
                        todosOsPedidos.Add(pedido);
                        RelatorioPedido(pedido);

                        break;
                    case 2:
                        pedido = LocalizarPedido(todosOsPedidos);
                        if (pedido != null)
                            AdicionarComida(pedido);
                        else
                            Console.WriteLine("Pedido não existente.");
                        break;
                    case 3:
                        pedido = LocalizarPedido(todosOsPedidos);
                        if (pedido != null)
                            Console.WriteLine(pedido.Relatorio());
                        else
                            Console.WriteLine("Pedido não existente.");
                        break;
                    case 4:
                        pedido = LocalizarPedido(todosOsPedidos);
                        if (pedido != null)
                        {
                            pedido.FecharPedido();
                            Console.WriteLine("Pedido encerrado: ");
                            Console.WriteLine(pedido.Relatorio());
                        }
                        else
                            Console.WriteLine("Pedido não existente.");
                        break;

                }
                Pausa();
            } while (opcao != 0);

        }
    }
}
