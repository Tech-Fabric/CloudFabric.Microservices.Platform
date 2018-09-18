using System;
using System.Collections.Generic;
using System.Text;
using CloudFabric.BuisnessRules.Models;
using CloudFabric.BuisnessRules.Structs;

namespace CloudFabric.BuisnessRules.Enums
{
    public class IsTypeEnumFactory : IComparerFactory
    {
        public const string Is = "Is";
        public const string IsNot = "Is Not";

        public bool Compare<TStruct, T>(T field1, Comparer comparer, T field2) where TStruct : Structs.IComparable<T>, new()
        {
            var comparable = new TStruct();
            switch ((IsTypeEnum)comparer.Value)
            {
                case IsTypeEnum.Is: return comparable.IsEqual(field1, field2);
                case IsTypeEnum.IsNot: return !comparable.IsEqual(field1, field2);
            }
            throw new Exception("invalid comparison");
        }

        public bool Compare(object field1, Comparer comparer, object field2)
        {
            var isNumber = field1 is sbyte
            || field1 is byte
            || field1 is short
            || field1 is ushort
            || field1 is int
            || field1 is uint
            || field1 is long
            || field1 is ulong
            || field1 is float
            || field1 is double
            || field1 is decimal;

            if (isNumber)
            {
                return Compare<Comparable>(Convert.ToDouble(field1), comparer, Convert.ToDouble(field2));
            }
            else
            {
                return Compare<Comparable>(Convert.ToString(field1), comparer, Convert.ToString(field2));
            }
        }

        public bool Compare<TStruct>(int field1, Comparer comparer, int field2) where TStruct : Structs.IComparable<int>, new() => Compare<TStruct, int>(field1, comparer, field2);
        public bool Compare<TStruct>(double field1, Comparer comparer, double field2) where TStruct : Structs.IComparable<double>, new() => Compare<TStruct, double>(field1, comparer, field2);
        public bool Compare<TStruct>(long field1, Comparer comparer, long field2) where TStruct : Structs.IComparable<long>, new() => Compare<TStruct, long>(field1, comparer, field2);
        public bool Compare<TStruct>(float field1, Comparer comparer, float field2) where TStruct : Structs.IComparable<float>, new() => Compare<TStruct, float>(field1, comparer, field2);
        public bool Compare<TStruct>(string field1, Comparer comparer, string field2) where TStruct : Structs.IComparable<string>, new() => Compare<TStruct, string>(field1, comparer, field2);
    }
}
