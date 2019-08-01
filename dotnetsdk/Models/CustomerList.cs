﻿using System.Collections.Generic;

namespace evertech.sdk.Models
{
    public class CustomerList
    {
        public int Limit { get; set; }
        public int Skip { get; set; }
        public int Count { get; set; }
        public List<CustomerData> Charges { get; set; }
    }
}
