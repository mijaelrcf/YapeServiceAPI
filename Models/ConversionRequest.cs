namespace YapeServiceAPI.Models
{
    public class ConversionRequest
    {
        public decimal Monto { get; set; }
        public string MonedaOrigen { get; set; }
        public string MonedaDestino { get; set; }
    }
}
