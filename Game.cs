
namespace Fountain
{
    public class Game
    {
        private bool _win;
        private bool _fountainEnabled;
        private bool _pitIsNear;
        private Map _map;
        private Player _player;

        public bool Win => _win;
        public bool FountainEnabled => _fountainEnabled;
        public bool PitIsNear => _pitIsNear;

        public Game(Map map, Player player)
        {
            _map = map;
            _player = player;
            _win = false;
            _fountainEnabled = false;
        }

        public void CheckAdjacentPit()
        {
            for (int x = 0; x < _map.GridSize; x++)
            {
                for (int y = 0; y < _map.GridSize; y++)
                {
                    if (Coord.IsAdjacent(_player.PlayerCoord, x, y))
                    {
                        if (_map.Rooms[x, y] == RoomTypes.Pit)
                        {
                            if (Coord.SameCoord(_player.PlayerCoord, x, y))
                            {
                                _pitIsNear = false;
                                return;
                            }

                            _pitIsNear = true;
                            return;
                        }
                    }
                }
            }

            _pitIsNear = false;
        }

        public void EnableFountain()
        {
            if (_map.PlayerRoom == RoomTypes.FountainRoom && !_fountainEnabled) 
                _fountainEnabled = true;
        } 

        public void Prompts()
        {
            if (_map.PlayerRoom == RoomTypes.Entrance && !_win)
                Console.WriteLine("You see light coming from the cavern entrance.");

            if (_map.PlayerRoom == RoomTypes.FountainRoom)
            {
                if (_fountainEnabled)
                    Console.WriteLine("You hear the rushing waters from the Fountain of Objects. It has been reactivated!");
                else
                    Console.WriteLine("You hear water dripping in this room. The Fountain of Objects is here!");
            }

            if (_pitIsNear && !_win && !_player.Dead)
                Console.WriteLine("You feel a draft. There is a pit in a nearby room.");

            if (_player.Dead)
                Console.WriteLine("You have fallen into a pit and DIED.");

            if (_win)
            {
                Console.WriteLine("You have enabled the Fountain of Objects and escaped with your life.");
                Console.WriteLine("You win!");
            }
        }

        public void ShowRoomsStatus()
        {
            Console.WriteLine();
            Console.Write($"Current Room: {_map.PlayerRoom}");

            for (int y = 0; y < _map.GridSize; y++)
            {

                Console.WriteLine();

                for (int x = 0; x < _map.GridSize; x++)
                {
                    if (_map.PlayerLoc[x, y] == true) Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(String.Format("({0},{1})", x, y));
                    Console.ResetColor();
                    Console.Write(" | ");
                }
            }

            Console.WriteLine();
            Console.WriteLine($"Win = {_win}");
            Console.WriteLine($"Fountain Enabled = {_fountainEnabled}");
            Console.WriteLine($"Pit Is Near = {_pitIsNear}");
        }

        public void CheckWin()
        {
            if (_map.PlayerRoom == RoomTypes.Entrance && _fountainEnabled)
                _win = true;
        }
    }
}
