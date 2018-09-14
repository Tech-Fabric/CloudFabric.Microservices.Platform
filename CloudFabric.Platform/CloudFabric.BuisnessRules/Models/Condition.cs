﻿using CloudFabric.BuisnessRules.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFabric.BuisnessRules.Models
{
    public class Condition
    {
        public Condition(Object val1, Comparer comparer, Object val2, ConnectorTypeEnum? connector)
        {
            Value1 = val1;
            Comparer = comparer;
            Value2 = val2;
            Connector = connector;
        }

        public Object Value1 { get; }
        public Comparer Comparer { get; }
        public Object Value2 { get; }
        public ConnectorTypeEnum? Connector { get; }

        public bool IsTrue()
        {

            return false;
        }
    }
}
