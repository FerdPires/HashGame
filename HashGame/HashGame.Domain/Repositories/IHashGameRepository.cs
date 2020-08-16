using System;
using System.Collections.Generic;
using HashGame.Domain.Entities;

namespace HashGame.Domain.Repositories
{
    public interface IHashGameRepository
    {
        void Create(Game game);
        void UpDateGame(Game game);
        void SaveMovement(Movements move);
        Game GetGameById(Guid id);
        IDictionary<string, IList<Position>> GetAllMovesByGame(Guid Id);
    }
}