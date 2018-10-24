import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { GridModule } from '@progress/kendo-angular-grid';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

import { NDDBreadcrumbModule } from '../../shared/ndd-ng-breadcrumb/component/ndd-ng-breadcrumb.module';
import { EmitenteListComponent } from './emitente-list/emitente-list.component';
import { EmitenteRoutingModule } from './emitente-routing.module';
import { EmitenteResolveService, EmitenteService } from './shared/emitente.service';
import { NDDTabsbarModule } from '../../shared/ndd-ng-tabsbar/component';
import { NDDTitlebarModule } from '../../shared/ndd-ng-titlebar/component';
import { EmitenteGridService } from './shared/emitente.grid.service';
import { EmitenteSharedService } from './shared/emitente.shared';
@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        EmitenteRoutingModule,
        GridModule,
        HttpClientModule,
        NDDTabsbarModule,
        NDDBreadcrumbModule,
        NDDTitlebarModule,
    ],
    exports: [],
    declarations: [
        EmitenteListComponent,
    ],
    providers: [
        EmitenteGridService,
        EmitenteResolveService,
        EmitenteSharedService,
        EmitenteService,
    ],
})

export class EmitenteModule {

}
