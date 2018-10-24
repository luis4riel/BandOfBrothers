import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { DestinatarioListComponent } from './destinatario-list/destinatario-list.component';
import { DestinatarioResolveService } from './shared/destinatario.service';
import { DestinatarioCreateComponent } from './destinatario-create/destinatario-create.component';
import { DestinatarioViewComponent } from './destinatario-view/destinatario-view.component';
import { DestinatarioDetailComponent } from './destinatario-view/destinatario-detail/destinatario-detail.component';
import { DestinatarioEditComponent } from './destinatario-view/destinatario-edit/destinatario-edit.component';

const destinatarioRoutes: Routes = [
    {
        path: '',
        component: DestinatarioListComponent,
    },
    {
        path: 'create',
        component: DestinatarioCreateComponent,
        data: {
            breadcrumbOptions: {
                breadcrumbLabel: 'Cadastrar Destinatario',
            },
        },
    },
    {
        path: ':destinatarioId',
        resolve: {
            destinatario: DestinatarioResolveService,
        },
        data: {
            breadcrumbOptions: {
                breadcrumbLabel: 'Destinatario',
            },
        },
        children: [
            {
                path: '',
                component: DestinatarioViewComponent,
                children: [
                    {
                        path: '',
                        redirectTo: 'info',
                        pathMatch: 'full',
                    },
                    {
                        path: 'info',
                        children: [
                            {
                                path: '',
                                component: DestinatarioDetailComponent,
                            },
                            {
                                path: 'edit',
                                component: DestinatarioEditComponent,
                                data: {
                                    breadcrumbOptions: {
                                        breadcrumbLabel: 'Editar Destinatario',
                                    },
                                },
                            },
                        ],
                    },
                ],
            },
        ],
    },
];

@NgModule({
    imports: [RouterModule.forChild(destinatarioRoutes)],
    exports: [RouterModule],
    declarations: [],
    providers: [],
})

export class DestinatarioRoutingModule {

}
