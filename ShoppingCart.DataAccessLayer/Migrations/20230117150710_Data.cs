using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCart.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class Data : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"SET IDENTITY_INSERT [dbo].[Product] ON 
GO
INSERT [dbo].[Product] ([Id], [Name], [Quantity], [ReservedQuantity], [Price]) VALUES (1, N'Paulaner', 0, 0, 168.39)
GO
INSERT [dbo].[Product] ([Id], [Name], [Quantity], [ReservedQuantity], [Price]) VALUES (2, N'Lasko', 5, 0, 100.44)
GO
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[ShoppingCart] ON 
GO
INSERT [dbo].[ShoppingCart] ([Id], [CustomerId], [IsCompleted]) VALUES (1, 1, 1)
GO
INSERT [dbo].[ShoppingCart] ([Id], [CustomerId], [IsCompleted]) VALUES (2, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[ShoppingCart] OFF
GO
SET IDENTITY_INSERT [dbo].[ShoppingItem] ON 
GO
INSERT [dbo].[ShoppingItem] ([Id], [ShoppingCartId], [ProductId], [Quantity], [UnitPrice], [TotalPrice]) VALUES (1, 1, 1, 30, 168.39, 5051.7)
GO
INSERT [dbo].[ShoppingItem] ([Id], [ShoppingCartId], [ProductId], [Quantity], [UnitPrice], [TotalPrice]) VALUES (2, 2, 1, 10, 168.39, 1683.8999999999999)
GO
INSERT [dbo].[ShoppingItem] ([Id], [ShoppingCartId], [ProductId], [Quantity], [UnitPrice], [TotalPrice]) VALUES (7, 2, 2, 10, 100.44, 1004.4)
GO
SET IDENTITY_INSERT [dbo].[ShoppingItem] OFF
GO
SET IDENTITY_INSERT [dbo].[Order] ON 
GO
INSERT [dbo].[Order] ([Id], [ShoppingCartId], [Amount], [Discount], [Sum], [CustomerAddress], [CustomerPhoneNumber]) VALUES (1, 1, 5051.7, 1515.51, 3536.1899999999996, N'Moravske divizije 11/3', N'(+381)649945515')
GO
INSERT [dbo].[Order] ([Id], [ShoppingCartId], [Amount], [Discount], [Sum], [CustomerAddress], [CustomerPhoneNumber]) VALUES (2, 2, 2688.2999999999997, 0, 2688.3, N'Moravske divizije 11/3 Borca', N'(+381)649945515')
GO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
