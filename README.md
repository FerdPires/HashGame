# HashGame

Api para um Jogo da Velha multiplayer.

### Antes de rodar a Api a primeira vez:
Dentro de .\HashGame>
```
dotnet restore
```

### Para executar:
Dentro de .\HashGame\HashGame.Api>
```
dotnet run
```

#### Rota da chamada de criar um novo jogo:
/v1/hashgame/new-game

#### Rota da chamada para fazer um movimento no jogo:
/v1/hashgame/make-movement

Exemplo de input:
```
{
    "id":"cba016fc-35aa-46fd-8f24-c25fde47ae41", // Colar aqui o Id retornado da chamada de criar um novo jogo
    "player":"X",
    "position":{
        "x": 2,
        "y": 0
    }
}
```

# 
Essa api foi desenvolvida em ASP.NET CORE 3.1 utilizando o padrão CQRS. Esse padrão irá segregar a API em leitura e escrita, utilizando na parte de domínio conceitos como commands, handlres e queries. Outros conceitos importantêntes utilizados na API foram repositories, domain notifications, desing by contracts, SOLID e clean code.
Utiliza banco de dados em memória.

