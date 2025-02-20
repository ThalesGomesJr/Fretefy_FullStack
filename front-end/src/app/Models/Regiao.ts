import { RegiaoCidade } from "./RegiaoCidade";

export interface Regiao {
    id: string;
    nome: string;
    ativo: boolean;
    regiaoCidades: RegiaoCidade[];
}