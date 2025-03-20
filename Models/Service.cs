using PetPals_BackEnd_Group_9.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("services")]
public class Service
{
    [Key]
    [Column("service_id")] 
    public int ServiceId { get; set; }

    [Column("provider_id")]
    public int ProviderId { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("category_id")]
    public int CategoryId { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    [Column("address")]
    public string? Address { get; set; }

    [Column("city")]
    public string? City { get; set; }

   
    public virtual User Provider { get; set; }
    public virtual ServiceCategory Category { get; set; }
}
