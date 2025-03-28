using HomebrokerSimulator.ClientAPI.Common.Exceptions;
using HomebrokerSimulator.ClientAPI.Features.Orders.DTOs;
using HomebrokerSimulator.ClientAPI.Features.Orders.Entities;
using HomebrokerSimulator.ClientAPI.Features.Orders.Enums;
using HomebrokerSimulator.ClientAPI.Features.Wallets.Entities;
using HomebrokerSimulator.ClientAPI.Infra.Mongo;
using MassTransit;
using MongoDB.Driver;

namespace HomebrokerSimulator.ClientAPI.Features.Orders;

public class OrderService(
    MongoDBService mongoDBService, 
    IServiceProvider serviceProvider) : IOrderService
{
    private async Task SendOrderToQueue(CreateQueueOrder createQueueOrder)
    {
        var scope = serviceProvider.CreateScope();
        var sendEndpointProvider = scope.ServiceProvider.GetRequiredService<ISendEndpointProvider>();

        var publishEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:order-create"));
        await publishEndpoint.Send(createQueueOrder);
    }

    public async Task<Order> CreateOrder(CreateOrderDTO createOrderDTO)
    {
        var order = new Order
        {
            AssetId = createOrderDTO.AssetId,
            WalletId = createOrderDTO.WalletId,
            Price = createOrderDTO.Price,
            Shares = createOrderDTO.Shares,
            Type = createOrderDTO.Type,
            Partial = createOrderDTO.Shares,
        };

        await mongoDBService.Orders.InsertOneAsync(order);

        var assetSymbol = await mongoDBService.Assets
            .Find(a => a.Id == createOrderDTO.AssetId)
            .Project(a => a.Symbol)
            .FirstOrDefaultAsync();

        await SendOrderToQueue(new CreateQueueOrder(
            assetSymbol,
            createOrderDTO.Price,
            createOrderDTO.Shares,
            createOrderDTO.Type.ToString(),
            order.Id!));

        return order;
    }

    public async Task<IEnumerable<Order>> GetOrdersByWalletId(string walletId)
    {
        var orders = await mongoDBService.Orders
            .Find(o => o.WalletId == walletId)
            .ToListAsync();

        return orders;
    }

    public async Task<Order> GetOrderById(string orderId)
    {
        var order = await mongoDBService.Orders
            .Aggregate()
            .Match(o => o.Id == orderId)
            .Lookup<OrderTrade, Order>(
                "OrderTrades",
                "_id",
                "OrderId",
                "Trades"
            )
            .FirstOrDefaultAsync();

        return order;
    }

    public async Task<Order> UpdateOrderStatus(string orderId, EOrderStatus orderStatus)
    {
        var upadate = Builders<Order>.Update
            .Set(o => o.Status, orderStatus);

        var updated = await mongoDBService.Orders.FindOneAndUpdateAsync(o => o.Id == orderId, upadate);
        return updated;
    }

    public async Task<OrderTrade> CreateOrderTrade(CreateOrderTradeDTO createOrderTradeDTO)
    {
        var session = mongoDBService.Client.StartSession();

        try
        {
            session.StartTransaction();

            var order = await mongoDBService.Orders
                .Find(o => o.Id == createOrderTradeDTO.OrderId)
                .FirstOrDefaultAsync();

            var walletAsset = await mongoDBService.WalletAssets
                .Find(wa => wa.WalletId == order.WalletId && wa.AssetId == order.AssetId)
                .FirstOrDefaultAsync();

            if (order.Status != Enums.EOrderStatus.OPENED)
            {
                throw new BadRequestException("Esta ordem nao esta mais aberta");
            }

            if (order.Partial < createOrderTradeDTO.Shares)
            {
                throw new BadRequestException("Quantidade no trade maior que a quantidade da ordem");
            }

            var orderTrade = new OrderTrade
            {
                OrderId = createOrderTradeDTO.OrderId,
                Price = createOrderTradeDTO.Price,
                Shares = createOrderTradeDTO.Shares,
            };

            await mongoDBService.OrderTrades.InsertOneAsync(orderTrade);

            order.Partial -= createOrderTradeDTO.Shares;

            if (order.Partial == 0)
            {
                order.Status = Enums.EOrderStatus.CLOSED;
            }

            await mongoDBService.Orders.ReplaceOneAsync(o => o.Id == order.Id, order);

            if (walletAsset == null)
            {
                walletAsset = new WalletAsset
                {
                    WalletId = order.WalletId,
                    AssetId = order.AssetId,
                    Shares = createOrderTradeDTO.Shares,
                };

                await mongoDBService.WalletAssets.InsertOneAsync(walletAsset);
            }
            else
            {
                walletAsset.Shares += order.Type == Enums.EOrderType.BUY ? createOrderTradeDTO.Shares : createOrderTradeDTO.Shares * -1;
                await mongoDBService.WalletAssets.ReplaceOneAsync(wa => wa.Id == walletAsset.Id, walletAsset);
            }

            await session.CommitTransactionAsync();

            return orderTrade;
        }
        catch (Exception)
        {
            await session.AbortTransactionAsync();
            throw;
        }
    }
}

