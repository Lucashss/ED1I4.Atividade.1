using System;

namespace ED1I4.Atividade1
{
    /*
     * PARTICIPANTES
     * LUCAS HENRIQUE DE SOUZA SANTOS CB3012212
     * KETHELEEN CRISTINE SIMÃO DOS SANTOS
 --------------------------------
| Venda                            | 
|----------------------------------|
| - qtde: int                      |
| - valor: double                  |
|----------------------------------|
| + valorMedio(): double           |
------------------------------------

------------------------------------------------
| Vendedor                                     | 
|----------------------------------------------|
| - id: int                                    |
| - nome: string                               |
| - percComissao: double                       |
| - asVendas: Venda[31]                        |
|----------------------------------------------|
| + registrarVenda(int dia, Venda venda): void |
| + valorVendas(): double                      |
| + valorComissao(): double                    |
------------------------------------------------
------------------------------------------------
| Vendedores                                   | 
|----------------------------------------------|
| - osVendedores: Vendedor[]                   |
| - max: int                                   |
| - qtde: int                                  |
|----------------------------------------------|
| + addVendedor(Vendedor v): bool              |
| + delVendedor(Vendedor v): bool              |
| + searchVendedor(Vendedor v): Vendedor       |
| + valorVendas(): double                      |
| + valorComissao(): double                    |
------------------------------------------------
OPÇÕES:

0. Sair
1. Cadastrar vendedor (*)
2. Consultar vendedor (**)
3. Excluir vendedor   (***)
4. Registrar venda
5. Listar vendedores  (****)


(*)    - Limitar o quantitativo de vendedores cadastrados (máximo 10).

(**)   - Quando encontrado, deverá ser informado o id, nome, o valor total das vendas, o valor da comissão devida e  o valor médio das vendas diárias (de cada dia que houve registro de venda).

(***)  - O vendedor só poderá ser excluído enquanto não houver nenhuma venda associada a ele.

(****) - Deverá ser informado, para cada vendedor, o id, nome, valor total das vendas e o valor da comissão devida. Ao final da listagem, esses valores deverão ser totalizados.
    */
    internal class Program
    {
        public static void Main(string[] args)
        {
            int opcao = 1;
            Vendedores vendedores = new Vendedores(10);

            while (opcao != 0)
            {
                Console.WriteLine("0. Sair\n1.Cadastrar vendedor\n2.Consultar vendedor\n3.Excluir vendedor\n4.Registrar venda\n5.Listar vendedores\n______________________");
                Console.Write("\nDigite a opção desejada: ");
                opcao = int.Parse(Console.ReadLine());
                Console.Clear();
                separador();

                switch (opcao)
                {
                    case 0:
                        sair();
                        break;
                    case 1:
                        cadastrarVendedor(vendedores);
                        break;
                    case 2:
                        consultarVendedor(vendedores);
                        break;
                    case 3:
                        excluirVendedor(vendedores);
                        break;
                    case 4:
                        registrarVenda(vendedores);
                        break;
                    case 5:
                        listarVendedores(vendedores);
                        break;
                    default:
                        Console.WriteLine("\n\nOpção invalida, selecione um valor entre 0 e 5\n\n");
                        break;
                }
                separador();
            }
        }

        private static void listarVendedores(Vendedores vendedores)
        {
            Console.WriteLine("Listagem dos vendedores: \n\n");
            foreach (var vendedor in vendedores.OsVendedores)
            {
                if (vendedor.Id != 0)
                {
                    separador();
                    Console.WriteLine(vendedor.ToString());
                }
            }
        }

        private static void registrarVenda(Vendedores vendedores)
        {
            int dia;
            Venda venda = new Venda();
            Vendedor vendedor = new Vendedor();

            Console.Write("Digite o id do vendedor: ");
            vendedor.Id = int.Parse(Console.ReadLine());

            vendedor = vendedores.searchVendedor(vendedor);

            if (vendedor.Id == 0)
            {
                Console.WriteLine("\nNão foi possivel encontrar o vendedor");
                return;
            }

            Console.Write("Digite o dia da venda: ");
            dia = int.Parse(Console.ReadLine());

            Console.Write("Digite a quantidade de vendas do dia: ");
            venda.Qtde = int.Parse(Console.ReadLine());

            Console.Write("Digite o valor total das vendas: ");
            venda.Valor = double.Parse(Console.ReadLine());

            vendedor.registrarVenda(dia, venda);

            Console.WriteLine("\nVenda registrada com sucesso!");
        }

        private static void excluirVendedor(Vendedores vendedores)
        {
            Vendedor vendedor = new Vendedor();

            Console.Write("Digite o id do vendedor: ");
            vendedor.Id = int.Parse(Console.ReadLine());

            vendedor = vendedores.searchVendedor(vendedor);

            if(vendedor.Id == 0)
            {
                Console.WriteLine("\nNão foi possivel encontrar o vendedor");
                return;
            }

            if(vendedor.valorVendas() > 0)
            {
                Console.WriteLine("\nVendedor não pode ser excluido pois possui vendas associadas");
                return;
            }

            bool excluidoComSucesso = vendedores.delVendedor(vendedor);

            if (excluidoComSucesso)
            {
                Console.WriteLine("\nVendedor excluido com sucesso!");
            }
            else
            {
                Console.WriteLine("\nNão foi possivel excluir o vendedor");
            }
        }

        private static void consultarVendedor(Vendedores vendedores)
        {
            Vendedor vendedor = new Vendedor();

            Console.Write("Digite o id do vendedor: ");
            vendedor.Id = int.Parse(Console.ReadLine());

            vendedor = vendedores.searchVendedor(vendedor);

            if (vendedor.Id == 0)
            {
                Console.WriteLine("\nNão foi possivel encontrar o vendedor");
                return;
            }

            Console.WriteLine($"\n{vendedor.ToString()}");
            if (vendedor.valorVendas() > 0) {
                Console.WriteLine("Vendas: \n");
                for (int i = 0; i < vendedor.AsVendas.Length; i++)
                {
                    Venda venda = vendedor.AsVendas[i];
                    if (venda.Qtde > 0)
                    {
                        Console.WriteLine($"Dia: {i + 1} - {venda.ToString()}");
                    }
                }
            }
        }

        private static void cadastrarVendedor(Vendedores vendedores)
        {
            Vendedor vendedor = new Vendedor();

            Console.Write("Digite o id do vendedor: ");
            vendedor.Id = int.Parse(Console.ReadLine());

            Console.Write("Digite o nome do vendedor: ");
            vendedor.Nome = Console.ReadLine();

            Console.Write("Digite o percentual de comissão do vendedor: ");
            vendedor.PercComissao = double.Parse(Console.ReadLine());

            bool cadastradoComSucesso = vendedores.addVendedor(vendedor);

            if (cadastradoComSucesso)
            {
                Console.WriteLine("\nCadastrado com sucesso!");
            }
            else
            {
                Console.WriteLine("\nNão foi possivel cadastrar");
            }
        }

        private static void sair()
        {
            Console.Clear();
            Console.WriteLine("*** PRESSIONE QUALQUER TECLA PARA FINALIZAR ***");
            Console.ReadKey();
        }

        private static void separador()
        {
            Console.WriteLine();
            for (int i = 0; i < 20; i++)
            {
                Console.Write("=*");
            }
            Console.WriteLine("\n");
        }
    }
}
