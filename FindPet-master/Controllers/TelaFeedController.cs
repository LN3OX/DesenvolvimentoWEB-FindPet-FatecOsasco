using Microsoft.AspNetCore.Mvc;
using findPet.Models;
using System.Text.Json;
using Newtonsoft.Json;

namespace findPet.Controllers
{
    public class TelaFeedController : Controller
    {
        // Lista estática para simular um banco de dados
        private static List<TelaFeedModel> _publicacoes = new List<TelaFeedModel>();
        private static int _proximoId = 1;

        [HttpGet]
        public IActionResult Index()
        {
            // Verifica se há uma nova publicação vinda do TempData
            if (TempData.ContainsKey("TelaPublicacaoModel"))
            {
                var publicacaoData = JsonConvert.DeserializeObject<TelaPublicacaoFeed>((string)TempData["TelaPublicacaoModel"]);
                
                // Converte para TelaFeedModel e adiciona à lista
                var novaPublicacao = new TelaFeedModel
                {
                    Id = _proximoId++,
                    Nome = publicacaoData.Nome,
                    Raca = publicacaoData.Raca,
                    Cor = publicacaoData.Cor,
                    Porte = publicacaoData.Porte,
                    LocalDesaparecimento = publicacaoData.LocalDesaparecimento,
                    DataDesaparecimento = publicacaoData.DataDesaparecimento,
                    Chip = publicacaoData.Chip,
                    Legenda = publicacaoData.Legenda,
                    ImageFileName = publicacaoData.ImageFileName,
                    DataPublicacao = DateTime.Now
                };

                _publicacoes.Insert(0, novaPublicacao); // Adiciona no início da lista
            }

            // Ordena as publicações por data (mais recentes primeiro)
            var publicacoesOrdenadas = _publicacoes.OrderByDescending(p => p.DataPublicacao).ToList();
            
            return View(publicacoesOrdenadas);
        }

        [HttpPost]
        public IActionResult Curtir(int publicacaoId)
        {
            var publicacao = _publicacoes.FirstOrDefault(p => p.Id == publicacaoId);
            if (publicacao != null)
            {
                publicacao.Curtidas++;
            }

            return Json(new { success = true, curtidas = publicacao?.Curtidas ?? 0 });
        }

        [HttpPost]
        public IActionResult Comentar(int publicacaoId, string textoComentario, string nomeUsuario = "Usuário Anônimo")
        {
            var publicacao = _publicacoes.FirstOrDefault(p => p.Id == publicacaoId);
            if (publicacao != null && !string.IsNullOrWhiteSpace(textoComentario))
            {
                var novoComentario = new Comentario
                {
                    Id = publicacao.Comentarios.Count + 1,
                    NomeUsuario = nomeUsuario,
                    Texto = textoComentario,
                    DataComentario = DateTime.Now
                };

                publicacao.Comentarios.Add(novoComentario);
            }

            return Json(new { 
                success = true, 
                totalComentarios = publicacao?.Comentarios.Count ?? 0,
                comentario = publicacao?.Comentarios.LastOrDefault()
            });
        }

        [HttpPost]
        public IActionResult Compartilhar(int publicacaoId)
        {
            var publicacao = _publicacoes.FirstOrDefault(p => p.Id == publicacaoId);
            if (publicacao != null)
            {
                publicacao.Compartilhamentos++;
            }

            return Json(new { success = true, compartilhamentos = publicacao?.Compartilhamentos ?? 0 });
        }

        [HttpGet]
        public IActionResult ObterComentarios(int publicacaoId)
        {
            var publicacao = _publicacoes.FirstOrDefault(p => p.Id == publicacaoId);
            if (publicacao != null)
            {
                return Json(new { 
                    success = true, 
                    comentarios = publicacao.Comentarios.Select(c => new {
                        id = c.Id,
                        nomeUsuario = c.NomeUsuario,
                        texto = c.Texto,
                        dataComentario = c.DataComentario.ToString("dd/MM/yyyy HH:mm")
                    })
                });
            }

            return Json(new { success = false });
        }
    }
}

