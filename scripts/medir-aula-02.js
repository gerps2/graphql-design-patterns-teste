// scripts/medir-aula-02.js
// Conta quantas buscas o servidor faz para responder cada query.
// Rodar: make medir (gera relatorio-aula-02.html e abre no navegador).
import http from 'k6/http';
import { gerarRelatorio } from './relatorio-aula-02.js';

const base = __ENV.BASE_URL || 'http://localhost:5000';

const consultas = [
  { nome: 'Só o nome', query: '{ usuario(id: 1) { nome } }' },
  { nome: 'Nome e carteira', query: '{ usuario(id: 1) { nome carteira { ativo } } }' },
  {
    nome: 'Tela inteira',
    query: '{ usuario(id: 1) { nome carteira { ativo } transacoes { ativo } rentabilidade { ativo } } }',
  },
  { nome: 'Cinco usuários e carteiras', query: '{ usuarios { nome carteira { ativo } } }' },
];

function medir(query) {
  http.get(`${base}/diagnostico/buscas`);
  http.post(`${base}/graphql`, JSON.stringify({ query }), {
    headers: { 'Content-Type': 'application/json' },
  });
  return http.get(`${base}/diagnostico/buscas`).json('buscas');
}

export const options = { vus: 1, iterations: 1 };

export function setup() {
  return consultas.map((consulta) => ({ nome: consulta.nome, buscas: medir(consulta.query) }));
}

export default function () {}

export function handleSummary(data) {
  const medidas = data.setup_data;
  return {
    'relatorio-aula-02.html': gerarRelatorio(medidas),
    stdout: `\n${medidas.map((m) => `${m.nome}: ${m.buscas} ${m.buscas === 1 ? 'busca' : 'buscas'}`).join('\n')}\n`,
  };
}
