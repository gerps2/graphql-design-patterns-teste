// scripts/relatorio-aula-02.js
// Monta o HTML do relatório a partir das buscas medidas pelo k6.

const linha = (medida, maximo) => `
  <div class="linha">
    <span class="nome">${medida.nome}</span>
    <div class="trilho"><div class="barra" style="width:${Math.max((medida.buscas / maximo) * 100, 2)}%"></div></div>
    <span class="valor">${medida.buscas} ${medida.buscas === 1 ? 'busca' : 'buscas'}</span>
  </div>`;

export function gerarRelatorio(medidas) {
  const maximo = Math.max(...medidas.map((m) => m.buscas));

  return `<!doctype html>
<html lang="pt-BR">
<head>
<meta charset="utf-8">
<meta name="viewport" content="width=device-width, initial-scale=1">
<title>Buscas por query</title>
<style>
  :root { --fundo:#0B1230; --cartao:#141D45; --linha:#2B3669; --texto:#F4F6FC; --suave:#A9B3D1; --destaque:#E63B6F; }
  * { box-sizing:border-box; margin:0; }
  body { min-height:100vh; padding:48px 24px; background:var(--fundo); color:var(--texto); font-family:"Segoe UI",system-ui,sans-serif; }
  main { max-width:980px; margin:0 auto; zoom:1.2; }
  .marca { color:var(--destaque); font-size:14px; font-weight:700; letter-spacing:2px; }
  h1 { margin:8px 0 6px; font-size:40px; font-weight:600; }
  .sub { color:var(--suave); font-size:18px; margin-bottom:32px; }
  .cartao { background:var(--cartao); border:1px solid var(--linha); border-radius:14px; padding:26px; }
  .linha { display:grid; grid-template-columns:230px 1fr 100px; gap:12px; align-items:center; margin-top:16px; font-size:16px; }
  .linha:first-child { margin-top:0; }
  .trilho { height:14px; background:var(--fundo); border-radius:7px; overflow:hidden; }
  .barra { height:100%; border-radius:7px; background:var(--destaque); animation:cresce .9s ease-out; }
  .valor { text-align:right; font-weight:600; }
  .nota { color:var(--suave); font-size:14px; margin-top:20px; }
  @keyframes cresce { from { width:0; } }
  @media (prefers-reduced-motion:reduce) { .barra { animation:none; } }
</style>
</head>
<body>
<main>
  <p class="marca">CORRETORA · AULA 2</p>
  <h1>Buscas por query</h1>
  <p class="sub">Quantas vezes o servidor foi aos dados para responder cada pergunta.</p>
  <section class="cartao">${medidas.map((m) => linha(m, maximo)).join('')}</section>
  <p class="nota">Medido com k6, com dados em memória e uma execução. Conta buscas, sem medir tempo. Não é teste de carga.</p>
</main>
</body>
</html>`;
}
