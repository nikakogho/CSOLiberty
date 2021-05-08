import { Component, Inject, NgModule } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'fetch-orders-in-period',
  templateUrl: './fetch-orders-in-period.component.html'
})
export class FetchOrdersInPeriodComponent {
  public order: Order | null;
  public loading: boolean
  private loader: (url: string) => Subscription
  private baseUrl: string

  public startDate: any = null
  public endDate: any = null

  // TODO date picker
  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.baseUrl = baseUrl
    this.loading = false
    this.loader = (url: string) => http.get<Order[]>(url).subscribe(result => {
      this.order = result.length > 0 ? result[0] : null
      this.loading = false
    }, error => console.error(error));
  }

  /*
  mdyToYdm(date: string) {
    const [m, d, y] = date.split('/')

    return `${y}/${d}/${m}`
  }
  */

  dateToYmd(date: string) {
    //console.log('Splitting ' + date)
    let toSplit = date + ''
    let [, m, d, y] = toSplit.split(' ').slice(0, 4)

    const months = {
      'Jan': '1',
      'Feb': '2',
      'Mar': '3',
      'Apr': '4',
      'May': '5',
      'Jun': '6',
      'Jul': '7',
      'Aug': '8',
      'Sep': '9',
      'Oct': '10',
      'Nov': '11',
      'Dec': '12'
    }

    m = months[m]

    return `${y}-${m}-${d}`
  }

  load() {
    this.loading = true
    const startDate = this.dateToYmd(this.startDate) // may alter before sending
    const endDate = this.dateToYmd(this.endDate)
    this.loader(`${this.baseUrl}api/orders/period?start=${startDate}&end=${endDate}`)
  }

  onSubmit() {
    //console.log(this.dateToYdm(this.startDate))
    //console.log(this.dateToYdm(this.startDate))
    this.load();
  }
}

interface Order {
  id: number,
  money: number
}
