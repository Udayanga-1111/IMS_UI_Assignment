namespace IMS_UI_Assignment.Models
{
    public class Order
    {
        public int _orderId { get; set; }
        public string? _orderName { get; set; }
        public string? _productName { get; set; }
        public string? _supplier { get; set; }
        public int Quantity { get; set; }
    }
}
