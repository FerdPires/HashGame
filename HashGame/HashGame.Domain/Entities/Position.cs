namespace HashGame.Domain.Entities
{
    public class Position
    {
        public Position()
        {

        }

        public Position(int posX, int posY)
        {
            x = posX;
            y = posY;
        }

        public int x { get; set; }
        public int y { get; set; }
    }
}