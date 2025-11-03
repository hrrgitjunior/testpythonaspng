import { Component } from '@angular/core';
import { NgModule } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { HttpClient, HttpResponse } from '@angular/common/http';
import 'datatables.net-dt';
import { Repository } from "../models/repository";
declare var $: any;



class DataTablesResponse {
  data: any[] = [];
  draw: number = 0;
  recordsFiltered : number = 0;
  recordsTotal: number = 0;
}


@Component({
  selector: "analysis-layout",
  templateUrl: "analysisLayout.component.html",
  providers: [Repository]
})

export class AnalysisLayoutComponent {
  dTable: any = null;
  hTable: any = null;
  jqDataTable: any = null;
  analysisdata: any = null;
  columns: any;
  //dtOptions: DataTables.Settings = {};
  constructor(
    private repo: Repository,
    private _route: ActivatedRoute,
    private router: Router,
    private http: HttpClient) {
  }

  ngOnInit() {
    console.log("analysis init");
    this.repo.get_dt_columns();
    
  }

  get columnsType(): any {
    console.log("=== GET COLUNS TYPE ===");''
      return this.repo.columnsType
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
      pageLength: 5,
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
            console.log("SUBSCRIBE ", resp.data);
            console.log("SUBSCRIBE ", resp.tableColumns)

            callback({
              recordsTotal: resp.rowNumber,
              //resp.recordsTotal, => from analysiscontroller
              recordsFiltered: resp.rowNumber, //=> from analysiscontroller
//                resp.recordsFiltered,
              data: resp.data,
              columns: resp.tableColumns
            });
          });
      },
     
      responsive: true,
      //columns: dataanalys_columns,
      columns: this.repo.tableColumns,
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
      .post('/api/analysis/ExploratoryColumns', {}, {})
      .subscribe((resp: any) => {
        this.columns = resp.columns;
    }, (error) => {
      // Handle error
    });
  }



}
