import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { CurrencyModel } from 'src/app/shared/models/currency.model';
import { CurrenciesService } from 'src/app/shared/services/currencies.service';

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css']
})
export class FormComponent implements OnInit {

  form: FormGroup;

  currencies1: CurrencyModel[];
  currencies2: CurrencyModel[];

  filteredCurrencies1: Observable<CurrencyModel[]>;
  filteredCurrencies2: Observable<CurrencyModel[]>;

  constructor(
    private _currenciesService: CurrenciesService,
    private _snackBar: MatSnackBar
  ) {

    this.form = new FormGroup({
      currencySourceControl: new FormControl(null, [Validators.required]),
      currencyTargetControl: new FormControl(null, [Validators.required])
    });
  }

  ngOnInit(): void {
    this.getCurrencies();
  }

  getCurrencies() {
    this._currenciesService.getCurrencies().subscribe((data: Array<CurrencyModel>) => {
      this.currencies1 = data;
      this.currencies2 = Object.assign([], data);
      this.setObservables();
    });
  }

  displayFn(value?: any): string | undefined {
    if (!value)
      return undefined;
    return typeof value === 'object' ? value.name : value;
  }

  onSearchClick() {
    if (!this.form.valid)
      return;
    let value1 = this.form.controls['currencySourceControl'].value as CurrencyModel;
    let value2 = this.form.controls['currencyTargetControl'].value as CurrencyModel;

    if(value1.nameId == value2.nameId)
      this._snackBar.open("Choose different currencies","", {duration : 2000})
  }
  private _filter(value, obj: CurrencyModel[]): CurrencyModel[] {
    const filterValue = typeof value == 'string' ? value.toLowerCase() : value.name;
    return obj.filter(option => option.name.toLowerCase().includes(filterValue));
  }

  private setObservables() {
    this.filteredCurrencies1 = this.form.controls['currencySourceControl'].valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value, this.currencies1))
      );

    this.filteredCurrencies2 = this.form.controls['currencyTargetControl'].valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value, this.currencies2))
      );
  }
}
