// scripts/medir-aula-03.js
// Compara a regra de leitura no REST e no GraphQL e conta as buscas de cada query.
// Rodar: make medir (gera relatorio-aula-03.html e abre no navegador).
import http from 'k6/http';
import { gerarRelatorio } from './relatorio-aula-03.js';

const base = __ENV.BASE_URL || 'http://localhost:5000';
const usuarios = [1, 2, 3, 4, 5];

const consultas = [
  { nome: 'Só o nome', query: '{ usuario(id: 1) { nome } }' },
  { nome: 'Tela inteira', query: '{ usuario(id: 1) { nome carteira { ativo } transacoes { ativo } rentabilidade { ativo } } }' },
  { nome: 'Cinco usuários e carteiras', query: '{ usuarios { nome carteira { ativo } } }' },
];

function graphql(query) {
  return http.post(`${base}/graphql`, JSON.stringify({ query }), {
    headers: { 'Content-Type': 'application/json' },
  });
}

function medirBuscas(query) {
  http.get(`${base}/diagnostico/buscas`);
  graphql(query);
  return http.get(`${base}/diagnostico/buscas`).json('buscas');
}

function regraDoRest(id) {
  const carteira = http.get(`${base}/web/usuarios/${id}/carteira`).json();
  const transacoes = http.get(`${base}/web/usuarios/${id}/transacoes`).json();
  return JSON.stringify({
    carteira: carteira.map((p) => [p.ativo, p.participacao]),
    transacoes: transacoes.map((t) => t.id),
  });
}

function regraDoGraphql(id) {
  const usuario = graphql(`{ usuario(id: ${id}) { carteira { ativo participacao } transacoes { id } } }`).json('data.usuario');
  return JSON.stringify({
    carteira: usuario.carteira.map((p) => [p.ativo, p.participacao]),
    transacoes: usuario.transacoes.map((t) => t.id),
  });
}

export const options = { vus: 1, iterations: 1 };

export function setup() {
  return {
    buscas: consultas.map((consulta) => ({ nome: consulta.nome, buscas: medirBuscas(consulta.query) })),
    iguais: usuarios.filter((id) => regraDoRest(id) === regraDoGraphql(id)).length,
    total: usuarios.length,
  };
}

export default function () {}

export function handleSummary(data) {
  const { buscas, iguais, total } = data.setup_data;
  return {
    'relatorio-aula-03.html': gerarRelatorio(buscas, iguais, total),
    stdout: `\nREST e GraphQL iguais: ${iguais} de ${total} usuários\n${buscas.map((m) => `${m.nome}: ${m.buscas} ${m.buscas === 1 ? 'busca' : 'buscas'}`).join('\n')}\n`,
  };
}
