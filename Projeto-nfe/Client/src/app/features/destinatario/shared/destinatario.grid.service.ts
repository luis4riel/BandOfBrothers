import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';
import { ICoreConfig, CORE_CONFIG_TOKEN } from '../../../core/core.config';
import { State, toODataString } from '@progress/kendo-data-query';
import { GridDataResult } from '@progress/kendo-angular-grid';

@Injectable()
export class DestinatarioGridService extends BehaviorSubject<GridDataResult>{
    public loading: Boolean = false;

    constructor( @Inject(CORE_CONFIG_TOKEN) private config: ICoreConfig,
        private http: HttpClient) {
        super(null);
    }

    public query(state: State): void {
        this.fetch(state).take(1).subscribe((result: GridDataResult) => super.next(result));
    }

    protected fetch(state: any): Observable<GridDataResult> {
        const queryStr: string = `${toODataString(state)}&$count=true`;
        this.loading = true;

        return this.http.get(`${this.config.apiEndpoint}api/destinatarios?${queryStr}`)
            .map((response: any): GridDataResult => ({
                data: response.items,
                total: parseInt(response.count, 10),
            }))
            .do(() => this.loading = false);
    }
}