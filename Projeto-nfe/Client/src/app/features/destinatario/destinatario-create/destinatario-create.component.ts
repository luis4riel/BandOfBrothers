import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { DestinatarioService } from '../shared/destinatario.service';
import { DestinatarioRegisterCommand } from '../shared/destinatario.model';

@Component({
  templateUrl: './destinatario-create.component.html',
})

export class DestinatarioCreateComponent implements OnInit {
  public destinatarioService: DestinatarioService;
  public isLoading: boolean = false;
  public title: string = 'Cadastro de destinatário';
  public destinatarioCreateForm: FormGroup = this.fb.group({
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
  },
);

    constructor(private fb: FormBuilder,
    private service: DestinatarioService,
    private router: Router,
    private route: ActivatedRoute) {
    this.destinatarioService = this.service;

  }
  public ngOnInit(): void {
    //
  }
  public redirect(): void {
    this.router.navigate(['/'], { relativeTo: this.route });
  }
  public onCreate(): void {
    const destinatarioCreate: DestinatarioRegisterCommand = new DestinatarioRegisterCommand(this.destinatarioCreateForm.value);
    this.destinatarioService.post(destinatarioCreate)
      .take(1)
      .subscribe(() => {
        this.isLoading = true;
        alert('Salvo com sucesso');
        this.redirect();
      });
  }
}
