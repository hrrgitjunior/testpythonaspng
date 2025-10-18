import { Component } from '@angular/core';
import { Router, ActivatedRoute } from "@angular/router";


@Component({
  selector: "category-layout",
  templateUrl: "categoryLayout.component.html"
})

export class CategoryLayoutComponent {
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
