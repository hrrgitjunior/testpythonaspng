import { Component, Input } from '@angular/core';
import { Router, ActivatedRoute, NavigationEnd } from "@angular/router";

class MyDto {
  name: string | any;
  file: File | any;
}

@Component({
  selector: 'ngx-tab',
  templateUrl: './tab.component.html',
  styles: [
    `
    .pane{
      padding: 1em;
    }
  `
  ],
})


export class TabComponent {
 // @Input('tabTitle') title: string | any;
  @Input() title: string | any;
  @Input() active = false;

  constructor() { }

}
