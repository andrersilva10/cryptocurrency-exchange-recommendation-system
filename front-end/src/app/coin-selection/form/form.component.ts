import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import {map, startWith} from 'rxjs/operators';
import { CurrencyModel } from 'src/app/shared/models/currency.model';
import { CurrenciesService } from 'src/app/shared/services/currencies.service';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {
  currencySourceControl = new FormControl();
  currencyTargetControl = new FormControl();

  currencies1: CurrencyModel[];
  currencies2: CurrencyModel[]; 
  filteredOptions1: Observable<CurrencyModel[]>;
  filteredOptions2: Observable<CurrencyModel[]>;

  constructor(private currenciesService: CurrenciesService) { }

  ngOnInit(): void {
    debugger;

    this.getCurrencies();
  }

  getCurrencies(){
    this.currenciesService.getCurrencies().subscribe((data: Array<CurrencyModel>) => {
      this.currencies1 = data;
      this.currencies2 = Object.assign([], data);
      this.setObservables();
    });
  }

  private _filter(value: string, obj: CurrencyModel[]): CurrencyModel[] {
    //arrumar o filtro, aceitar as outras propriedades
    const filterValue = value.toLowerCase();
    return obj.filter(option => option.name.toLowerCase().includes(filterValue));
  }

  private setObservables (){
    this.filteredOptions1 =  this.currencySourceControl.valueChanges
    .pipe(
      startWith(''),
      map(value => this._filter(value, this.currencies1))
    );

    this.filteredOptions2 =this.currencyTargetControl.valueChanges
    .pipe(
      startWith(''),
      map(value => this._filter(value, this.currencies2))
    );
  }
}
