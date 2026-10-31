// scripts/relatorio-aula-01.js
// Monta o HTML do relatório a partir dos números medidos pelo k6.

const reducao = (antes, depois) => Math.round((1 - depois / antes) * 100);

const barra = (nome, valor, maximo, unidade, classe) => `
  <div class="linha">
    <span class="nome">${nome}</span>
    <div class="trilho"><div class="barra ${classe}" style="width:${Math.max((valor / maximo) * 100, 2)}%"></div></div>
    <span class="valor">${valor} ${valor === 1 ? unidade.singular : unidade.plural}</span>
  </div>`;

const cartao = (titulo, rest, graphql, unidade) => `
  <section class="cartao">
    <h2>${titulo}</h2>
    <p class="destaque">${reducao(rest, graphql)}% a menos</p>
    ${barra('REST', rest, rest, unidade, 'rest')}
    ${barra('GraphQL', graphql, rest, unidade, 'graphql')}
  </section>`;

export function gerarRelatorio({ rest, graphql, rotas }) {
  const itens = rotas
    .map((rota) => `<tr><td>GET ${rota.caminho}</td><td>${rota.bytes} bytes</td></tr>`)
    .join('');

  return `<!doctype html>
<html lang="pt-BR">
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<title>REST x GraphQL</title>
<style>
  :root { --fundo:#0B1230; --cartao:#141D45; --linha:#2B3669; --texto:#F4F6FC; --suave:#A9B3D1; --rest:#7C8BBA; --graphql:#E63B6F; }
  * { box-sizing:border-box; margin:0; }
  body { min-height:100vh; padding:48px 24px; background:var(--fundo); color:var(--texto); font-family:"Segoe UI",system-ui,sans-serif; }
  main { max-width:980px; margin:0 auto; zoom:1.2; }
  .marca { color:var(--graphql); font-size:14px; font-weight:700; letter-spacing:2px; }
  h1 { margin:8px 0 6px; font-size:40px; font-weight:600; }
  .sub { color:var(--suave); font-size:18px; margin-bottom:32px; }
  .grade { display:grid; grid-template-columns:repeat(auto-fit,minmax(300px,1fr)); gap:20px; }
  .cartao { background:var(--cartao); border:1px solid var(--linha); border-radius:14px; padding:26px; }
  h2 { color:var(--suave); font-size:16px; font-weight:600; }
  .destaque { color:var(--graphql); font-size:36px; font-weight:700; margin:6px 0 20px; }
  .linha { display:grid; grid-template-columns:70px 1fr 100px; gap:12px; align-items:center; margin-top:12px; font-size:16px; }
  .trilho { height:14px; background:var(--fundo); border-radius:7px; overflow:hidden; }
  .barra { height:100%; border-radius:7px; animation:cresce .9s ease-out; }
  .rest { background:var(--rest); } .graphql { background:var(--graphql); }
  .valor { text-align:right; font-weight:600; }
  @keyframes cresce { from { width:0; } }
  table { width:100%; border-collapse:collapse; margin-top:12px; font-size:16px; }
  td { padding:10px 0; border-top:1px solid var(--linha); }
  td:first-child { font-family:Consolas,monospace; } td:last-child { text-align:right; }
  .nota { color:var(--suave); font-size:14px; margin-top:20px; }
  .detalhe { margin-top:20px; }
  @media (prefers-reduced-motion:reduce) { .barra { animation:none; } }
</style>
</head>
<body>
<main>
  <p class="marca">CORRETORA · AULA 1</p>
  <h1>REST x GraphQL</h1>
  <p class="sub">A mesma tela inicial, medida nos dois formatos.</p>
  <div class="grade">
    ${cartao('Chamadas para montar a tela', rest.chamadas, graphql.chamadas, { singular: 'chamada', plural: 'chamadas' })}
    ${cartao('Bytes recebidos no corpo', rest.bytes, graphql.bytes, { singular: 'byte', plural: 'bytes' })}
  </div>
  <section class="cartao detalhe">
    <h2>O que o REST trouxe em cada chamada</h2>
    <table>${itens}</table>
  </section>
  <p class="nota">Medido com k6, na mesma máquina, com uma execução. Conta chamadas e bytes do corpo. Não é teste de carga.</p>
</main>
</body>
</html>`;
}
