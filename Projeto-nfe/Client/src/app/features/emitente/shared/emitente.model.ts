export class Emitente {
    public id: number;
    public nome: string;
    public razaoSocial: string;
    public cpf: string;
    public cnpj: string;
    public inscricaoEstadual: string;
    public localizacao: string;
}

export class EmitenteDeleteCommand {
    public emitenteIds: number[] = [];

    constructor(emitentes: Emitente[]) {
        this.emitenteIds = emitentes.map((c: Emitente) => c.id);
    }
}
export class EmitenteRegisterCommand {
    public name: string;
    public sale: number;
    public expense: number;
    public isAvailable: boolean;
    public manufacture: Date;
    public expiration: Date;
    constructor(emitente: Emitente) {
        this.name = emitente.name;
        this.sale = emitente.sale;
        this.expense = emitente.expense;
        this.isAvailable = emitente.isAvailable;
        this.manufacture = new Date(emitente.manufacture.toString());
        this.expiration = new Date(emitente.expiration.toString());
    }
}

export class EmitenteEditCommand {
    public id: number;
    public name: string;
    public sale: number;
    public expense: number;
    public isAvailable: boolean;
    public manufacture: Date;
    public expiration: Date;
    constructor(emitente: Emitente) {
        this.id = emitente.id;
        this.name = emitente.name;
        this.sale = emitente.sale;
        this.expense = emitente.expense;
        this.isAvailable = emitente.isAvailable;
        this.manufacture = new Date(emitente.manufacture.toString());
        this.expiration = new Date(emitente.expiration.toString());
    }
}
