using System;
using Flunt.Notifications;
using Flunt.Validations;
using HashGame.Domain.Commands.Contracts;
using HashGame.Domain.Entities;

namespace HashGame.Domain.Commands
{
    public class MakeMovementCommand : Notifiable, ICommand
    {
        public MakeMovementCommand() { }

        public MakeMovementCommand(string Player, Position positionPlayer, Guid Id)
        {
            player = Player;
            position = positionPlayer;
            id = Id;
        }

        public string player { get; set; }
        public Position position { get; set; }
        public Guid id { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsNotEmpty(id, "Id do Jogo", "O Id do Jogo não pode ser vazio!")
                    .IsNotNullOrEmpty(player, "Jogador", "O jogador não pode ser vazio!")
                    .IsNotNull(position.x, "Coordenada X", "A coordenada X não pode ser vazia!")
                    .IsNotNull(position.y, "Coordenada Y", "A coordenada Y não pode ser vazia!")
                    .IsLowerOrEqualsThan(position.x, 2, "Coordenada X", "A coordenada X não pode ser maior do que 2!")
                    .IsLowerOrEqualsThan(position.y, 2, "Coordenada Y", "A coordenada Y não pode ser maior do que 2!")
            );

            if (player != "X" && player != "O")
                AddNotification("Jogador", "O jogador deve ser X ou O!");
        }

    }
}