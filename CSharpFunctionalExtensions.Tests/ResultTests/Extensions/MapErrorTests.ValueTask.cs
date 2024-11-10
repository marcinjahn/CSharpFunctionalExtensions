using System.Threading.Tasks;
using CSharpFunctionalExtensions.ValueTasks;
using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.Tests.ResultTests.Extensions
{
    public class MapError_ValueTask_Tests : TestBase
    {
        private const string ContextMessage = "Context-specific error";

        [Fact]
        public async Task MapError_ValueTask_returns_success()
        {
            ValueTask<Result> result = Result.Success().AsValueTask();
            var invocations = 0;

            Result actual = await result.MapError(error =>
            {
                invocations++;
                return $"{error} {error}".AsCompletedValueTask();
            });

            actual.IsSuccess.Should().BeTrue();
            invocations.Should().Be(0);
        }

        [Fact]
        public async Task MapError_ValueTask_with_context_returns_new_failure()
        {
            ValueTask<Result> result = Result.Failure(ErrorMessage).AsValueTask();
            var invocations = 0;

            Result actual = await result.MapError(
                (error, context) =>
                {
                    invocations++;
                    return $"{error} {context}".AsCompletedValueTask();
                },
                ContextMessage
            );

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be($"{ErrorMessage} {ContextMessage}");
            invocations.Should().Be(1);
        }

        [Fact]
        public async Task MapError_ValueTask_T_with_context_returns_new_failure()
        {
            ValueTask<Result<T>> result = Result.Failure<T>(ErrorMessage).AsValueTask();
            var invocations = 0;

            Result<T> actual = await result.MapError(
                (error, context) =>
                {
                    invocations++;
                    return $"{error} {context}".AsCompletedValueTask();
                },
                ContextMessage
            );

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be($"{ErrorMessage} {ContextMessage}");
            invocations.Should().Be(1);
        }

        [Fact]
        public async Task MapError_ValueTask_UnitResult_with_context_returns_new_failure()
        {
            ValueTask<UnitResult<E>> result = UnitResult.Failure(E.Value).AsValueTask();
            var invocations = 0;

            Result actual = await result.MapError(
                (error, context) =>
                {
                    invocations++;
                    return $"{context} Error".AsCompletedValueTask();
                },
                ContextMessage
            );

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be($"{ContextMessage} Error");
            invocations.Should().Be(1);
        }

        [Fact]
        public async Task MapError_ValueTask_E_UnitResult_with_context_returns_new_failure()
        {
            ValueTask<UnitResult<E>> result = UnitResult.Failure(E.Value).AsValueTask();
            var invocations = 0;

            UnitResult<E2> actual = await result.MapError(
                (error, context) =>
                {
                    invocations++;
                    return E2.Value.AsCompletedValueTask();
                },
                ContextMessage
            );

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be(E2.Value);
            invocations.Should().Be(1);
        }

        [Fact]
        public async Task MapError_ValueTask_T_E_with_context_returns_new_failure()
        {
            ValueTask<Result<T>> result = Result.Failure<T>(ErrorMessage).AsValueTask();
            var invocations = 0;

            Result<T, E> actual = await result.MapError(
                (error, context) =>
                {
                    invocations++;
                    return E.Value.AsCompletedValueTask();
                },
                ContextMessage
            );

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be(E.Value);
            invocations.Should().Be(1);
        }

        [Fact]
        public async Task MapError_T_E_E2_with_context_ValueTask_returns_new_failure()
        {
            ValueTask<Result<T, E>> result = Result.Failure<T, E>(E.Value).AsValueTask();
            var invocations = 0;

            Result<T, E2> actual = await result.MapError(
                (error, context) =>
                {
                    invocations++;
                    return E2.Value.AsCompletedValueTask();
                },
                ContextMessage
            );

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be(E2.Value);
            invocations.Should().Be(1);
        }
    }
}
