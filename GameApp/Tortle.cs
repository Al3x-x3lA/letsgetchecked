using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp
{
    public class Tortle
    {
        public enum Move
        {
            m,  // Move forward
            r   // Rotate 90 degrees to the right
        }


        public Map.Direction[] DirectionInOrder { get; private set; }


        private Point _Position;
        public Point Position
        {
            get
            {
                return _Position;
            }
        }
        public Map.Direction Facing { get; private set; }


        public int HealthPoint { get; private set; }

        

        public Tortle(Point startPosition, Map.Direction facing)
        {
            _Position = startPosition;
            Facing = facing;

            DirectionInOrder = new[] {
                Map.Direction.North,
                Map.Direction.East,
                Map.Direction.South,
                Map.Direction.West
            };

            HealthPoint = 1;
        }


        public bool IsAlive()
        {
            return HealthPoint > 0;
        }


        public void InstantDead()
        {
            HealthPoint = 0;
        }


        public void Rotate()
        {
            Facing = DirectionInOrder[(Array.FindIndex(DirectionInOrder, a => a == Facing) + 1) % 4];
        }



        public void Forward()
        {
            switch (Facing)
            {
                case Map.Direction.East:
                    _Position.X += 1;
                    break;
                case Map.Direction.South:
                    _Position.Y += 1;
                    break;
                case Map.Direction.West:
                    _Position.X += -1;
                    break;
                case Map.Direction.North:
                    _Position.Y += -1;
                    break;
            }
        }

    }
}
