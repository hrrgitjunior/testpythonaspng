import { Injectable } from "@angular/core";
//import { Http, Headers, RequestMethod, Request, Response } from "@angular/http";
//import { ResponseContentType } from '@angular/http';
import { HttpClient, HttpRequest, HttpEvent, HttpResponse } from '@angular/common/http';

@Injectable()
export class Repository {
  tableColumns: any;
  columnsType: any;
  corr_image_url: string = "";

  constructor(private http: HttpClient) {
  }

  get_dt_columns() {
    this.http
      .post('/api/analysis/GetDTColumns', {}, {})
      .subscribe((resp: any) => {
        this.tableColumns = resp.tableColumns;
        console.log("==== repository tableColumns ===", this.tableColumns);
      }, (error) => {
        // Handle error
      });
  }

  exploratory_get_columns() {
    this.http
      .post('/api/analysis/ExploratoryColumns', {}, {})
      .subscribe((resp: any) => {
        this.columnsType = resp.columnsType;
        this.corr_image_url = resp.corr_image_url;
        console.log("==== repository tableColumns ===", resp.columnsType);
        console.log("==== repository corr_image_url ===", resp.corr_image_url);
      }, (error) => {
        // Handle error
      });
  }
}
