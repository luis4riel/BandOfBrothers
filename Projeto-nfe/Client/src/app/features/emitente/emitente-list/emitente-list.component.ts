import { Router, ActivatedRoute } from '@angular/router';
import { Component } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { GridDataResult, DataStateChangeEvent, SelectionEvent } from '@progress/kendo-angular-grid';
import { State } from '@progress/kendo-data-query';

import { EmitenteGridService } from '../shared/emitente.grid.service';
import { EmitenteService } from '../shared/emitente.service';
import { GridUtilsComponent } from '../../../shared/grid-utils/grid-utils-component';
import { EmitenteDeleteCommand } from '../shared/emitente.model';

@Component({
    templateUrl: './emitente-list.component.html',
})

export class EmitenteListComponent extends GridUtilsComponent {
    public view: Observable<GridDataResult>;
    public emitenteService: EmitenteService;
    public state: State = {
        skip: 0,
        take: 10,
    };

    constructor(private serviceGrid: EmitenteGridService, private service: EmitenteService, private router: Router,
        private route: ActivatedRoute) {
        super();
        this.view = this.serviceGrid;
        this.emitenteService = this.service;
        this.serviceGrid.query(this.createFormattedState());
    }

    public deleteEmitente(): void {
        this.serviceGrid.loading = true;
        const emitenteToDelete: EmitenteDeleteCommand = new EmitenteDeleteCommand(this.getSelectedEntities());

        this.emitenteService.delete(emitenteToDelete)
            .take(1)
            .do(() => this.serviceGrid.loading = false)
            .subscribe(() => {
                this.selectedRows = [];
                this.serviceGrid.query(this.createFormattedState());
            });
    }
    public addemitente(): void {
        this.router.navigate(['create'], {
            relativeTo: this.route,
        });
    }
    public redirectOpenEmitente(): void {
        this.router.navigate(['./', `${this.getSelectedEntities()[0].id}`], { relativeTo: this.route });
    }

    public dataStateChange(state: DataStateChangeEvent): void {
        this.state = state;
        this.serviceGrid.query(this.createFormattedState());
        this.selectedRows = [];
    }

    public onSelectionChange(event: SelectionEvent): void {
        this.updateSelectedRows(event.selectedRows, true);
        this.updateSelectedRows(event.deselectedRows, false);
    }

}