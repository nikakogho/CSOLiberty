import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs';

@Component({
  selector: 'fetch-orders',
  templateUrl: './fetch-orders.component.html'
})
export class FetchOrdersComponent {
  public orders: Order[];
  public loading: boolean
  private loader: () => Subscription

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.loading = false
    this.loader = () => http.get<Order[]>(baseUrl + 'api/orders').subscribe(result => {
      this.orders = result;
      this.loading = false
    }, error => console.error(error));
  }

  load() {
    this.loading = true
    this.loader()
  }
}

interface Order {
  id: number,
  parts: number,
  date: string,
  money: number
}
