using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using FluentAssertions;

public class OrderControllerTest
{
    private readonly Mock<IOrderService> _mockOrderService;
    private readonly OrderController _orderController;

    public OrderControllerTest()
    {
        _mockOrderService = new Mock<IOrderService>();
        _orderController = new OrderController(_mockOrderService.Object);
    }

    [Fact]
    public async Task GetOrders_WhenCalled_ReturnsOkResultWithOrders()
    {
        // Arrange
        var mockOrders = new List<Order>
        {
            new Order
            {
                OrderId = 1,
                CustomerId = 101,
                OrderDetails = "Order 1 details",
                OrderDate = new DateTime(2025, 3, 20),
                OrderTotalAmount = 100.50m,
                Customer = null // Assuming no customer object is included in this test
            },
            new Order
            {
                OrderId = 2,
                CustomerId = 102,
                OrderDetails = "Order 2 details",
                OrderDate = new DateTime(2025, 3, 21),
                OrderTotalAmount = 200.75m,
                Customer = null // Assuming no customer object is included in this test
            }
        };
        _mockOrderService.Setup(service => service.GetAllOrdersAsync())
            .ReturnsAsync(mockOrders);

        // Act
        var result = await _orderController.GetOrders();

        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().BeEquivalentTo(mockOrders);
    }

    [Fact]
    public async Task GetOrders_WhenNoOrdersExist_ReturnsOkResultWithEmptyList()
    {
        // Arrange
        var mockOrders = new List<Order>();
        _mockOrderService.Setup(service => service.GetAllOrdersAsync())
            .ReturnsAsync(mockOrders);

        // Act
        var result = await _orderController.GetOrders();

        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().BeEquivalentTo(mockOrders);
    }

    [Fact]
    public async Task GetOrder_ById_ReturnsOkResultWithOrder()
    {
        // Arrange
        var mockOrder = new Order
        {
            OrderId = 1,
            CustomerId = 101,
            OrderDetails = "Order details",
            OrderDate = new DateTime(2025, 3, 20),
            OrderTotalAmount = 100.50m,
            Customer = null
        };
        _mockOrderService.Setup(service => service.GetOrderByIdAsync(1))
            .ReturnsAsync(mockOrder);

        // Act
        var result = await _orderController.GetOrder(1);

        // Assert
        var okResult = result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().BeEquivalentTo(mockOrder);
    }
}