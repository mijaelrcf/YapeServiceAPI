using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YapeServiceAPI.Models;

namespace YapeServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CambioMonedaController : ControllerBase
    {
        [HttpPost("convertir")]
        public ActionResult<ConversionResponse> ConvertirMoneda([FromBody] ConversionRequest request)
        {
            decimal tipoDeCambio = ObtenerTipoDeCambio(request.MonedaOrigen, request.MonedaDestino);

            if (tipoDeCambio == 0)
            {
                return BadRequest("No se encontró un tipo de cambio válido para las monedas proporcionadas.");
            }

            decimal montoConvertido = request.Monto * tipoDeCambio;

            var response = new ConversionResponse
            {
                Monto = request.Monto,
                MontoConTipoDeCambio = montoConvertido,
                MonedaOrigen = request.MonedaOrigen,
                MonedaDestino = request.MonedaDestino,
                TipoDeCambio = tipoDeCambio
            };

            return Ok(response);
        }

        private decimal ObtenerTipoDeCambio(string monedaOrigen, string monedaDestino)
        {
            if (monedaOrigen == "BOB" && monedaDestino == "USD")
            {
                return 0.14m;
            }
            else if (monedaOrigen == "BOB" && monedaDestino == "EUR")
            {
                return 0.13m;
            }
            else if (monedaOrigen == "USD" && monedaDestino == "BOB")
            {
                return 6.91m;
            }
            else if (monedaOrigen == "EUR" && monedaDestino == "BOB")
            {
                return 7.55m;
            }
            else if (monedaOrigen == "USD" && monedaDestino == "EUR")
            {
                return 0.92m;
            }
            else if (monedaOrigen == "EUR" && monedaDestino == "USD")
            {
                return 1.09m;
            }

            return 0;
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("No se ha subido ninguna imagen.");
            }

            // Define la ruta interna donde se almacenará la imagen
            var rutaInterna = Path.Combine("C:/temp", image.FileName);

            // Crea el directorio si no existe
            Directory.CreateDirectory(Path.GetDirectoryName(rutaInterna));

            // Guarda la imagen en la ruta especificada
            using (var stream = new FileStream(rutaInterna, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return Ok(new { Ruta = rutaInterna, Mensaje = "Imagen subida correctamente." });
        }
    }
}
