using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

[Table("customers")]
public class Customer
{
    [Key]
    [Column("customer_id")]
    public int CustomerId { get; set; }
    [Required]
    [Column("customer_name")]
    [JsonPropertyName("name")]
    public string CustomerName { get; set; }
    [Required]
    [Column("customer_street_address")]
    [JsonPropertyName("address")]
    public string CustomerStreetAddress { get; set; }
    [Required]
    [Column("city")]
    public string City { get; set; }
    [Required]
    [Column("state")]
    public string State { get; set; }
    [Required]
    [Column("postal_code")]
    public string PostalCode { get; set; }
    [Column("create_date")]
    public DateTime CreateDate { get; set; } = DateTime.Now;
}
