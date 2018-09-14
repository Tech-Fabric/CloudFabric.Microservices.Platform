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
            
            switch ((IsTypeEnum)comparer.Value)
            {
                case IsTypeEnum.Is: return field1.Equals(field2);
                case IsTypeEnum.IsNot: return !field1.Equals(field2);
            }
            throw new Exception("invalid comparison");
        }

        public bool Compare<TStruct>(int field1, Comparer comparer, int field2) where TStruct : Structs.IComparable<int>, new() => Compare<TStruct, int>(field1, comparer, field2);
        public bool Compare<TStruct>(double field1, Comparer comparer, double field2) where TStruct : Structs.IComparable<double>, new() => Compare<TStruct, double>(field1, comparer, field2);
        public bool Compare<TStruct>(long field1, Comparer comparer, long field2) where TStruct : Structs.IComparable<long>, new() => Compare<TStruct, long>(field1, comparer, field2);
        public bool Compare<TStruct>(float field1, Comparer comparer, float field2) where TStruct : Structs.IComparable<float>, new() => Compare<TStruct, float>(field1, comparer, field2);
        public bool Compare<TStruct>(string field1, Comparer comparer, string field2) where TStruct : Structs.IComparable<string>, new() => Compare<TStruct, string>(field1, comparer, field2);
    }
}
