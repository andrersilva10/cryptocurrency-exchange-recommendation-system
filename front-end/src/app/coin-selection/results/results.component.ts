import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { Subscription } from 'rxjs';
import { PairModel } from 'src/app/shared/models/pair.model';
import { CurrenciesService } from 'src/app/shared/services/currencies.service';
import {MatPaginator} from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}

const ELEMENT_DATA: PeriodicElement[] = [
  {position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H'},
  {position: 2, name: 'Helium', weight: 4.0026, symbol: 'He'},
  {position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li'},
  {position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be'},
  {position: 5, name: 'Boron', weight: 10.811, symbol: 'B'},
  {position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C'},
  {position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N'},
  {position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O'},
  {position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F'},
  {position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne'},
];

@Component({
  selector: 'app-results',
  templateUrl: './results.component.html',
  styleUrls: ['./results.component.css']
})
export class ResultsComponent implements OnInit {
  displayedColumns: string[] = ['idPair','exchangeName','pairPrice','pairVolume'  ];
  routeData: Subscription;
  apiData: Subscription;

  currencySymbol1: string = '';
  currencySymbol2: string = '';

  dataSource = new MatTableDataSource<PairModel>();

  @ViewChild(MatPaginator) paginator: MatPaginator;
  showResults = false;
  constructor(private _currenciesService: CurrenciesService
    , private _activatedRoute: ActivatedRoute,) { }

  ngOnInit(): void {
    this.routeData = this._activatedRoute.params.subscribe((params: Params) => {
      this.currencySymbol1 = params['trade'];
      this.currencySymbol2 = params['to'];
      if (this.currencySymbol1 && this.currencySymbol2) {
        this.showResults = false;
        this.apiData = this._currenciesService.getPairs(this.currencySymbol1,this.currencySymbol2).subscribe(data => {
          this.dataSource = new MatTableDataSource<PairModel>(data) ;
          this.showResults = true;
          this.dataSource.paginator = this.paginator;
        });
      }
    });
    
  }

}
