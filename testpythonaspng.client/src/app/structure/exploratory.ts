import { Component } from '@angular/core';
import { NgModule } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Repository } from "../models/repository";



@Component({
  selector: "exploratory-layout",
  templateUrl: "exploratory.component.html",
  providers: [Repository]
})

export class ExploratoryComponent {

  constructor(
    private repo: Repository,
    private _route: ActivatedRoute,
    private router: Router,
    private http: HttpClient) {
  }

  get columnsType(): any {
    console.log("=== GET COLUNS TYPE ===");
    return this.repo.columnsType
    //return this.repo.products;
  }

  get corr_image_url(): any {
    console.log("=== GET CORR IMAGE URL ===");
    return this.repo.corr_image_url;
  }

  public get_exploratory_columns() {
    this.repo.exploratory_get_columns();
  }

}
