using FluentAssertions;
using TaskManager.DeveloperEvaluation.Domain.Enums;
using TaskManager.DeveloperEvaluation.Domain.Specifications;
using TaskManager.DeveloperEvaluation.Unit.Domain.Specifications.TestData;
using Xunit;

namespace TaskManager.DeveloperEvaluation.Unit.Domain.Specifications
{
    public class ActiveUserSpecificationTests
    {
        [Theory]
        [InlineData(UserStatus.Active, true)]
        [InlineData(UserStatus.Inactive, false)]        
        public void IsSatisfiedBy_ShouldValidateUserStatus(UserStatus status, bool expectedResult)
        {
            // Arrange
            var user = ActiveUserSpecificationTestData.GenerateUser(status);
            var specification = new ActiveUserSpecification();

            // Act
            var result = specification.IsSatisfiedBy(user);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
