using CloudFabric.BuisnessRules.Enums;
using CloudFabric.BuisnessRules.Models;
using CloudFabric.BuisnessRules.Structs;
using System;
using Xunit;

namespace CloudFabric.BuisnessRules.Tests
{
    public class Is
    {
        private static Comparer _comparer = new Comparer(ComparerTypeEnum.NumberComparer, (int)IsTypeEnum.Is);
        private static IsTypeEnumFactory isComparer = new IsTypeEnumFactory();

        int intV1 = 1;
        int intV2 = 2;

        float floatV1 = 1;
        float floatV2 = 2;

        double doubleV1 = 1;
        double doubleV2 = 2;

        long longV1 = 1;
        long longV2 = 2;

        string stringV1 = "this is a test";
        string stringV2 = "this is a test";
        string stringV3 = "different string";


        [Fact]
        public void OneIsNotTwo()
        {

            Assert.False(isComparer.Compare<Comparable>(intV1, _comparer, intV2));
            Assert.False(isComparer.Compare<Comparable>(floatV1, _comparer, floatV2));
            Assert.False(isComparer.Compare<Comparable>(doubleV1, _comparer, doubleV2));
            Assert.False(isComparer.Compare<Comparable>(longV1, _comparer, longV2));
            Assert.False(isComparer.Compare<Comparable>(stringV1, _comparer, stringV3));
        }
        [Fact]
        public void OneIsOne()
        {
            Assert.True(isComparer.Compare<Comparable>(intV1, _comparer, intV1));
            Assert.True(isComparer.Compare<Comparable>(floatV1, _comparer, floatV1));
            Assert.True(isComparer.Compare<Comparable>(doubleV1, _comparer, doubleV1));
            Assert.True(isComparer.Compare<Comparable>(longV1, _comparer, longV1));
        }
        [Fact]
        public void stringCompares()
        {
            Assert.True(isComparer.Compare<Comparable>(stringV1, _comparer, stringV2));
            Assert.False(isComparer.Compare<Comparable>(stringV1, _comparer, stringV3));
        }
    }
}
