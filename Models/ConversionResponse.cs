namespace YapeServiceAPI.Models
{
    public class ConversionResponse
    {
        public decimal Monto { get; set; }
        public decimal MontoConTipoDeCambio { get; set; }
        public string MonedaOrigen { get; set; }
        public string MonedaDestino { get; set; }
        public decimal TipoDeCambio { get; set; }
    }
}
