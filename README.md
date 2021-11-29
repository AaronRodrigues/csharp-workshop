# C# Workshop

This Api determines the winner of superheroes battling supervillains. 

The characters and their stats are stored in a json file stored here - https://jsonkeeper.com/b/X95Y

Our `BattleController` pulls the json file and works out the winner by comparing the scores from the json.


## Battle endpoint

Have a look at the `/battle` endpoint. How would you make this better?

How would you improve the tests in `./Superheroes.Tests/BattleTests.cs`?


## Weaknesses

Some superheroes are particularly weak against certain supervillains. If a hero has a villain specifed in their `weakness` field then they have 1 point knocked off their score when fighting that villain. This can affect the outcome of the battle.

Change the `/battle` endpoint to support this functionality.

## Acceptance tests

Run the application.

1. Should return Joker - http://localhost:5000/battle?hero=Batman&villain=Joker
2. Should return Superman - http://localhost:5000/battle?hero=Superman&villain=Lex%20Luthor

