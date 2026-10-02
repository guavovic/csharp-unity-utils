# C# Unity Utils

Pacote de utilitários para Unity, tirados de projetos reais, para não reescrever o mesmo código a cada jogo novo.

## O que tem

| Área | Itens |
|---|---|
| Inspector | `[ReadOnly]`, `[Required]`, `[ShowIf]` e `SceneField` (arraste a cena e use o nome ou o caminho) |
| Jogo | `Singleton<T>`, `Cooldown`, `CoroutineRunner`, `SafeAreaFitter` e `FrameRateDisplay` |
| Dados | `TypedPlayerPrefs` (bool, enum e objeto como JSON), `ApplicationLanguageIdentifier` e `DateRegionConverter` |
| Extensões | `Vector2` e `Vector3`, `Transform`, `Color`, `LayerMask`, listas (`RandomElement`, `Shuffle`) e `MonoBehaviour` |
| Texto | `StringUtilities.FormatFieldName` |

## Instalação

No Unity, abra **Window > Package Manager > Add package from git URL** e use:

```
https://github.com/guavovic/csharp-unity-utils.git
```

## Como foi feito

- C# em um pacote UPM para Unity 2021.3 ou mais novo, com os assemblies de Runtime e Editor separados por `asmdef`
- drawers do inspector no assembly de Editor, para o código de editor nunca entrar no build do jogo
- testes de edit mode com NUnit e Unity Test Framework

> [!IMPORTANT]
> Contribuições são bem-vindas. Veja o [CONTRIBUTING.md](CONTRIBUTING.md).
