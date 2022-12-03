using boletoAPI.Domain;
using boletoAPI.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AspNet_UploadImagem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoletoController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        public BoletoController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet]
        public string Get()
        {
            return "Serviço Funcionando";
        }

        [HttpPost("upload")]
        [Obsolete]
        public Task<Retorno> EnviaArquivo([FromForm] IFormFile arquivo)
        {
            Retorno retorno = new Retorno();
            if (arquivo.Length > 0)
            {
                try
                {

                    using (var ms = new MemoryStream())
                    {
                        arquivo.CopyTo(ms);
                        return Task.FromResult(Reader.GetCodeBar(ms));
                    }


                }
                catch (Exception ex)
                {
                    retorno.codigo = 2;
                    retorno.mensagem = ex.ToString();
                    return Task.FromResult(retorno);
                }
            }
            else
            {
                retorno.codigo = 3;
                retorno.mensagem = "Ocorreu uma falha no envio do arquivo";
                return Task.FromResult(retorno);
            }
        }
    }
}