using CleanArch.Domain.Entities;
using FluentAssertions;

namespace CleanArch.Domain.Tests;

public class ProductUnitTest1
{
    [Fact]
    public void CreateProduct_WithValidParameters_ResultObjectValidState()
    {
        Action action = () =>
        {
            _ = new Product(1, "Product Name", "Product Description", 9.99m, 99, "product image");
        };
        action.Should().NotThrow<CleanArch.Domain.Validation.DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
    {
        Action action = () =>
        {
            _ = new Product(-1, "Product Name", "Product Description", 9.99m, 99, "product image");
        };

        action
            .Should()
            .Throw<CleanArch.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid Id value");
    }

    [Fact]
    public void CreateProduct_ShortNameValue_DomainExceptionShortName()
    {
        Action action = () =>
        {
            _ = new Product(1, "P", "Product Description", 9.99m, 99, "product image");
        };

        action
            .Should()
            .Throw<CleanArch.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid name. Name is too short");
    }

    [Fact]
    public void CreateProduct_LongImageName_DomainExceptionLongImage()
    {
        Action action = () =>
        {
            _ = new Product(
                1,
                "Product Name",
                "Product Description",
                9.99m,
                99,
                "productimageproductimageproductimageproductimageproductimageproductimageproductimageproductimageproductimageproductimageproductimageproductimageproductimageproductimageproductimageproductimageproductimageproductimageproductimageproductimageproductimageproductimageproductimageproductimageproductimage"
            );
        };

        action
            .Should()
            .Throw<CleanArch.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid image name, too long, maximum 250 characters");
    }

    [Fact]
    public void CreateProduct_WithNullImageName_NoDomainException()
    {
        Action action = () =>
        {
            _ = new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
        };
        action.Should().NotThrow<CleanArch.Domain.Validation.DomainExceptionValidation>();
    }

    [Fact]
    public void CreateProduct_WithNullImageName_NoNullReferenceException()
    {
        Action action = () =>
        {
            _ = new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
        };
        action.Should().NotThrow<NullReferenceException>();
    }

    [Fact]
    public void CreateProduct_WithEmptyImageName_NoDomainException()
    {
        Action action = () =>
        {
            _ = new Product(1, "Product Name", "Product Description", 9.99m, 99, "");
        };
        action.Should().NotThrow<CleanArch.Domain.Validation.DomainExceptionValidation>();
    }

    [Theory]
    [InlineData(-9.99)]
    public void CreateProduct_InvalidPriceValue_DomainExceptionInvalidPrice(int value)
    {
        Action action = () =>
        {
            _ = new Product(1, "Product Name", "Product Description", value, 99, "product image");
        };

        action
            .Should()
            .Throw<CleanArch.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid price value");
    }

    [Theory]
    [InlineData(-5)]
    public void CreateProduct_InvalidStockValue_DomainExceptionInvalidStock(int value)
    {
        Action action = () =>
        {
            _ = new Product(
                1,
                "Product Name",
                "Product Description",
                9.99m,
                value,
                "product image"
            );
        };

        action
            .Should()
            .Throw<CleanArch.Domain.Validation.DomainExceptionValidation>()
            .WithMessage("Invalid stock value");
    }
}
