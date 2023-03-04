using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace L01_2020GA603_BLOG.Models
{
    public class comentarios
    {
        [Key]
        public int cometarioId { get; set; }
        public int publicacionId { get; set; }
        public string? comentario { get; set; }
        public int usuarioId  { get; set; }
}
}
