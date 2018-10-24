import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CORE_CONFIG_TOKEN, ICoreConfig } from '../../../core/core.config';
import { Observable } from 'rxjs/Observable';

import { BaseService } from '../../../core/utils';
import { AbstractResolveService } from '../../../core/utils/abstract-resolve.service';
import { NDDBreadcrumbService } from '../../../shared/ndd-ng-breadcrumb';

import {Destinatario, DestinatarioRegisterCommand, DestinatarioEditCommand, DestinatarioDeleteCommand } from './destinatario.model';

@Injectable()
export class DestinatarioService extends BaseService {
    private api: string;

    constructor( @Inject(CORE_CONFIG_TOKEN) config: ICoreConfig, public http: HttpClient) {
        super(http);

        this.api = `${config.apiEndpoint}api/destinatarios`;
    }

    public get(entityId: number): Observable<Destinatario> {
        return this.http.get(`${this.api}/${entityId}`).map((response: Destinatario) => response);
    }
   
    public post(destinatario: DestinatarioRegisterCommand): Observable<boolean> {
        return this.http.post(this.api, destinatario).map((response: boolean) => response);
    }

    public put(destinatario: DestinatarioEditCommand): Observable<boolean> {
        return this.http.put(this.api, destinatario).map((response: boolean) => response);
    }

    public delete(destinatarioDeleteCommand: DestinatarioDeleteCommand): Observable<boolean> {
        return this.deleteRequestWithBody(this.api, destinatarioDeleteCommand);
    }
}

@Injectable()
export class DestinatarioResolveService extends AbstractResolveService<Destinatario>{

    constructor(private destinatarioService: DestinatarioService, router: Router, private breadcrumbService: NDDBreadcrumbService) {
        super(router);
        this.paramsProperty = 'destinatarioId';
    }

    protected loadEntity(entityId: number): Observable<Destinatario> {
        return this.destinatarioService.get(entityId).take(1).do((destinatario: Destinatario) => {
            this.breadcrumbService.setMetadata({
                id: 'destinatario',
                label: destinatario.nome,
                sizeLimit: true,
            });
        });
    }
}
