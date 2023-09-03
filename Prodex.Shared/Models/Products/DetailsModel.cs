﻿using Prodex.Shared.Utils;

namespace Prodex.Shared.Models.Products
{
    public class DetailsModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public KeyValueResult Template { get; set; }
        public KeyValueResult Status { get; set; }
        public List<ApiButton> Buttons { get; set; }
    }
}
