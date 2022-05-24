using BibliotecaServer;
using BibliotecaServer.Models;
using Grpc.Core;

namespace BibliotecaServer.Services
{
    public class BibliotecaService : BibliotecaServiceGrpc.BibliotecaServiceGrpcBase
    {
        public static readonly List<LivroModel> Biblioteca = new List<LivroModel>
        {
            new LivroModel(0, "As 20 mil léguas submarinas", "Ficção" ),
            new LivroModel(1, "Viagem ao centro da Terra", "Ficção" ),
            new LivroModel(2, "It: a coisa", "Ficção" ),
        };

        public override Task<Livros> SelectAll(Empty request, ServerCallContext context)
        {
            Livros responseData = new Livros();
            var query = from liv in Biblioteca
                        select new Livro()
                        {
                            LivroID = liv.Id,
                            Name = liv.Nome,
                            Tipo = liv.Tipo
                        };

            responseData.Items.AddRange(query.ToArray());

            return Task.FromResult(responseData);
        }

        public override Task<Empty> Insert(Livro request, ServerCallContext context)
        {
            Biblioteca.Add(new LivroModel(request.LivroID, request.Name, request.Tipo));

            return Task.FromResult(new Empty());
        }

        public override Task<Livro> SelectByID(LivroFilter request, ServerCallContext context)
        {
            var data = Biblioteca.Find(livro => livro.Id == request.LivroID);

            Livro livro = new Livro() { LivroID = data.Id, Name = data.Nome, Tipo = data.Tipo };
            
            return Task.FromResult(livro);
        }

        public override Task<Empty> Update(Livro request, ServerCallContext context)
        {
            var data = Biblioteca.FirstOrDefault(livro => livro.Id == request.LivroID);

            if (data is not null)
            {
                data.Nome = request.Name;
                data.Tipo = request.Tipo;
            }

            return Task.FromResult(new Empty());
        }

        public override Task<Empty> Delete(LivroFilter request, ServerCallContext context)
        {
            var data = Biblioteca.FirstOrDefault(livro => livro.Id == request.LivroID);

            if(data is not null)
                Biblioteca.Remove(data);

            return Task.FromResult(new Empty());
        }
    }
}
