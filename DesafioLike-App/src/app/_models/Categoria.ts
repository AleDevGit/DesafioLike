import { Pergunta } from './Pergunta';

export interface Categoria {
    id: number;
    descricao: string;
    ativo: boolean;
    perguntas: Pergunta[];

}


