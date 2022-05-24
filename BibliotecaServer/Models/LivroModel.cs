namespace BibliotecaServer.Models
{
    public class LivroModel
    {
        public LivroModel(int id, string nome, string tipo)
        {
            Id = id;
            Nome = nome;
            Tipo = tipo;
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }
    }
}
