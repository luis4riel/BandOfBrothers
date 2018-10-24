import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { EmitenteListComponent } from './emitente-list/emitente-list.component';
//import { EmitenteDetailComponent } from './emitente-view/emitente-detail/emitente-detail.component';
//import { EmitenteViewComponent } from './emitente-view/emitente-view.component';
import { EmitenteResolveService } from './shared/emitente.service';
//import { EmitenteCreateComponent } from './emitente-create/emitente-create.component';
//import { EmitenteEditComponent } from './emitente-view/emitente-edit/emitente-edit.component';

const emitenteRoutes: Routes = [
    {
        path: '',
        component: EmitenteListComponent,
    },
    {
       /* path: 'create',
        component: EmitenteCreateComponent,
        data: {
            breadcrumbOptions: {
                breadcrumbLabel: 'Cadastrar Emitente',
            },
        },*/
    },
    {
        path: ':emitenteId',
        resolve: {
            emitente: EmitenteResolveService,
        },
        data: {
            breadcrumbOptions: {
                breadcrumbLabel: 'Emitente',
            },
        },
        /*children: [
            {
                path: '',
                component: EmitenteViewComponent,
                children: [
                    {
                        path: '',
                        redirectTo: 'info',
                        pathMatch: 'full',
                    },
                    {
                       /* path: 'info',
                        children: [
                            {
                                path: '',
                                component: EmitenteDetailComponent,
                            },
                            {
                                path: 'edit',
                                component: EmitenteEditComponent,
                                data: {
                                    breadcrumbOptions: {
                                        breadcrumbLabel: 'Editar Emitente',
                                    },
                                },
                            },
                        ],
                    },
                ],
            },
        ],*/
    },
];

@NgModule({
    imports: [RouterModule.forChild(emitenteRoutes)],
    exports: [RouterModule],
    declarations: [],
    providers: [],
})

export class EmitenteRoutingModule {

}
