import { Endereco } from "../../../shared/models/endereco/endereco.model";

export class Destinatario {
    public id: number;
    public nome: string;
    public razaoSocial: string;
    public cpf: string;
    public cnpj: string;
    public inscricaoEstadual: string;

    public logradouro: string;
    public numero: string;
    public bairro: string;
    public municipio: string;
    public estado: string;

    public endereco: Endereco;
}
export class DestinatarioDeleteCommand {
    public destinatarioIds: number[] = [];

    constructor(destinatarios: Destinatario[]) {
        this.destinatarioIds = destinatarios.map((c: Destinatario) => c.id);
    }
}
export class DestinatarioRegisterCommand {
    public id: number;
    public nome: string;
    public razaoSocial: string;
    public cpf: string;
    public cnpj: string;
    public inscricaoEstadual: string;
    public endereco: Endereco;
    constructor(destinatario: Destinatario) {
        this.nome = destinatario.nome;
        this.razaoSocial = destinatario.razaoSocial ? destinatario.razaoSocial : "";
        this.cpf = destinatario.cpf ? destinatario.cpf : "00000000000";
        this.cnpj = destinatario.cnpj ? destinatario.cnpj : "00000000000";
        this.inscricaoEstadual = destinatario.inscricaoEstadual;
        
        this.endereco = new Endereco();
        this.endereco.logradouro = destinatario.logradouro;
        this.endereco.numero = destinatario.numero;
        this.endereco.bairro = destinatario.bairro;
        this.endereco.municipio = destinatario.municipio;
        this.endereco.estado = destinatario.estado;
    }
    
}
export class DestinatarioEditCommand {
    public id: number;
    public nome: string;
    public razaoSocial: string;
    public cpf: string;
    public cnpj: string;
    public inscricaoEstadual: string;
    public localizacao: string;
    constructor(destinatario: Destinatario) {
        this.nome = destinatario.nome;
        this.razaoSocial = destinatario.razaoSocial;
        this.cpf = destinatario.cpf;
        this.cnpj = destinatario.cnpj;
        this.inscricaoEstadual = destinatario.inscricaoEstadual;

    }
}
