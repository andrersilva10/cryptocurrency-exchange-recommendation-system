import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CurrencyModel } from '../models/currency.model';
import { PairModel } from '../models/pair.model';

@Injectable()
export class ExchangeRateService{
    apiUrl = "https://api.exchangerate.host/";

    constructor(
        public httpClient: HttpClient
    ) 
    {

    }

    getTimeSeries(base: string, quote: string){
        let tenDaysAgo = new Date();
        tenDaysAgo.setDate(tenDaysAgo.getDate() - 10);
        let today = new Date();

        //Colocando um 0 a esquerda (Sem esse 0 a API nao está retornando dados)
        let month1 = this.putZeroInTheLeft((tenDaysAgo.getMonth() + 1));
        let month2 = this.putZeroInTheLeft((today.getMonth() + 1));
        let day1 = this.putZeroInTheLeft(tenDaysAgo.getDate());
        let day2 = this.putZeroInTheLeft(today.getDate());
        
        let d1 =`${tenDaysAgo.getFullYear()}-${month1}-${day1}` ;
        let d2 =`${today.getFullYear()}-${month2}-${day2}` ;

        const url = this.apiUrl + `timeseries?start_date=${d1}&end_date=${d2}&base=${base}&symbols=${quote}`;

        return <any>this.httpClient.get(url);

    }

    putZeroInTheLeft(number){
        return number < 10 ? `0${number}` : number;
    }
}
