import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { Destinatario } from '../shared/destinatario.model';
import { DestinatarioResolveService } from '../shared/destinatario.service';



@Component({
    templateUrl: './destinatario-view.component.html',
})

export class DestinatarioViewComponent implements OnInit, OnDestroy {
    public destinatario: Destinatario;
    public title: string;
    public infoItems: object[];
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(private resolver: DestinatarioResolveService) {
    }
    public ngOnInit(): void {
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((destinatario: Destinatario) => {
                this.destinatario = destinatario;
                this.createProperty();
            });
    }
    public ngOnDestroy(): void {
        this.ngUnsubscribe.next();
        this.ngUnsubscribe.complete();
    }
    private createProperty(): void {
        this.title = this.destinatario.nome? this.destinatario.nome : this.destinatario.razaoSocial;
        const inscricaoEstadual: string = 'Inscrição Estadual: ' + this.destinatario.inscricaoEstadual;
        const cpf_cnpj: string = 'CPF/CNPJ: ' + this.destinatario.cpf? this.destinatario.cpf : this.destinatario.cnpj;
        this.infoItems = [
            {
                value: inscricaoEstadual,
                description: inscricaoEstadual,
            },
            {
                value: cpf_cnpj,
                description: cpf_cnpj,
            },
        ];
    }

}