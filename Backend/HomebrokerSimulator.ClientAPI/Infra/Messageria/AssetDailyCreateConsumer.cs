using System;
using HomebrokerSimulator.ClientAPI.Features.Assets;
using HomebrokerSimulator.ClientAPI.Infra.Messageria.DTOs;
using MassTransit;

namespace HomebrokerSimulator.ClientAPI.Infra.Messageria;

public class AssetDailyCreateConsumer(IAssetService assetService) : IConsumer<AssetQueueDailyDTO>
{
    public async Task Consume(ConsumeContext<AssetQueueDailyDTO> context)
    {
        var asset = await assetService.GetAssetBySymbol(context.Message.AssetSymbol);

        asset ??= await assetService.CreateAsset(new Features.Assets.DTOs.CreateAssetDTO(
                context.Message.AssetSymbol,
                context.Message.AssetSymbol,
                context.Message.Price
            ));

        await assetService.CreateAssetDaily(new Features.Assets.DTOs.CreateAssetDailyDTO(
            asset.Id!,
            context.Message.Date,
            context.Message.Price
        ));

        await assetService.UpdateAssetPrice(asset.Id!, context.Message.Price);
    }
}
