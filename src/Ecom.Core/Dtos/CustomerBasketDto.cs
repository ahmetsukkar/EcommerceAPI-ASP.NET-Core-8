﻿using Ecom.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public Guid Id { get; set; }
        public List<BasketItemDto> BasketItems { get; set; } = new List<BasketItemDto>();

        public Guid? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }

        public decimal ShippingPrice { get; set; }
    }
}
