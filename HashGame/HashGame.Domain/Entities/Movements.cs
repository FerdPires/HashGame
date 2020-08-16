using System;

namespace HashGame.Domain.Entities
{
    public class Movements : Entity
    {
        public Movements()
        {

        }

        public Movements(int posX, int posY, string Player, Guid idGame)
        {
            pos_x = posX;
            pos_y = posY;
            player = Player;
            id_game = idGame;
        }

        public int pos_x { get; private set; }
        public int pos_y { get; private set; }
        public string player { get; private set; }
        public Guid id_game { get; private set; }
    }
}