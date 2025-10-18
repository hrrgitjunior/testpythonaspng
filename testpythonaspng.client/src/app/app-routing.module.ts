import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainLayoutComponent } from "./structure/mainLayout";
import { CategoryLayoutComponent } from "./structure/categoryLayout";
import { AnalysisLayoutComponent } from "./structure/analysisLayout";


const routes: Routes = [
  {
    path: '', component: MainLayoutComponent,
    children: [
      {
        path: '', component: CategoryLayoutComponent,
        children: [
          {
            path: '', component: AnalysisLayoutComponent
          }
        ]
      }
    ]
  }
]
    //children: [
    //  { path: '', redirectTo: '', pathMatch: 'full' },
    //  {
    //    path: '', component: StoreLayoutComponent,
    //    children: [
    //      { path: '', redirectTo: 'introduction', pathMatch: 'full' },
    //      { path: "introduction", component: IntroductionComponent },
    //      {
    //        path: "category", component: CategoryWrapperComponent,
    //        children: [
    //          { path: "store/:category", component: CategoryProductsComponent },
    //          { path: "designs/:category", component: DesignsCategoryComponent },
    //          { path: "designs/:category/:page", component: DesignsCategoryComponent },
    //          { path: "detail/:id", component: ProductDetailComponent },
    //          { path: "interactive3d/:id", component: ProductDetailComponent },
    //          { path: "embroidery/:id", component: EmbroiderySimulatorComponent }
    //        ]
    //      },
    //    ]
    //  },
    //  {
    //    path: "contacts", component: ContactsComponent
    //  }

    //]
 


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
