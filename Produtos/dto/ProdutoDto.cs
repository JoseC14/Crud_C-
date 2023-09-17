namespace Produto.Dto
{

    //Metodos get e set
    public class ProdutoDto
    {
        private string nome;
        private float preco;
        private string validade;
        private string categoria;

        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        public float Preco
        {
            get { return preco; }
            set { preco = value; }
        }

        public string Validade
        {
            get { return validade; }
            set { validade = value; }
        }

        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; }
        }

    }
}