using System.Collections.Generic;
using System.Linq;
using HashGame.Domain.Entities;
using HashGame.Infra.Contexts;
using HashGame.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using HashGame.Domain.Queries;

namespace HashGame.Infra.Repositories
{
    public class HashGameRepository : IHashGameRepository
    {
        private readonly DataContext _context;

        public HashGameRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(Game game)
        {
            _context.Games.Add(game);
            _context.SaveChanges();
        }

        public IDictionary<string, IList<Position>> GetAllMovesByGame(Guid Id)
        {
            IDictionary<string, IList<Position>> moves = new Dictionary<string, IList<Position>>();

            //  var result = _context.Movements.AsNoTracking().Where(x => x.id_game == Id);
            var result = _context.Movements.AsNoTracking().Where(GameQueries.GetAllMovesByGame(Id));

            var resultX = result.Where(x => x.player == "X").ToList();
            IList<Position> listX = new List<Position>();
            foreach (var x in resultX)
            {
                listX.Add(new Position(x.pos_x, x.pos_y));
            }
            moves.Add("X", listX);

            var resultO = result.Where(o => o.player == "O").ToList();
            IList<Position> listO = new List<Position>();
            foreach (var o in resultO)
            {
                listO.Add(new Position(o.pos_x, o.pos_y));
            }
            moves.Add("O", listO);

            return moves;
        }

        public Game GetGameById(Guid id)
        {
            return _context.Games.FirstOrDefault(x => x.Id == id);
        }

        public void SaveMovement(Movements move)
        {
            _context.Movements.Add(move);
            _context.SaveChanges();
        }

        public void UpDateGame(Game game)
        {
            _context.Entry(game).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}