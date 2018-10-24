import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { CORE_CONFIG_TOKEN, ICoreConfig } from '../../../core/core.config';
import { Observable } from 'rxjs/Observable';

import { BaseService } from '../../../core/utils';
import { AbstractResolveService } from '../../../core/utils/abstract-resolve.service';
import { NDDBreadcrumbService } from '../../../shared/ndd-ng-breadcrumb';

import {Emitente, EmitenteRegisterCommand, EmitenteEditCommand, EmitenteDeleteCommand } from './emitente.model';

@Injectable()
export class EmitenteService extends BaseService {
    private api: string;

    constructor( @Inject(CORE_CONFIG_TOKEN) config: ICoreConfig, public http: HttpClient) {
        super(http);

        this.api = `${config.apiEndpoint}api/emitentes`;
    }

    public post(emitente: EmitenteRegisterCommand): Observable<boolean> {
        return this.http.post(this.api, emitente).map((response: boolean) => response);
    }

    public put(emitente: EmitenteEditCommand): Observable<boolean> {
        return this.http.put(this.api, emitente).map((response: boolean) => response);
    }

    public get(id: number): Observable<Emitente> {
        return this.http.get(`${this.api}/${id}`).map((response: Emitente) => response);
    }

    public delete(emitenteDeleteCommand: EmitenteDeleteCommand): Observable<boolean> {
        return this.deleteRequestWithBody(this.api, emitenteDeleteCommand);
    }
}

@Injectable()
export class EmitenteResolveService extends AbstractResolveService<Emitente>{

    constructor(private emitenteService: EmitenteService, router: Router, private breadcrumbService: NDDBreadcrumbService) {
        super(router);
        this.paramsProperty = 'emitenteId';
    }

    protected loadEntity(entityId: number): Observable<Emitente> {
        return this.emitenteService.get(entityId).take(1).do((emitente: Emitente) => {
            this.breadcrumbService.setMetadata({
                id: 'emitente',
                label: emitente.nome,
                sizeLimit: true,
            });
        });
    }
}
