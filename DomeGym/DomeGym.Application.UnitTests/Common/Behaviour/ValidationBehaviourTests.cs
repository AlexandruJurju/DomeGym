using DomeGym.Application.Common.Behaviors;
using DomeGym.Application.Gyms.Commands.CreateGym;
using DomeGym.Domain.Gyms;
using ErrorOr;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using NSubstitute;
using TestsCommon.Gyms;

namespace DomeGym.Application.UnitTests.Common.Behaviour;

public class ValidationBehaviourTests
{
    private readonly RequestHandlerDelegate<ErrorOr<Gym>> _mockNextBehavior;
    private readonly IValidator<CreateGymCommand> _mockValidator;
    private readonly ValidationBehavior<CreateGymCommand, ErrorOr<Gym>> _validationBehavior;

    public ValidationBehaviourTests()
    {
        // Create a next behavior (mock)
        _mockNextBehavior = Substitute.For<RequestHandlerDelegate<ErrorOr<Gym>>>();

        // Create validator (mock)
        _mockValidator = Substitute.For<IValidator<CreateGymCommand>>();

        // Create validation behavior (SUT)
        _validationBehavior = new ValidationBehavior<CreateGymCommand, ErrorOr<Gym>>(_mockValidator);
    }

    [Fact]
    public async Task InvokeBehaviour_WhenValidatorResultIsValid_ShouldInvokeNextBehaviour()
    {
        // Arrange
        var createGymRequest = GymCommandFactory.CreateCreateGymCommand();
        var gym = GymFactory.CreateGym();

        _mockValidator
            .ValidateAsync(createGymRequest, Arg.Any<CancellationToken>())
            .Returns(new ValidationResult());

        _mockNextBehavior.Invoke().Returns(gym);

        // Act
        var result = await _validationBehavior.Handle(createGymRequest, _mockNextBehavior, default);

        // Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().BeEquivalentTo(gym);
    }

    [Fact]
    public async Task InvokeBehaviour_WhenValidatorResultIsNotValid_ShouldReturnListOfErrors()
    {
        // Arrange
        var createGymRequest = GymCommandFactory.CreateCreateGymCommand();
        List<ValidationFailure> validationFailures = [new ValidationFailure("foo", "bad foo")];

        _mockValidator
            .ValidateAsync(createGymRequest, Arg.Any<CancellationToken>())
            .Returns(new ValidationResult(validationFailures));

        // Act
        var result = await _validationBehavior.Handle(createGymRequest, _mockNextBehavior, default);

        // Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Code.Should().Be("foo");
        result.FirstError.Description.Should().Be("bad foo");
    }
}