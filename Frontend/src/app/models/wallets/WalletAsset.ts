import { Asset } from "../assets/Asset";

export type WalletAsset = {
    id: string;
    walletId: string;
    assetId: string;
    shares: number;
    asset: Asset;
}