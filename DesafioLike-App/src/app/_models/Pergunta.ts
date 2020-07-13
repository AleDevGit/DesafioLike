import { Resposta } from './Resposta';

export interface Pergunta {
    id: number;
    categoriaId: number;
    descricao: string;
    opcao1: string;
    opcao2: string;
    ativo: boolean;
    respostas: Resposta[];
}
