import { Injectable } from "@angular/core";
//import { Http, Headers, RequestMethod, Request, Response } from "@angular/http";
//import { ResponseContentType } from '@angular/http';
import { HttpClient, HttpRequest, HttpEvent, HttpResponse } from '@angular/common/http';

@Injectable()
export class Repository {
  tableColumns: any;
  columnsType: any;

  constructor(private http: HttpClient) {
  }

  exploratory_get_columns() {
    this.http
      .post('/api/analysis/ExploratoryColumns', {}, {})
      .subscribe((resp: any) => {
        this.tableColumns = resp.tableColumns;
        this.columnsType = resp.columnsType;
        console.log("==== repository tableColumns ===", this.tableColumns);
        console.log("==== repository tableColumns ===", resp.columnsType);
      }, (error) => {
        // Handle error
      });
  }
}
