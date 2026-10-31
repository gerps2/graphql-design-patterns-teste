// scripts/medir-aula-01.js
// Compara a tela web em REST (4 chamadas) e em GraphQL (1 chamada).
// Conta requisições e bytes do corpo. Não é teste de carga.
// Rodar: make medir (gera relatorio-aula-01.html e abre no navegador).
import http from 'k6/http';
import { Counter } from 'k6/metrics';
import { gerarRelatorio } from './relatorio-aula-01.js';

const base = __ENV.BASE_URL || 'http://localhost:5000';

const restChamadas = new Counter('rest_chamadas');
const restBytes = new Counter('rest_bytes');
const graphqlChamadas = new Counter('graphql_chamadas');
const graphqlBytes = new Counter('graphql_bytes');

const rotas = [
  '/web/usuarios/1',
  '/web/usuarios/1/carteira',
  '/web/usuarios/1/transacoes',
  '/web/usuarios/1/rentabilidade',
];
const bytesPorRota = rotas.map((_, indice) => new Counter(`rota_${indice}_bytes`));

const query = `query TelaInicial {
  usuario(id: 1) {
    nome
    carteira { ativo saldo }
    transacoes { ativo valor }
    rentabilidade { ativo percentualMes }
  }
}`;

export const options = { vus: 1, iterations: 1 };

export default function () {
  rotas.forEach((rota, indice) => {
    const resposta = http.get(`${base}${rota}`, { responseType: 'binary' });
    restChamadas.add(1);
    restBytes.add(resposta.body.byteLength);
    bytesPorRota[indice].add(resposta.body.byteLength);
  });

  const resposta = http.post(`${base}/graphql`, JSON.stringify({ query }), {
    headers: { 'Content-Type': 'application/json' },
    responseType: 'binary',
  });
  graphqlChamadas.add(1);
  graphqlBytes.add(resposta.body.byteLength);
}

export function handleSummary(data) {
  const valor = (nome) => data.metrics[nome].values.count;
  const rest = { chamadas: valor('rest_chamadas'), bytes: valor('rest_bytes') };
  const graphql = { chamadas: valor('graphql_chamadas'), bytes: valor('graphql_bytes') };
  const detalhe = rotas.map((caminho, indice) => ({ caminho, bytes: valor(`rota_${indice}_bytes`) }));

  return {
    'relatorio-aula-01.html': gerarRelatorio({ rest, graphql, rotas: detalhe }),
    stdout: [
      '',
      `REST:    ${rest.chamadas} chamadas, ${rest.bytes} bytes`,
      `GraphQL: ${graphql.chamadas} chamada, ${graphql.bytes} bytes`,
      '',
    ].join('\n'),
  };
}
