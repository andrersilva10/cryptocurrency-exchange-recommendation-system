import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Params } from '@angular/router';
import { ChartDataSets } from 'chart.js';
import { Label } from 'ng2-charts';
import { Subscription } from 'rxjs';
import { PairModel } from 'src/app/shared/models/pair.model';
import { CurrenciesService } from 'src/app/shared/services/currencies.service';
import { ExchangeRateService } from 'src/app/shared/services/exchange-rate.service';

@Component({
  selector: 'app-results',
  templateUrl: './results.component.html',
  styleUrls: ['./results.component.css']
})
export class ResultsComponent implements OnInit, AfterViewInit {
  displayedColumns: string[] = ['idPair', 'exchangeName', 'pairPrice', 'pairVolume'];
  routeData: Subscription;
  apiData: Subscription;

  currencySymbol1: string = '';
  currencySymbol2: string = '';

  dataSource = new MatTableDataSource<PairModel>();

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  showResults = false;
  dataFound = true;
  showChart = false;

  chartLabels: Label[];
  chartData: ChartDataSets[];

  startRequest;
  endRequest;

  constructor(
    private _currenciesService: CurrenciesService,
    private _activatedRoute: ActivatedRoute,
    private _exchangeRateService: ExchangeRateService) { }

  ngOnInit(): void {
    this.routeData = this._activatedRoute.params.subscribe((params: Params) => {
      this.currencySymbol1 = params['trade'];
      this.currencySymbol2 = params['to'];
      if (this.currencySymbol1 && this.currencySymbol2) {
        this.showResults = false;
        this.startRequest = performance.now();
        this.apiData = this._currenciesService
                          .getPairs(this.currencySymbol1, this.currencySymbol2)
                            .subscribe(data => this.getPairsCallback(data));

      }
    });

  }

  setTimeSeries() {

  }
  goToLink(url: string) {
    window.open(url, '_blank');
  }

  ngAfterViewInit() {
  }

  getPairsCallback = (data) => {
    this.dataSource = new MatTableDataSource<PairModel>(data);

    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;

    this._exchangeRateService.getTimeSeries(this.currencySymbol1, this.currencySymbol2).subscribe(data2 => this.chartCallback(data2));

    this.showResults = true;
    this.dataFound = data != null && data.length > 0;
    this.endRequest =Math.round(performance.now() - this.startRequest);
  }

  chartCallback = (data) => {
    let chartLabels = Object.getOwnPropertyNames(data.rates);
    if(chartLabels.length == 0){
      this.showChart = false;
      return;
    }
      
    let chartData = [];

    chartLabels.forEach(element => {
      chartData.push(data.rates[element][this.currencySymbol2]);
    });
    this.chartLabels  = chartLabels;
    this.chartData = [{data: chartData, label: this.currencySymbol1 + '/' + this.currencySymbol2}];
    this.showChart = true;

  }



}
