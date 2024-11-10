using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.Tests.ResultTests.Extensions
{
    public class MapErrorTests_ValueTask_Right : TestBase
    {
        private const string ContextMessage = "Context-specific error";

        [Fact]
        public async Task MapError_ValueTask_Right_returns_success()
        {
            Result result = Result.Success();
            var invocations = 0;

            Result actual = await result.MapError(error =>
            {
                invocations++;
                return Task.FromResult($"{error} {error}");
            });

            actual.IsSuccess.Should().BeTrue();
            invocations.Should().Be(0);
        }

        [Fact]
        public async Task MapError_ValueTask_Right_with_context_returns_new_failure()
        {
            Result result = Result.Failure(ErrorMessage);
            var invocations = 0;

            Result actual = await result.MapError(
                (error, context) =>
                {
                    invocations++;
                    return Task.FromResult($"{error} {context}");
                },
                ContextMessage
            );

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be($"{ErrorMessage} {ContextMessage}");
            invocations.Should().Be(1);
        }

        [Fact]
        public async Task MapError_ValueTask_Right_T_with_context_returns_new_failure()
        {
            Result<T> result = Result.Failure<T>(ErrorMessage);
            var invocations = 0;

            Result<T> actual = await result.MapError(
                (error, context) =>
                {
                    invocations++;
                    return Task.FromResult($"{error} {context}");
                },
                ContextMessage
            );

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be($"{ErrorMessage} {ContextMessage}");
            invocations.Should().Be(1);
        }

        [Fact]
        public async Task MapError_ValueTask_Right_UnitResult_with_context_returns_new_failure()
        {
            UnitResult<E> result = UnitResult.Failure(E.Value);
            var invocations = 0;

            Result actual = await result.MapError(
                (error, context) =>
                {
                    invocations++;
                    return Task.FromResult($"{context} Error");
                },
                ContextMessage
            );

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be($"{ContextMessage} Error");
            invocations.Should().Be(1);
        }

        [Fact]
        public async Task MapError_ValueTask_Right_E_UnitResult_with_context_returns_new_failure()
        {
            UnitResult<E> result = UnitResult.Failure(E.Value);
            var invocations = 0;

            UnitResult<E2> actual = await result.MapError(
                (error, context) =>
                {
                    invocations++;
                    return Task.FromResult(E2.Value);
                },
                ContextMessage
            );

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be(E2.Value);
            invocations.Should().Be(1);
        }

        [Fact]
        public async Task MapError_ValueTask_Right_T_E_with_context_returns_new_failure()
        {
            Result<T> result = Result.Failure<T>(ErrorMessage);
            var invocations = 0;

            Result<T, E> actual = await result.MapError(
                (error, context) =>
                {
                    invocations++;
                    return Task.FromResult(E.Value);
                },
                ContextMessage
            );

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be(E.Value);
            invocations.Should().Be(1);
        }

        [Fact]
        public async Task MapError_ValueTask_Right_T_E_E2_with_context_returns_new_failure()
        {
            Result<T, E> result = Result.Failure<T, E>(E.Value);
            var invocations = 0;

            Result<T, E2> actual = await result.MapError(
                (error, context) =>
                {
                    invocations++;
                    return Task.FromResult(E2.Value);
                },
                ContextMessage
            );

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be(E2.Value);
            invocations.Should().Be(1);
        }
    }
}
