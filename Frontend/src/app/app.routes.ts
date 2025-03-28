import { Routes } from '@angular/router';
import { WalletsComponent } from './pages/wallets/wallets.component';
import { AssetsComponent } from './pages/assets/assets.component';
import { MyWalletComponent } from './pages/my-wallet/my-wallet.component';
import { OrdersComponent } from './pages/orders/orders.component';
import { AssetNegotiationComponent } from './pages/asset-negotiation/asset-negotiation.component';

export const routes: Routes = [
    {
        path: '',
        component: WalletsComponent
    },
    {
        path: 'assets',
        component: AssetsComponent
    },
    {
        path: 'my-wallet',
        component: MyWalletComponent
    },
    {
        path: 'orders',
        component: OrdersComponent
    },
    {
        path: 'assets/:id',
        component: AssetNegotiationComponent
    },
];
