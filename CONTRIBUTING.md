Obrigado por considerar contribuir com o C# Unity Utils!

# Como contribuir

1. Abra uma issue descrevendo a mudança (um bug, um utilitário novo ou um ajuste de documentação).
2. Crie uma branch a partir da `main`, como `fix/12-nome-curto` ou `feat/12-nome-curto`.
3. Abra a Pull Request começando com `Resolve #12.`

# Padrões de código

> Nomenclatura: use nomes descritivos para variáveis e métodos. \
> Comentários: só onde o código não se explica sozinho; nos métodos públicos, um `<summary>` curto. \
> Formatação: siga o estilo dos arquivos que já existem. \
> Editor: código que usa `UnityEditor` fica na pasta `Editor/`, nunca em `Runtime/`.

# Testes

Utilitário novo vem com teste de edit mode em `Tests/Editor/`. Para rodar, adicione o pacote como local no seu projeto e inclua o nome dele em `testables` no `manifest.json`.

# Licença

Ao contribuir, você concorda que sua contribuição será licenciada sob a licença MIT.
