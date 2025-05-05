using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

/// <summary>
/// Contains unit tests for the Sale entity class.
/// Tests cover status changes and validation scenarios.
/// </summary>
public class SaleTests
{
    /// <summary>
    /// Tests that when a suspended sale is activated, their status changes to Active.
    /// </summary>
    [Fact(DisplayName = "Sale isCancelled should change to true when cancelSale")]
    public void Given_ActiveSale_When_Cancelled_Then_isCancelledShoudBeTrue()
    {
        // Arrange
        var sale = SaleTestData.GenerateActiveSale();        

        // Act
        sale.CancelSale();

        // Assert
        Assert.True(sale.IsCancelled);
    }

    [Fact(DisplayName = "Sale addItem should change item count to one")]
    public void GivenSaleWithNoItems_WhenAddItem_ThenItemIsAddedToSale()
    {
        // Arrange
        var sale = SaleTestData.GenerateActiveSale();
        var item = new SaleItem()
        {
            Quantity = 1,
            UnitPrice = 5,
        };

        // Act
        sale.AddItem(item);

        // Assert
        Assert.Single(sale.Items);
    }

}
