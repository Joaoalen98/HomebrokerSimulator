import { Component, OnInit } from '@angular/core';

import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { ApiService } from '../../services/api.service';
import { Wallet } from '../../models/wallets/Wallet';

@Component({
  selector: 'app-wallets',
  imports: [
    TableModule,
    ButtonModule
  ],
  templateUrl: './wallets.component.html',
  styleUrl: './wallets.component.scss'
})
export class WalletsComponent implements OnInit {

  wallets: Wallet[] = [];

  constructor(private api: ApiService) { }

  ngOnInit(): void {
    this.api.getWallets()
      .subscribe({
        next: (res) => {
          this.wallets = res;
        }
      });
  }

}
