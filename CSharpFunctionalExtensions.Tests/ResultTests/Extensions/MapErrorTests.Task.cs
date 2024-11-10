using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace CSharpFunctionalExtensions.Tests.ResultTests.Extensions
{
    public class MapError_Task_Tests : TestBase
    {
        private const string ContextMessage = "Context-specific error";

        [Fact]
        public async Task MapError_Task_returns_success()
        {
            Task<Result> result = Result.Success().AsTask();
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
        public async Task MapError_Task_returns_new_failure()
        {
            Task<Result> result = Result.Failure(ErrorMessage).AsTask();
            var invocations = 0;

            Result actual = await result.MapError(error =>
            {
                invocations++;
                return Task.FromResult($"{error} {error}");
            });

            actual.IsSuccess.Should().BeFalse();
            actual.Error.Should().Be($"{ErrorMessage} {ErrorMessage}");
            invocations.Should().Be(1);
        }

        [Fact]
        public async Task MapError_Task_with_context_returns_new_failure()
        {
            Task<Result> result = Result.Failure(ErrorMessage).AsTask();
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
        public async Task MapError_Task_returns_UnitResult_success()
        {
            Task<Result> result = Result.Success().AsTask();
            var invocations = 0;

            UnitResult<E> actual = await result.MapError(error =>
            {
                invocations++;
                return Task.FromResult(E.Value);
            });

            actual.IsSuccess.Should().BeTrue();
            invocations.Should().Be(0);
        }

        [Fact]
        public async Task MapError_Task_with_context_returns_new_UnitResult_failure()
        {
            Task<Result> result = Result.Failure(ErrorMessage).AsTask();
            var invocations = 0;

            UnitResult<E> actual = await result.MapError(
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
        public async Task MapError_Task_T_returns_success()
        {
            Task<Result<T>> result = Result.Success(T.Value).AsTask();
            var invocations = 0;

            Result<T> actual = await result.MapError(error =>
            {
                invocations++;
                return Task.FromResult($"{error} {error}");
            });

            actual.IsSuccess.Should().BeTrue();
            actual.Value.Should().Be(T.Value);
            invocations.Should().Be(0);
        }

        [Fact]
        public async Task MapError_Task_T_with_context_returns_new_failure()
        {
            Task<Result<T>> result = Result.Failure<T>(ErrorMessage).AsTask();
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
        public async Task MapError_Task_UnitResult_with_context_returns_new_failure()
        {
            Task<UnitResult<E>> result = UnitResult.Failure(E.Value).AsTask();
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
        public async Task MapError_Task_E_UnitResult_with_context_returns_new_failure()
        {
            Task<UnitResult<E>> result = UnitResult.Failure(E.Value).AsTask();
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
        public async Task MapError_Task_T_E_with_context_returns_new_failure()
        {
            Task<Result<T>> result = Result.Failure<T>(ErrorMessage).AsTask();
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
        public async Task MapError_Task_T_E_E2_with_context_returns_new_failure()
        {
            Task<Result<T, E>> result = Result.Failure<T, E>(E.Value).AsTask();
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
