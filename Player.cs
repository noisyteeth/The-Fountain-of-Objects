
namespace Fountain
{
    public class Player
    {
        private bool _dead;
        private Coord _playerCoord;

        public bool Dead => _dead;
        public Coord PlayerCoord => _playerCoord;
        public string PlayerCoordString => $"({PlayerCoord.X}, {PlayerCoord.Y})";

        public Player()
        {
            _dead = false;
            _playerCoord = new(0, 0);
        }

        public void MovePlayer(Direction direction, Map map)
        {
            switch (direction)
            {
                case Direction.North:
                    if (PlayerCoord.Y - 1 < 0) return;
                    PlayerCoord.Y--;
                    break;
                case Direction.South:
                    if (PlayerCoord.Y + 1 > map.GridSize - 1) return;
                    PlayerCoord.Y++;
                    break;
                case Direction.East:
                    if (PlayerCoord.X + 1 > map.GridSize - 1) return;
                    PlayerCoord.X++;
                    break;
                case Direction.West:
                    if (PlayerCoord.X - 1 < 0) return;
                    PlayerCoord.X--;
                    break;
            }
        }

        public void ShowPlayerCoord()
        {
            Console.WriteLine(PlayerCoordString);
        }

        public void Death(Map map)
        {
            if (map.PlayerRoom == RoomTypes.Pit) _dead = true;
        }
    }
}
