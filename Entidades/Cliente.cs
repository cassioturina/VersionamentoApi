using System;

namespace VersionamentoApi.Entidades
{
    public class Cliente
    {
        public Cliente(int id, string nome)
        {
            Nome = nome;
            Id = id;
        }
        public Cliente(int id, string nome, string sobreNome) : this(id, nome)
        {
            SobreNome = sobreNome;
        }


        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string SobreNome { get; private set; }

    }
}
