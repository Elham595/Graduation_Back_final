﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace graduation_project.Models
{
    public partial class GetOfferForTailorResult
    {
        public int OfferId { get; set; }
        public int DesignOrderNumber { get; set; }
        public string ClientUserName { get; set; }
        public string TailorUserName { get; set; }
        public DateTime OfferDate { get; set; }
        public int NumberOfDays { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}
