import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';


import { Subject } from 'rxjs/Subject';
import { Destinatario, DestinatarioEditCommand } from '../../shared/destinatario.model';
import { DestinatarioService, DestinatarioResolveService } from '../../shared/destinatario.service';

@Component({
    templateUrl: './destinatario-edit.component.html',
})

export class DestinatarioEditComponent implements OnInit {
    public destinatario: Destinatario;
    public destinatarioService: DestinatarioService;
    public isLoading: boolean = false;
    public title: string = 'Editar Produto';

    public destinatarioEditForm: FormGroup = this.fb.group({
        id: ['', Validators.required],
        nome: ['', Validators.required],
        razaoSocial: ['', Validators.required],
        cpf: ['', Validators.required],
        cnpj: ['', Validators.required],
        inscricaoEstadual: ['', Validators.required],
        logradouro: ['', Validators.required],
        municipio: ['', Validators.required],
        bairro: ['', Validators.required],
        numero: ['', Validators.required],
        estado: ['', Validators.required],
    });
    private ngUnsubscribe: Subject<void> = new Subject<void>();

    constructor(private fb: FormBuilder,
        private service: DestinatarioService,
        private resolver: DestinatarioResolveService,
        private router: Router,
        private route: ActivatedRoute) {
        this.destinatarioService = this.service;
    }

    public ngOnInit(): void {
        const index: number = 16;
        this.resolver.onChanges
            .takeUntil(this.ngUnsubscribe)
            .subscribe((destinatario: Destinatario) => {
                this.isLoading = false;
                this.destinatario = Object.assign(new Destinatario(), destinatario);
                this.destinatarioEditForm.setValue({
                    id: this.destinatario.id,
                    nome: this.destinatario.nome,
                    inscricaoEstadual: this.destinatario.inscricaoEstadual,
                    numero: this.destinatario.numero,
                });
            });

    }
    public redirect(): void {
        this.router.navigate(['../'], { relativeTo: this.route });
    }

    public onEdit(): void {
        const destinatarioEdit: DestinatarioEditCommand = new DestinatarioEditCommand(this.destinatarioEditForm.value);
        this.isLoading = true;
        this.destinatarioService.put(destinatarioEdit)
            .take(1)
            .subscribe(() => {
                this.isLoading = false;
                this.resolver.resolveFromRouteAndNotify();
                this.redirect();
            });
    }
}
