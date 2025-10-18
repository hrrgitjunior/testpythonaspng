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

class DataTablesResponse {
  data: any[] = [];
  draw: number = 0;
  recordsFiltered : number = 0;
  recordsTotal: number = 0;
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
  //dtOptions: DataTables.Settings = {};
  constructor(
    private _route: ActivatedRoute,
    private router: Router,
    private http: HttpClient) {
  }

  ngOnInit() {
    console.log("analysis init");
    const that = this;
    $('button').click(function () {
     // alert('Wass up!');
      that.create_table();
    });
    
  }

  public create_table(): void {
    let that = this;
    this.dTable = $('#analysTable');
    this.hTable = this.dTable.DataTable({
      columnDefs: [{
        'targets': 0,
        'searcheble': true,
        'orderable': false,
        'className': 'dt-body-center'
       }],
      pagingType: 'numbers',
      pageLength: 5,
      serverSide: true,
      processing: true,
      ajax: (dataTablesParameters: any, callback: any) => {
        dataTablesParameters.filter = { field: "pId", value: 10 };
        console.log("AJAX PARAMS ===", dataTablesParameters);
        that.http
          .get<any>('/analysis', {})
          .subscribe(resp => {
            //that.products = resp.data;
            console.log("SUBSCRIBE ", resp);

            callback({
              recordsTotal: resp.recordsTotal,
              recordsFiltered: resp.recordsFiltered,
              data: resp.data,
            });
          });
      },
      responsive: true,
      columns: product_columns
      //data: product_data
      
    });
    console.log("analysis after init", this.hTable);
    
  }
  

}
