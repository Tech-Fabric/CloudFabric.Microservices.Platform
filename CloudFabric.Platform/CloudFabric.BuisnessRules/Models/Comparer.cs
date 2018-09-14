using CloudFabric.BuisnessRules.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFabric.BuisnessRules.Models
{
    public class Comparer
    {
        public Comparer(ComparerTypeEnum type, int value)
        {
            Type = type;
            Value = value;
        }
        public ComparerTypeEnum Type { get; }
        public int Value { get; }
    }
}
