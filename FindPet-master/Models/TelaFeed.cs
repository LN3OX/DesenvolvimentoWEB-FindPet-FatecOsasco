using System.ComponentModel.DataAnnotations;

namespace findPet.Models
{
    public class TelaFeedModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Raca { get; set; } = string.Empty;
        public string Cor { get; set; } = string.Empty;
        public string Porte { get; set; } = string.Empty;
        public string LocalDesaparecimento { get; set; } = string.Empty;
        public DateTime DataDesaparecimento { get; set; }
        public string Chip { get; set; } = string.Empty;
        public string Legenda { get; set; } = string.Empty;
        public string ImageFileName { get; set; } = string.Empty;
        public int Curtidas { get; set; } = 0;
        public List<Comentario> Comentarios { get; set; } = new List<Comentario>();
        public int Compartilhamentos { get; set; } = 0;
        public DateTime DataPublicacao { get; set; } = DateTime.Now;
    }

    public class Comentario
    {
        public int Id { get; set; }
        public string NomeUsuario { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
        public DateTime DataComentario { get; set; } = DateTime.Now;
    }

    public class InteracaoModel
    {
        public int PublicacaoId { get; set; }
        public string TipoInteracao { get; set; } = string.Empty; // "curtir", "comentar", "compartilhar"
        public string TextoComentario { get; set; } = string.Empty;
        public string NomeUsuario { get; set; } = string.Empty;
    }
}

