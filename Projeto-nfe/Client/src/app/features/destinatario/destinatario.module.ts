import { ReactiveFormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { GridModule } from '@progress/kendo-angular-grid';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

import { NDDBreadcrumbModule } from '../../shared/ndd-ng-breadcrumb/component/ndd-ng-breadcrumb.module';
import { DestinatarioListComponent } from './destinatario-list/destinatario-list.component';
import { DestinatarioRoutingModule } from './destinatario-routing.module';
import { DestinatarioResolveService, DestinatarioService } from './shared/destinatario.service';
import { NDDTabsbarModule } from '../../shared/ndd-ng-tabsbar/component';
import { NDDTitlebarModule } from '../../shared/ndd-ng-titlebar/component';
import { DestinatarioGridService } from './shared/destinatario.grid.service';
import { DestinatarioSharedService } from './shared/destinatario.shared';
import { DestinatarioCreateComponent } from './destinatario-create/destinatario-create.component';
import { DestinatarioViewComponent } from './destinatario-view/destinatario-view.component';
import { DestinatarioDetailComponent } from './destinatario-view/destinatario-detail/destinatario-detail.component';
import { DestinatarioEditComponent } from './destinatario-view/destinatario-edit/destinatario-edit.component';
@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        DestinatarioRoutingModule,
        GridModule,
        HttpClientModule,
        NDDTabsbarModule,
        NDDBreadcrumbModule,
        NDDTitlebarModule,
    ],
    exports: [],
    declarations: [
        DestinatarioListComponent,
        DestinatarioCreateComponent,
        DestinatarioViewComponent,
        DestinatarioDetailComponent,
        DestinatarioEditComponent,
    ],
    providers: [
        DestinatarioGridService,
        DestinatarioResolveService,
        DestinatarioSharedService,
        DestinatarioService,
    ],
})

export class DestinatarioModule {

}
