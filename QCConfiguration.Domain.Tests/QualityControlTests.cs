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
            var qualityControl = new QualityControl();
            qualityControl.Create(11170,7.072,0.161);

            var creationEvent = qualityControl.Changes.First() as QualityControlCreated;

            Assert.IsNotNull(creationEvent);

            creationEvent.TestCode.Should().Be(11170);
            creationEvent.StandardDeviation.Should().Be(0.161);
            creationEvent.TargetValue.Should().Be(7.072);
        }

        [TestMethod]
        public void WhenTheQualityControlIsCreatedItInitializeTheTestCodeTheRangeSD1AndTheTargetValue()
        {
            var qualityControl = new QualityControl();
            qualityControl.Create(11170, 7.072, 0.161);

            qualityControl.TestCode.Should().Be(11170);
            qualityControl.TargetValue.Should().Be(7.072);
            qualityControl.StandardDeviation.Should().Be(0.161);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void WhenANonCreatedQualityControlIsUpdatedAnExceptionIsThrown()
        {
            QualityControl qualityControl = new QualityControl();
            qualityControl.Update(0.707, 1110);
        }


        [TestMethod]
        public void WhenAQualityControlIsCreatedItsStatusIsInstalled()
        {
            QualityControl qualityControl = new QualityControl();
            qualityControl.Create(11150,7.072,0.161);

            qualityControl.Installed.Should().BeTrue();
        }

        [TestMethod]
        public void WhenAnUpdateIsDoneAQualityControlUpdatedEventIsCreated()
        {
            QualityControl qualityControl = new QualityControl();
            qualityControl.Create(11150, 7.072, 0.161);
            qualityControl.Update(0.707, 1627);

            var aggreggateEvent = qualityControl.Changes.Last();
            aggreggateEvent.Should().BeOfType(typeof (QualityControlUpdated));
            aggreggateEvent.As<QualityControlUpdated>().TargetValue.Should().Be(0.707);
            aggreggateEvent.As<QualityControlUpdated>().StandardDeviation.Should().Be(1627);
        }

        [TestMethod]
        public void WhenAnUpdateIsDoneTheStandardDeviationAndTargetValueAreUpdated()
        {
            QualityControl qualityControl = new QualityControl();
            qualityControl.Create(11150, 7.072, 0.161);
            qualityControl.Update(0.707, 1627);

            qualityControl.StandardDeviation.Should().Be(1627);
            qualityControl.TargetValue.Should().Be(0.707);
        }
    }
}
