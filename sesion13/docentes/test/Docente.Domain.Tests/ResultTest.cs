using Docentes.Domain.Abstractions;

namespace Docente.Domain.Tests;

public class ResultTest
{
    [Fact]
    public void Success_Should_Return_SuccessResult()
    {
        var result = Result.Success();
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(result.Error, Error.None);
    }

    [Fact]
    public void Failure_Should_Return_FailureResult()
    {
        var error = new Error("Error", "Error message");
        var result = Result.Failure(error);
        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Equal(result.Error, error);
    }

    [Fact]
    public void Success_Should_Return_SuccessResultWithValue()
    {
        var value = 10;
        var result = Result.Success(value);
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(result.Error, Error.None);
        Assert.Equal(result.Value, value);
    }




}