import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CurrencyModel } from '../models/currency.model';

@Injectable()
export class CurrenciesService{
    apiUrl = "https://localhost:44332/api/simple/";

    constructor(
        public httpClient: HttpClient
    ) 
    {

    }

    getCurrencies(): Observable<Array<CurrencyModel>>{
        const url = this.apiUrl + 'currencies';
        return <any>this.httpClient.get(url);
    }
}
