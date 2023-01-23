﻿using System.ComponentModel.DataAnnotations;

namespace Backend_Final.Areas.Client.ViewModels.Product
{
    public class ListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; }

        public ListItemViewModel(int id, string title, decimal price, DateTime createdAt)
        {
            Id = id;
            Title = title;
            Price = price;
            CreatedAt = createdAt;
        }
    }
}
