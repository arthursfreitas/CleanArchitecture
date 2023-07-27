using CleanArch.Domain.Entities;
using FluentAssertions;

namespace CleanArch.Domain.Tests;

public class CategoryUnitTest1
{
    [Fact(DisplayName = "Create Category With Valid State")]
    public void CreateCategory_WithValidParameters_ResultObjectValidState()
    {
        Category? category = null;
        Action action = () => category = new Category(1, "Category Name");
        action.Should().NotThrow<CleanArch.Domain.Validation.DomainExceptionValidation>();
    }

    [Fact]
    public void CreateCategory_NegativeIdValue_DomainExceptionInvalidId()
    {
        Category? category = null;
        Action action = () => category = new Category(-1, "Category Name");
        action
            .Should()
            .Throw<CleanArch.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid Id value");
    }

    [Fact]
    public void CreateCategory_ShortNameValue_DomainExceptionShortName()
    {
        Category? category = null;
        Action action = () => category = new Category(1, "Ca");
        action
            .Should()
            .Throw<CleanArch.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name, too short, minimum 3 characters");
    }

    [Fact]
    public void CreateCategory_MissingNameValue_DomainExceptionRequiredName()
    {
        Category? category = null;
        Action action = () => category = new Category(1, "");
        action
            .Should()
            .Throw<CleanArch.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name. Name is required");
    }

    [Fact]
    public void CreateCategory_WithNullNameValue_DomainExceptionInvalidName()
    {
        Category? category = null;
        Action action = () => category = new Category(1, "");
        action.Should().Throw<CleanArch.Domain.Validation.DomainExceptionValidation>();
    }
}
