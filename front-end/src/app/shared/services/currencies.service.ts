import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CurrencyModel } from '../models/currency.model';
import { PairModel } from '../models/pair.model';

@Injectable()
export class CurrenciesService{
    apiUrl = environment.localApiUrl + 'simple/';
    
    constructor(
        public httpClient: HttpClient
    ) 
    {

    }

    getCurrencies(): Observable<Array<CurrencyModel>>{
        const url = this.apiUrl + 'currencies';
        return <any>this.httpClient.get(url);
    }

    getPairs(currencySymbol1: string, currencySymbol2: string): Observable<Array<PairModel>>{
        const url = this.apiUrl + `exchanges/trade/${currencySymbol1}/to/${currencySymbol2}`;
        return <any>this.httpClient.get(url);
    }
}
