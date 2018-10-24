import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { LayoutComponent } from './core/layout/layout.component';

const appRoutes: Routes = [
    {
        path: 'page-not-found',
        loadChildren: './features/error-pages/page-not-found/page-not-found.module#PageNotFoundModule',
    },
    {
        path: '',
        component: LayoutComponent,
        children: [
            {
                path: '',
                redirectTo: 'destinatarios',
                pathMatch: 'full',
            },
            {
                path: 'destinatarios',
                loadChildren: './features/destinatario/destinatario.module#DestinatarioModule',
                data: {
                    breadCrumbOptions: {
                        breadCrumbLabel: 'Destinatario',
                    },
                },
            },
        ],
    },
    {
        path: '',
        component: LayoutComponent,
        children: [
            {
                path: '',
                redirectTo: 'emitentes',
                pathMatch: 'full',
            },
            {
                path: 'emitentes',
                loadChildren: './features/emitente/emitente.module#EmitenteModule',
                data: {
                    breadCrumbOptions: {
                        breadCrumbLabel: 'Emitentes',
                    },
                },
            },
        ],
    },
    { path: '**', redirectTo: 'page-not-found', pathMatch: 'full' },
];
@NgModule({
    imports: [RouterModule.forRoot(appRoutes, {
        paramsInheritanceStrategy: 'always',
    })],
    exports: [RouterModule],
})
export class AppRoutingModule { }
