using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.Tests.ResultTests.Extensions
{
    public class MapErrorTests_Task_Left : TestBase
    {
        private const string ContextMessage = "Context-specific error";

        [Fact]
        public async Task MapError_Task_Left_returns_success()
        {
            Task<Result> result = Result.Success().AsTask();
            var invocations = 0;

            Result actual = await result.MapError(error =>
            {
                invocations++;
                return $"{error} {error}";
            });

            actual.IsSuccess.Should().BeTrue();
            invocations.Should().Be(0);
        }

        [Fact]
        public async Task MapError_Task_Left_with_context_returns_new_failure()
        {
            Task<Result> result = Result.Failure(ErrorMessage).AsTask();
            var invocations = 0;

            Result actual = await result.MapError(
                (error, context) =>
                {
                    invocations++;
                    return $"{error} {context}";
                },
                ContextMessage
            );

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be($"{ErrorMessage} {ContextMessage}");
            invocations.Should().Be(1);
        }

        [Fact]
        public async Task MapError_Task_Left_returns_UnitResult_success()
        {
            Task<Result> result = Result.Success().AsTask();
            var invocations = 0;

            UnitResult<E> actual = await result.MapError(error =>
            {
                invocations++;
                return E.Value;
            });

            actual.IsSuccess.Should().BeTrue();
            invocations.Should().Be(0);
        }

        [Fact]
        public async Task MapError_Task_Left_with_context_returns_new_UnitResult_failure()
        {
            Task<Result> result = Result.Failure(ErrorMessage).AsTask();
            var invocations = 0;

            UnitResult<E> actual = await result.MapError(
                (error, context) =>
                {
                    invocations++;
                    return E.Value;
                },
                ContextMessage
            );

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be(E.Value);
            invocations.Should().Be(1);
        }

        [Fact]
        public async Task MapError_Task_Left_T_with_context_returns_new_failure()
        {
            Task<Result<T>> result = Result.Failure<T>(ErrorMessage).AsTask();
            var invocations = 0;

            Result<T> actual = await result.MapError(
                (error, context) =>
                {
                    invocations++;
                    return $"{error} {context}";
                },
                ContextMessage
            );

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be($"{ErrorMessage} {ContextMessage}");
            invocations.Should().Be(1);
        }

        [Fact]
        public async Task MapError_Task_Left_UnitResult_with_context_returns_new_failure()
        {
            Task<UnitResult<E>> result = UnitResult.Failure(E.Value).AsTask();
            var invocations = 0;

            Result actual = await result.MapError(
                (error, context) =>
                {
                    invocations++;
                    return $"{context} Error";
                },
                ContextMessage
            );

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be($"{ContextMessage} Error");
            invocations.Should().Be(1);
        }

        [Fact]
        public async Task MapError_Task_Left_E_UnitResult_with_context_returns_new_failure()
        {
            Task<UnitResult<E>> result = UnitResult.Failure(E.Value).AsTask();
            var invocations = 0;

            UnitResult<E2> actual = await result.MapError(
                (error, context) =>
                {
                    invocations++;
                    return E2.Value;
                },
                ContextMessage
            );

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be(E2.Value);
            invocations.Should().Be(1);
        }

        [Fact]
        public async Task MapError_Task_Left_T_E_with_context_returns_new_failure()
        {
            Task<Result<T>> result = Result.Failure<T>(ErrorMessage).AsTask();
            var invocations = 0;

            Result<T, E> actual = await result.MapError(
                (error, context) =>
                {
                    invocations++;
                    return E.Value;
                },
                ContextMessage
            );

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be(E.Value);
            invocations.Should().Be(1);
        }

        [Fact]
        public async Task MapError_Task_Left_T_E_E2_with_context_returns_new_failure()
        {
            Task<Result<T, E>> result = Result.Failure<T, E>(E.Value).AsTask();
            var invocations = 0;

            Result<T, E2> actual = await result.MapError(
                (error, context) =>
                {
                    invocations++;
                    return E2.Value;
                },
                ContextMessage
            );

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be(E2.Value);
            invocations.Should().Be(1);
        }
    }
}
