﻿using CloudFabric.BuisnessRules.Enums;
using CloudFabric.BuisnessRules.Models;
using CloudFabric.BuisnessRules.Structs;
using System;
using Xunit;

namespace CloudFabric.BuisnessRules.Tests
{
    public class IsGreaterThanOrEqualTo
    {
        private static Comparer _comparer = new Comparer(ComparerTypeEnum.NumberComparer, (int)NumberComparerTypeEnum.GreaterThanOrEqual);
        private static NumberComparerTypeEnumFactory numberComparer = new NumberComparerTypeEnumFactory();

        int intV1 = 1;
        int intV2 = 2;

        float floatV1 = 1;
        float floatV2 = 2;

        double doubleV1 = 1;
        double doubleV2 = 2;

        long longV1 = 1;
        long longV2 = 2;


        [Fact]
        public void OneIsNotGreaterThanOrEqualtoTwo()
        {

            Assert.False(numberComparer.Compare<Comparable>(intV1, _comparer, intV2));
            Assert.False(numberComparer.Compare<Comparable>(floatV1, _comparer, floatV2));
            Assert.False(numberComparer.Compare<Comparable>(doubleV1, _comparer, doubleV2));
            Assert.False(numberComparer.Compare<Comparable>(longV1, _comparer, longV2));

        }
        [Fact]
        public void TwoIsGreaterThanOrEqualToOne()
        {
            Assert.True(numberComparer.Compare<Comparable>(intV2, _comparer, intV1));
            Assert.True(numberComparer.Compare<Comparable>(floatV2, _comparer, floatV1));
            Assert.True(numberComparer.Compare<Comparable>(doubleV2, _comparer, doubleV1));
            Assert.True(numberComparer.Compare<Comparable>(longV2, _comparer, longV1));
        }
        [Fact]
        public void TwoIsGreaterThanOrEqualToTwo()
        {
            Assert.True(numberComparer.Compare<Comparable>(intV2, _comparer, intV2));
            Assert.True(numberComparer.Compare<Comparable>(floatV2, _comparer, floatV2));
            Assert.True(numberComparer.Compare<Comparable>(doubleV2, _comparer, doubleV2));
            Assert.True(numberComparer.Compare<Comparable>(longV2, _comparer, longV2));
        }
    }
}
