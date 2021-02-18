import { HttpClient } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CoinSelectionModule } from './coin-selection/coin-selection.module';
import { CurrenciesService } from './shared/services/currencies.service';
import { HttpClientModule } from '@angular/common/http';
import {MatButtonModule} from '@angular/material/button';
import { ChartsModule } from 'ng2-charts';
import { ExchangeRateService } from './shared/services/exchange-rate.service';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    CoinSelectionModule,
    HttpClientModule,
    ChartsModule
  ],
  providers: [HttpClient,CurrenciesService, ExchangeRateService],
  bootstrap: [AppComponent]
})
export class AppModule { }
