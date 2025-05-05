using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleTests
{

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
