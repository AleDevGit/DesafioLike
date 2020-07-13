import { Resposta } from './Resposta';

export interface Usuario {

    id: number;
    cPF: string;
    senha: string;
    anoNasc: number;
    sexo: boolean;
    respostas: Resposta[];
}
