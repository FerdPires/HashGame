using System;

namespace HashGame.Domain.Entities
{
    public class Game : Entity
    {
        public Game()
        {
            first_player = SetFirstPlayer();
            next_player = first_player;
            status_game = "Em progresso";
        }

        public string first_player { get; private set; }
        public string next_player { get; private set; }
        public string status_game { get; private set; }
        public string winner_game { get; private set; }

        public void UpdateGame(string nextPlayer, string statusGame, string winnerGame)
        {
            next_player = nextPlayer;
            status_game = statusGame;
            winner_game = winnerGame;
        }

        private string SetFirstPlayer()
        {
            Random random = new Random();
            string[] players = { "X", "O" };
            return players[random.Next(players.Length)];
        }
    }
}