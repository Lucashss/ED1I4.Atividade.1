using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ED1I4.Atividade1
{
    public class Vendedor
    {
        private int id;
        private string nome;
        private double percComissao;
        private Venda[] asVendas;

        private const int qtdDia = 31;

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public double PercComissao { get => percComissao; set => percComissao = value; }
        public Venda[] AsVendas { get => asVendas; }


        public Vendedor(int id, string nome, double percComissao)
        {
            this.id = id;
            this.nome = nome;
            this.percComissao = percComissao;
            inicializarVendas();
        }

        public Vendedor(int id) : this(id, "", 0.0)
        { }

        public Vendedor() : this(0, "", 0.0)
        { }

        public void registrarVenda(int dia, Venda venda)
        {
            asVendas[dia - 1] = venda;
        }
        public double valorVendas()
        {
            double somaValorVendas = 0;

            foreach (var venda in asVendas)
            {
                somaValorVendas += venda.Valor;
            }

            return somaValorVendas;
        }
        public double valorComissao() => valorVendas() * (this.percComissao/100);
        private void inicializarVendas()
        {
            this.asVendas = new Venda[qtdDia];
            for (int i = 0; i < qtdDia; i++)
            {
                asVendas[i] = new Venda();
            }
        }

        public override bool Equals(object obj) => this.id.Equals(((Vendedor)obj).Id);

        public override string ToString()
        {
            return $"Id: {id}\nNome: {nome}\nValor total das vendas: {valorVendas():C}\nValor comissão: {valorComissao():C}";
        }

    }
}
