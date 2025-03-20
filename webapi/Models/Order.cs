using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[Table("orders")]
public class Order
{
    [Key]
    [Column("order_id")]
    public int OrderId { get; set; }
    [ForeignKey("Customer")]
    [Column("customer_id")]
    public int CustomerId { get; set; }
    [Required]
    [Column("order_details")]
    [JsonPropertyName("orderDetails")]
    public string OrderDetails { get; set; }
    [Column("order_date")]
    public DateTime OrderDate { get; set; } = DateTime.Now;
    [Required]
    [Column("order_total_amount")]
    [JsonPropertyName("amount")]
    public decimal OrderTotalAmount { get; set; }

    [JsonIgnore]
    public Customer? Customer { get; set; }
}
