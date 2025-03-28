import { Component } from '@angular/core';

import { MenubarModule } from 'primeng/menubar';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-menu',
  imports: [
    MenubarModule
  ],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.scss'
})
export class MenuComponent {

  items: MenuItem[] = [
    {
      label: 'Carteiras',
      routerLink: ['/']
    },
    {
      label: 'Minha carteira',
      routerLink: ['/my-wallet']
    },
    {
      label: 'Ativos',
      routerLink: ['/assets']
    },
    {
      label: 'Ordens',
      routerLink: ['/orders']
    },
  ];
}
