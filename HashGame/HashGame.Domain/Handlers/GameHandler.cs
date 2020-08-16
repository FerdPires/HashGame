using System;
using System.Collections.Generic;
using System.Linq;
using Flunt.Notifications;
using HashGame.Domain.Commands;
using HashGame.Domain.Commands.Contracts;
using HashGame.Domain.Entities;
using HashGame.Domain.Handlers.Contracts;
using HashGame.Domain.Repositories;

namespace HashGame.Domain.Handlers
{
    public class GameHandler :
        Notifiable,
        IHandler<MakeMovementCommand>
    {
        private readonly IHashGameRepository _repository;

        public GameHandler(IHashGameRepository repository)
        {
            _repository = repository;
        }
        public ICommandResult NewGame()
        {
            var newGame = new Game();

            _repository.Create(newGame);

            return new CreateGameCommandResult(true, "Partida Iniciada!", newGame.Id, newGame.first_player);
        }

        public ICommandResult Handle(MakeMovementCommand command)
        {

            var game = _repository.GetGameById(command.id);

            if (game == null)
                return new GenericCommandResult(false, "Partida não encontrada!", null);

            if (game.status_game != "Em progresso")
                return new GenericCommandResult(false, "Partida já finalizada!", game.winner_game);

            command.Validate();
            if (command.Invalid)
            {
                var message = command.Notifications.FirstOrDefault();
                return new GenericCommandResult(false, message.Message, null);
            }

            if (game.next_player != command.player)
                return new GenericCommandResult(false, "Não é o turno desse jogador!", null);

            var movements = _repository.GetAllMovesByGame(command.id);

            var validatePosition = ValidatePosition(movements, command.position);
            if (validatePosition)
                return new GenericCommandResult(false, "Jogada inválida!", null);


            var move = new Movements
                (
                    command.position.x,
                    command.position.y,
                    command.player,
                    command.id
                );

            _repository.SaveMovement(move);

            //*************************************************************

            var verifyWinner = VerifyWinner(command.id);
            game.UpdateGame
                (
                    command.player == "X" ? "O" : "X",
                    verifyWinner != "" ? "Finalizado" : "Em progresso",
                    verifyWinner
                );

            _repository.UpDateGame(game);

            if (verifyWinner != "")
            {
                return new GenericCommandResult(true, "Partida Finalizada", verifyWinner);
            }
            else
            {
                return new GenericCommandResult(true, "Jogada Realizada com sucesso!", null);
            }

        }

        public bool ValidatePosition(IDictionary<string, IList<Position>> movements, Position position_player)
        {
            foreach (var position in movements)
            {
                if (position.Value.Any(p => p.x == position_player.x && p.y == position_player.y))
                {
                    return true;
                }
            }
            return false;
        }

        private string VerifyWinner(Guid Id)
        {
            var movements = _repository.GetAllMovesByGame(Id);

            IList<Position> player_X = movements["X"];
            IList<Position> player_O = movements["O"];
            if (VerifyPlayerWinner(player_X))
            {
                return "X";
            }
            if (VerifyPlayerWinner(player_O))
            {
                return "O";
            }
            return movements.Sum(x => x.Value.Count) < 9 ? "" : "Draw";
        }

        private bool VerifyPlayerWinner(IList<Position> position)
        {
            return VerifyLine(position) ||
                   VerifyColumn(position) ||
                   VerifyDiagonal(position);
        }

        private static bool VerifyLine(IList<Position> position)
        {
            for (int y = 0; y < 3; y++)
            {
                bool line = false;
                for (int x = 0; x < 3; x++)
                {
                    line = position.Any(p => p.x == x && p.y == y);
                    if (!line)
                    {
                        break;
                    }
                }
                if (line)
                {
                    return line;
                }
            }
            return false;
        }

        private static bool VerifyColumn(IList<Position> position)
        {
            for (int x = 0; x < 3; x++)
            {
                bool column = false;
                for (int y = 0; y < 3; y++)
                {
                    column = position.Any(p => p.x == x && p.y == y);
                    if (!column)
                    {
                        break;
                    }
                }
                if (column)
                {
                    return column;
                }
            }
            return false;
        }

        private static bool VerifyDiagonal(IList<Position> position)
        {
            int diagonalA = 0;
            int diagonalB = 0;
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if ((x + y) % 2 == 0)
                    {
                        if (x == y && position.Any(p => p.x == x && p.y == y))
                        {
                            diagonalA++;
                        }
                        if ((x != y || (x == 1 && y == 1)) && position.Any(p => p.x == x && p.y == y))
                        {
                            diagonalB++;
                        }
                    }
                }
            }
            return diagonalA == 3 || diagonalB == 3;
        }
    }
}