using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QCConfiguration.Domain.Events;

namespace QCConfiguration.Domain.Tests
{
    [TestClass]
    public class QualityControlTests
    {
        [TestMethod]
        public void WhenQualityControlIsCreatedAQualityControlCreatedEventIsGenerated()
        {
            var qualityControl = new QualityControl();
            qualityControl.Create(16272, 3.62, 0.16);

            qualityControl.Changes.Count.Should().Be(1);
            qualityControl.Changes.First().Should().BeOfType(typeof (QualityControlCreated));
        }

        [TestMethod]
        public void TheQualityControlCreatedEventContainsTheTestCodeTheRangeSD1AndTheTargetValue()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void WhenTheQualityControlIsCreatedItInitializeTheTestCodeTheRangeSD1AndTheTargetValue()
        {
            Assert.Inconclusive();
        }
    }
}
