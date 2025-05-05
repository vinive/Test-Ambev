using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class SaleTestData
{
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(s => s.Id, f => Guid.NewGuid())
        .RuleFor(s => s.SaleNumber, f => f.Random.Number())
        .RuleFor(s => s.Date, f => f.Date.Between(new DateTime(2000, 1, 1), new DateTime(2025, 1, 1)))
        .RuleFor(s => s.Customer, f => f.Internet.UserName())
        .RuleFor(s => s.Branch, f => f.Random.String())
        .RuleFor(s => s.IsCancelled, f => f.Random.Bool());

    public static Sale GenerateSale()
    {
        return SaleFaker.Generate();
    }

    public static Sale GenerateActiveSale()
    {
        var sale = SaleFaker.Generate();
        sale.IsCancelled = false;

        return sale;
    }

}
