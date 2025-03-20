// filepath: tests/Services/OrderServiceTest.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Xunit;
using FluentAssertions;
using Microsoft.EntityFrameworkCore.InMemory;


public class OrderServiceTest : IDisposable
{
    private readonly AppDbContext _context;
    private readonly OrderService _orderService;

    public OrderServiceTest()
    {
        // Ensure the correct namespace for InMemoryDatabase is included
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _orderService = new OrderService(_context);

        SeedTestData();
    }

    private void SeedTestData()
    {
        _context.Orders.AddRange(
            new Order
            {
                OrderId = 1,
                CustomerId = 101,
                OrderDetails = "Order 1 details",
                OrderDate = new DateTime(2025, 3, 20),
                OrderTotalAmount = 100.50m
            },
            new Order
            {
                OrderId = 2,
                CustomerId = 102,
                OrderDetails = "Order 2 details",
                OrderDate = new DateTime(2025, 3, 21),
                OrderTotalAmount = 200.75m
            }
        );
        _context.SaveChanges();
    }

    [Fact]
    public async Task GetAllOrdersAsync_WhenCalled_ReturnsAllOrders()
    {
        // Act
        var result = await _orderService.GetAllOrdersAsync();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().Contain(o => o.OrderId == 1 && o.OrderDetails == "Order 1 details");
        result.Should().Contain(o => o.OrderId == 2 && o.OrderDetails == "Order 2 details");
    }

    [Fact]
    public async Task GetOrderByIdAsync_WhenOrderExists_ReturnsOrder()
    {
        // Act
        var result = await _orderService.GetOrderByIdAsync(1);

        // Assert
        result.Should().NotBeNull();
        result!.OrderId.Should().Be(1);
        result.OrderDetails.Should().Be("Order 1 details");
    }

    [Fact]
    public async Task GetOrderByIdAsync_WhenOrderDoesNotExist_ReturnsNull()
    {
        // Act
        var result = await _orderService.GetOrderByIdAsync(999);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task CreateOrderAsync_WhenCalled_AddsOrderToDatabase()
    {
        // Arrange
        var newOrder = new Order
        {
            OrderId = 3,
            CustomerId = 103,
            OrderDetails = "Order 3 details",
            OrderDate = new DateTime(2025, 3, 22),
            OrderTotalAmount = 300.00m
        };

        // Act
        var result = await _orderService.CreateOrderAsync(newOrder);

        // Assert
        result.Should().NotBeNull();
        result.OrderId.Should().Be(3);

        var orders = await _context.Orders.ToListAsync();
        orders.Should().HaveCount(3);
        orders.Should().Contain(o => o.OrderId == 3 && o.OrderDetails == "Order 3 details");
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }
}