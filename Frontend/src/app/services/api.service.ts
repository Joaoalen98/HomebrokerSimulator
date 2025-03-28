import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { HttpClient } from '@angular/common/http';
import { Wallet } from '../models/wallets/Wallet';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private url = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getWallets() {
    return this.http.get<Wallet[]>(`${this.url}/wallets`);
  }
}
