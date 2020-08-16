using System;
using System.Linq.Expressions;
using HashGame.Domain.Entities;

namespace HashGame.Domain.Queries
{
    public static class GameQueries
    {
        public static Expression<Func<Movements, bool>> GetAllMovesByGame(Guid Id)
        {
            return x => x.id_game == Id;
        }
    }
}