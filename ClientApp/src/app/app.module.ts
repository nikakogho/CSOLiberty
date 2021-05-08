import { BrowserModule } from '@angular/platform-browser'
import { NgModule } from '@angular/core'
import { FormsModule } from '@angular/forms'
import { HttpClientModule } from '@angular/common/http'
import { RouterModule } from '@angular/router'

import { AppComponent } from './app.component'

import { FetchOrdersComponent } from './fetch-orders/fetch-orders.component'
import { FetchOrdersInPeriodComponent } from './fetch-orders-in-period/fetch-orders-in-period.component'
import { FetchSellersComponent } from './fetch-sellers/fetch-sellers.component'

import { MainTabsComponent } from './main-tabs/main-tabs.component'

//import { NgxMatDatetimePickerModule } from '@angular-material-components/datetime-picker'

import { MatDatepickerModule } from '@angular/material/datepicker'
import { MatNativeDateModule } from '@angular/material/core'
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material'
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'
import { MatTreeModule } from '@angular/material/tree';
import { MatIconModule } from '@angular/material/icon';
import { MatTabsModule } from '@angular/material/tabs';

const matModules: any[] = [
  FormsModule,
  MatFormFieldModule,
  MatDatepickerModule,
  MatNativeDateModule,
  MatInputModule,
  MatTreeModule,
  MatIconModule,
  MatTabsModule
]

@NgModule({
  declarations: [
    AppComponent,
    FetchOrdersComponent,
    FetchSellersComponent,
    FetchOrdersInPeriodComponent,
    MainTabsComponent
  ],
  imports: [
    BrowserAnimationsModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    RouterModule.forRoot([
      { path: '', component: MainTabsComponent, pathMatch: 'full' }
    ]),
    HttpClientModule,
    ...matModules],
  providers: [
    MatDatepickerModule,
    MatNativeDateModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
