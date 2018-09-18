using CloudFabric.BuisnessRules.Models;
using CloudFabric.BuisnessRules.Structs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFabric.BuisnessRules.Enums
{
    public interface IComparerFactory
    {
        bool Compare<TStruct, T>(T field1, Comparer comparer, T field2) where TStruct : Structs.IComparable<T>, new();

        bool Compare(object field1, Comparer comparer, object field2);
        bool Compare<TStruct>(int field1, Comparer comparer, int field2) where TStruct : Structs.IComparable<int>, new();
        bool Compare<TStruct>(double field1, Comparer comparer, double field2) where TStruct : Structs.IComparable<double>, new();
        bool Compare<TStruct>(long field1, Comparer comparer, long field2) where TStruct : Structs.IComparable<long>, new();
        bool Compare<TStruct>(float field1, Comparer comparer, float field2) where TStruct : Structs.IComparable<float>, new();
        bool Compare<TStruct>(string field1, Comparer comparer, string field2) where TStruct : Structs.IComparable<string>, new();
    }
}
