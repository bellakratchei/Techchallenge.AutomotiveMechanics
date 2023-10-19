﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge.AutomotiveMechanics.Services.Business.Result
{
    public class CarResult
    {
        public int Id { get; set; }

        public int YearManufactured { get; set; }

        public string Plate { get; set; }   

        public DateTime CreatedDate { get; set; }   

        public DateTime? LastModifiedDate { get; set; }
    }
}