import {
  Component,
  ContentChildren,
  QueryList,
  AfterContentInit,
  ViewChild,
  ComponentFactoryResolver,
  ViewContainerRef
} from '@angular/core';
import { TabComponent } from './tab';


  @Component({
    selector: 'ngx-tabs',
    templateUrl: './tabs.component.html',
    styles: [
      `
      .tabs-container .nav-tabs{
          border-bottom:0px solid blue;
        }

      .tabs-container {
        height: 200px;
        margin:2px;
      }
   

      .tabs-container .tab{
        color: green;
        padding:5px;
        text-align:center;
        width:150px;
        border-bottom: 0px !important;
        cursor: pointer;

      }
      .tabs-container .tab:hover{
         border-radius:2px;
         opacity:100%;
      }
       .tabs-container .tab.active{
         background-color: #fff;
         border-top: 3px solid rgb(30, 18, 197);
         border-left: 1px solid #ccc;
         border-right: 1px solid #ccc;
         position: relative;
         bottom: -1px;
         }
       .tabs-container .cont{
         background-color: #fff;
         padding: 6px 12px;
         border: 1px solid #ccc;
         }
  


    `
    ]
  })

export class TabsComponent implements AfterContentInit {

  @ContentChildren(TabComponent) tabs: QueryList<TabComponent> | any;

  // contentChildren are set
  ngAfterContentInit() {
    // get all active tabs
    let activeTabs = this.tabs.filter((tab: any) => tab.active);

    // if there is no active tab set, activate the first
    if (activeTabs.length === 0) {
      this.selectTab(this.tabs.first);
    }
  }

  selectTab(tab: TabComponent) {
    // deactivate all tabs
     console.log("++++++++++++", tab);
    this.tabs.toArray().forEach((tab: TabComponent) => tab.active = false);

    // activate the tab the user has clicked on.
    tab.active = true;
  }
}

