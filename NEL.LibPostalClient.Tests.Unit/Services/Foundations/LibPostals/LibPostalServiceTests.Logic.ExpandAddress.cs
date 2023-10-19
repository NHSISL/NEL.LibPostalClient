﻿// ---------------------------------------------------------------
// Copyright (c) North East London ICB. All rights reserved.
// ---------------------------------------------------------------

using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using Xunit;

namespace NEL.LibPostalClient.Tests.Unit.Services.Foundations.LibPostals
{
    public partial class LibPostalServiceTests
    {
        [Fact]
        public void ShouldExpandAddress()
        {
            // given
            string randomAddress = GetRandomString();
            string[] inputAddress = new string[] { randomAddress };
            string[] storageAddress = inputAddress;
            string expectedAddress = storageAddress[0].DeepClone();

            this.libPostalServiceBrokerMock.Setup(broker =>
               broker.ExpandAddress(randomAddress))
                   .Returns(inputAddress);

            // when
            ValueTask<string[]> actualAddress = this.libPostalService.ExpandAddress(randomAddress);

            // then
            actualAddress.Should().BeEquivalentTo(expectedAddress);

            this.libPostalServiceBrokerMock.Verify(broker =>
                broker.ExpandAddress(randomAddress),
                    Times.Once);

            this.libPostalServiceBrokerMock.VerifyNoOtherCalls();
        }
    }
}