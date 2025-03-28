import { WalletAsset } from "./WalletAsset";

export type Wallet = {
    id: string;
    walletAssets: WalletAsset[] | null;
}

