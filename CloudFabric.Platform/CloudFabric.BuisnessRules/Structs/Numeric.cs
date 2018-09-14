using System;
using System.Collections.Generic;
using System.Text;

namespace CloudFabric.BuisnessRules.Structs
{
    public interface IComparable<T>
    {
        bool IsLessThanOrEqualTo(T val1, T val2);
        bool IsLessThan(T val1, T val2);
        bool IsEqual(T val1, T val2);
        bool IsGreaterThan(T val1, T val2);
        bool IsGreaterThanOrEqualTo(T val1, T val2);
    }
    public struct Comparable :
        IComparable<int>,
        IComparable<double>,
        IComparable<float>,
        IComparable<long>,
        IComparable<short>,
        IComparable<DateTime>
    {
        bool IComparable<int>.IsLessThanOrEqualTo(int val1, int val2) => val1 <= val2;
        bool IComparable<int>.IsLessThan(int val1, int val2) => val1 < val2;
        bool IComparable<int>.IsEqual(int val1, int val2) => val1 == val2;
        bool IComparable<int>.IsGreaterThan(int val1, int val2) => val1 > val2;
        bool IComparable<int>.IsGreaterThanOrEqualTo(int val1, int val2) => val1 >= val2;

        bool IComparable<double>.IsLessThanOrEqualTo(double val1, double val2) => val1 <= val2;
        bool IComparable<double>.IsLessThan(double val1, double val2) => val1 < val2;
        bool IComparable<double>.IsEqual(double val1, double val2) => val1 == val2;
        bool IComparable<double>.IsGreaterThan(double val1, double val2) => val1 > val2;
        bool IComparable<double>.IsGreaterThanOrEqualTo(double val1, double val2) => val1 >= val2;

        bool IComparable<float>.IsLessThanOrEqualTo(float val1, float val2) => val1 <= val2;
        bool IComparable<float>.IsLessThan(float val1, float val2) => val1 < val2;
        bool IComparable<float>.IsEqual(float val1, float val2) => val1 == val2;
        bool IComparable<float>.IsGreaterThan(float val1, float val2) => val1 > val2;
        bool IComparable<float>.IsGreaterThanOrEqualTo(float val1, float val2) => val1 >= val2;

        bool IComparable<long>.IsLessThanOrEqualTo(long val1, long val2) => val1 <= val2;
        bool IComparable<long>.IsLessThan(long val1, long val2) => val1 < val2;
        bool IComparable<long>.IsEqual(long val1, long val2) => val1 == val2;
        bool IComparable<long>.IsGreaterThan(long val1, long val2) => val1 > val2;
        bool IComparable<long>.IsGreaterThanOrEqualTo(long val1, long val2) => val1 >= val2;

        bool IComparable<short>.IsLessThanOrEqualTo(short val1, short val2) => val1 <= val2;
        bool IComparable<short>.IsLessThan(short val1, short val2) => val1 < val2;
        bool IComparable<short>.IsEqual(short val1, short val2) => val1 == val2;
        bool IComparable<short>.IsGreaterThan(short val1, short val2) => val1 > val2;
        bool IComparable<short>.IsGreaterThanOrEqualTo(short val1, short val2) => val1 >= val2;

        bool IComparable<DateTime>.IsLessThanOrEqualTo(DateTime val1, DateTime val2) => DateTime.Compare(val1, val2) <= 0;
        bool IComparable<DateTime>.IsLessThan(DateTime val1, DateTime val2) => DateTime.Compare(val1, val2) < 0;
        bool IComparable<DateTime>.IsEqual(DateTime val1, DateTime val2) => DateTime.Compare(val1, val2) == 0;
        bool IComparable<DateTime>.IsGreaterThan(DateTime val1, DateTime val2) => DateTime.Compare(val1, val2) > 0;
        bool IComparable<DateTime>.IsGreaterThanOrEqualTo(DateTime val1, DateTime val2) => DateTime.Compare(val1, val2) >= 0;
    }
}
