import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MainLayoutComponent } from "./structure/mainLayout";
import { CategoryLayoutComponent } from "./structure/categoryLayout";
import { AnalysisLayoutComponent } from "./structure/analysisLayout";
import { UploadComponent } from './structure/upload';
import { TabComponent } from './structure/tab';
import { TabsComponent } from './structure/tabs';
//import { NgbModule} from '@ng-bootstrap/ng-bootstrap';

import { AppRoutingModule} from './app-routing.module';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent,
    MainLayoutComponent,
    CategoryLayoutComponent,
    AnalysisLayoutComponent,
    UploadComponent,
    TabComponent,
    TabsComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule
    //NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
