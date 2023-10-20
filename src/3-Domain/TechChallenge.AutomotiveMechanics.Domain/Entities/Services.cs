﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge.AutomotiveMechanics.Domain.Entities
{
    public class Service : Entity
    {
        public string Name { get; set; }

        public int CarId { get; set; }

        public Car Car { get; set; }
    }
}
