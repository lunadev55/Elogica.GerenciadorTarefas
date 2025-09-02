using TaskManager.DeveloperEvaluation.Domain.Entities;
using TaskManager.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace TaskManager.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the User entity class.
/// Tests cover status changes and validation scenarios.
/// </summary>
public class UserTests
{  
    /// <summary>
    ///// Tests that validation passes when all user properties are valid.
    ///// </summary>
    //[Fact(DisplayName = "Validation should pass for valid user data")]
    //public void Given_ValidUserData_When_Validated_Then_ShouldReturnValid()
    //{
    //    // Arrange
    //    var user = UserTestData.GenerateValidUser();

    //    // Act
    //    var result = user.Validate();

    //    // Assert
    //    Assert.True(result.IsValid);
    //    Assert.Empty(result.Errors);
    //}

    /// <summary>
    /// Tests that validation fails when user properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid user data")]
    public void Given_InvalidUserData_When_Validated_Then_ShouldReturnInvalid()
    {
        // Arrange
        var user = new User
        {
            Username = "", // Invalid: empty
            Password = UserTestData.GenerateInvalidPassword(), // Invalid: doesn't meet password requirements
            Email = UserTestData.GenerateInvalidEmail(), // Invalid: not a valid email
            Phone = UserTestData.GenerateInvalidPhone(), // Invalid: doesn't match pattern            
        };

        // Act
        var result = user.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}
