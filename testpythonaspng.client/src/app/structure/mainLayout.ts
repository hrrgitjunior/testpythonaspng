import { Component } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";


@Component({
  selector: "main-layout",
  templateUrl: "mainLayout.component.html"
})

export class MainLayoutComponent {
  constructor(
    private _route: ActivatedRoute,
    private router: Router) {
  }

  getNavigation() {
    if ((this.router.url == "/contacts") || (this.router.url == "/admin"))
      return "/";
    else
      return this.router.url;
  }

}
