import { Component } from '@angular/core';
import { NgModule } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { HttpClient, HttpResponse } from '@angular/common/http';
import 'datatables.net-dt';
//import DataTable from 'datatables.net-dt';
declare var $: any;

export let product_data =
  [
    { 'pId': 1, 'name': 'Hristo', 'size': 'Gabrovo' },
    { 'pId': 1, 'name': 'Hristo', 'size': 'Gabrovo' },
    { 'pId': 1, 'name': 'Hristo', 'size': 'Gabrovo' }]


export let product_columns = [
  { 'data': 'col1', 'title': 'col1' },
  { 'data': 'col2', 'title': 'col2' },
  { 'data': 'col3', 'title': 'col3' },
  { 'data': 'col4', 'title': 'col4' }]

export let dataanalys_columns = [
  { 'data': 'pv_cnt', 'title': 'pv_cnt' },
  { 'data': 'amount', 'title': 'amount' },
  { 'data': 'price', 'title': 'price' },
  { 'data': 'week', 'title': 'week' }]




class DataTablesResponse {
  data: any[] = [];
  draw: number = 0;
  recordsFiltered : number = 0;
  recordsTotal: number = 0;
}

class DataTablesColumnsResponse {
  columns: any[] = [];
}

class MyDto {
  name: string | any;
}

@Component({
  selector: "analysis-layout",
  templateUrl: "analysisLayout.component.html"
})

export class AnalysisLayoutComponent {
  dTable: any = null;
  hTable: any = null;
  jqDataTable: any = null;
  analysisdata: any = null;
  columns: any;
  //dtOptions: DataTables.Settings = {};
  constructor(
    private _route: ActivatedRoute,
    private router: Router,
    private http: HttpClient) {
  }

  ngOnInit() {
    console.log("analysis init");
    const that = this;
 /*   $('button').click(function () {
     // alert('Wass up!');
      that.create_table();
    });*/
    
  }

  public create_table(): void {
    let that = this;
    this.dTable = $('#analysTable');
    this.hTable = this.dTable.DataTable({
      layout: {
        topStart: 'pageLength',
        topEnd: 'search',
        bottomStart: 'info',
        bottomEnd: 'paging',
      /*  bottom: [
          'pageLength',
          'info'
        ]*/
      },
      pagingType: 'numbers',
      pageLength: 3,
      serverSide: true,
      processing: true,
      columnDefs: [{
        'targets': 0,
        'searcheble': true,
        'orderable': false,
        'className': 'dt-body-center'
       }],
       ajax: (dataTablesParameters: any, callback: any) => {
        dataTablesParameters.filter = { field: "week", value: 10};
        console.log("AJAX PARAMS ===", dataTablesParameters);
        that.http
          .post<any>('/api/analysis', dataTablesParameters, {})
          .subscribe(resp => {
            that.analysisdata = resp.test;
            this.columns = resp.columns;
            console.log("SUBSCRIBE ", resp.test);
            console.log("SUBSCRIBE ", resp.columns)

            callback({
              recordsTotal: 0,
              //resp.recordsTotal, => from analysiscontrol
              recordsFiltered: 6, //=> from analysiscontrol
//                resp.recordsFiltered,
              data: resp.test,
              columns: resp.columns
            });
          });
      },
     
      responsive: true,
      //columns: dataanalys_columns,
      columns: this.columns,
      data: this.analysisdata
      
    });
    console.log("analysis after init", this.hTable);
    
  }

  upload() {
    console.log("==== upload click ====");
    this.router.navigateByUrl("/api/upload");
  }

  exploratory() {
    console.log("==== exploratory ====");
    let that = this;
    this.http
      .post('/api/analysis/Exploratory', {}, {})
      .subscribe((resp: any) => {
        that.columns = resp.columns;
    }, (error) => {
      // Handle error
    });
  }



}
