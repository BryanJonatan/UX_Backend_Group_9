﻿using System.ComponentModel.DataAnnotations;

namespace PetPals_BackEnd_Group_9.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        public int ProviderId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public ServiceCategory Category { get; set; } 
        public User Provider { get; set; } 
    }
}
