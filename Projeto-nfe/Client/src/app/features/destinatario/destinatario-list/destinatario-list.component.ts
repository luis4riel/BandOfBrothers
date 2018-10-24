import { Router, ActivatedRoute } from '@angular/router';
import { Component } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { GridDataResult, DataStateChangeEvent, SelectionEvent } from '@progress/kendo-angular-grid';
import { State } from '@progress/kendo-data-query';

import { DestinatarioGridService } from '../shared/destinatario.grid.service';
import { DestinatarioService } from '../shared/destinatario.service';
import { GridUtilsComponent } from '../../../shared/grid-utils/grid-utils-component';
import { DestinatarioDeleteCommand } from '../shared/destinatario.model';

@Component({
    templateUrl: './destinatario-list.component.html',
})

export class DestinatarioListComponent extends GridUtilsComponent {
    public view: Observable<GridDataResult>;
    public destinatarioService: DestinatarioService;
    public state: State = {
        skip: 0,
        take: 10,
    };

    constructor(private serviceGrid: DestinatarioGridService, private service: DestinatarioService, private router: Router,
        private route: ActivatedRoute) {
        super();
        this.view = this.serviceGrid;
        this.destinatarioService = this.service;
        this.serviceGrid.query(this.createFormattedState());
    }

    public deleteDestinatario(): void {
        this.serviceGrid.loading = true;
        const destinatarioToDelete: DestinatarioDeleteCommand = new DestinatarioDeleteCommand(this.getSelectedEntities());

        this.destinatarioService.delete(destinatarioToDelete)
            .take(1)
            .do(() => this.serviceGrid.loading = false)
            .subscribe(() => {
                this.selectedRows = [];
                this.serviceGrid.query(this.createFormattedState());
            });
    }
    
    public addDestinatario(): void {
        this.router.navigate(['create'], {
            relativeTo: this.route,
        });
    }

    public redirectOpenDestinatario(): void {
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