﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommand: IRequest<bool>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public decimal TotalPrice { get; set; }
        //billing address
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        //payment 
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CVV { get; set; }
        public string Expiration { get; set; }
        public int PaymentMethod { get; set; }

        public string UpdateBy { get; set; } = string.Empty;
        public DateTime? UpdateDate { get; set; } 
    }
}