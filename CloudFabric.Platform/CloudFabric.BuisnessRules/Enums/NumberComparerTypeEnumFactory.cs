using System;
using CloudFabric.BuisnessRules.Models;
using CloudFabric.BuisnessRules.Structs;

namespace CloudFabric.BuisnessRules.Enums
{
    public class NumberComparerTypeEnumFactory : IComparerFactory
    {
        public bool Compare<TStruct, T>(T field1, Comparer comparer, T field2) where TStruct : Structs.IComparable<T>, new()
        {
            var comparable = new TStruct();
            switch ((NumberComparerTypeEnum)comparer.Value)
            {
                case NumberComparerTypeEnum.LessThanOrEqual: return comparable.IsLessThanOrEqualTo(field1, field2);
                case NumberComparerTypeEnum.LessThan: return comparable.IsLessThan(field1, field2);
                case NumberComparerTypeEnum.EqualTo: return comparable.IsEqual(field1, field2);
                case NumberComparerTypeEnum.GreaterThan: return comparable.IsGreaterThan(field1, field2);
                case NumberComparerTypeEnum.GreaterThanOrEqual: return comparable.IsGreaterThan(field1, field2);
            }
            return false;
        }

        public bool Compare<TStruct>(int field1, Comparer comparer, int field2) where TStruct : Structs.IComparable<int>, new() => Compare<TStruct, int>(field1, comparer, field2);
        public bool Compare<TStruct>(double field1, Comparer comparer, double field2) where TStruct : Structs.IComparable<double>, new() => Compare<TStruct, double>(field1, comparer, field2);
        public bool Compare<TStruct>(long field1, Comparer comparer, long field2) where TStruct : Structs.IComparable<long>, new() => Compare<TStruct, long>(field1, comparer, field2);
        public bool Compare<TStruct>(float field1, Comparer comparer, float field2) where TStruct : Structs.IComparable<float>, new() => Compare<TStruct, float>(field1, comparer, field2);
        public bool Compare<TStruct>(string field1, Comparer comparer, string field2) where TStruct : Structs.IComparable<string>, new() => Compare<TStruct, string>(field1, comparer, field2);
    }
}
