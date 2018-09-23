using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp
{    
    public class Map
    {
        private const int MAX_SIZE = 10000;

        
        public enum Direction
        {
            North,
            East,
            South,
            West
        }


        public int SizeX { get; private set; }
        public int SizeY { get; private set; }

        public Point Start { get; private set; }
        public Map.Direction DirectionStart { get; private set; }
        public Point Exit { get; private set; }
        public List<Point> Mines { get; private set; }


        public delegate void ActionHitMine();
        public ActionHitMine TriggerHitMine;

        public delegate void ActionForward();
        public ActionForward ForwardSuccess;

        public delegate void ActionOutsideMap();
        public ActionOutsideMap TriggerOutsideMap;

        public delegate void ActionExit();
        public ActionExit TriggerExit;


        public Map (int sizeX, int sizeY, Point start, string directionStart, Point exit, List<Point> mines)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            Mines = mines;
            Exit = exit;
            Start = start;
            try
            {
                DirectionStart = (Map.Direction)Enum.Parse(typeof(Map.Direction), directionStart);
            }
            catch
            {
                throw new ArgumentException("sizeX can not be lower than 1.");
            }

            if (SizeX < 1)
                throw new ArgumentException("sizeX can not be lower than 1.");

            if (SizeY < 1)
                throw new ArgumentException("sizeY can not be lower than 1.");

            if (SizeX > MAX_SIZE)
                throw new ArgumentException($"sizeX can not be bigger than {MAX_SIZE}.");

            if (SizeY > MAX_SIZE)
                throw new ArgumentException($"sizeY can not be bigger than {MAX_SIZE}.");

            if (Mines.Count != Mines.Distinct().Count())
                throw new ArgumentException("Mines contains the same points: " + string.Join(", ", Mines.Except(Mines.Distinct())));

            if (Mines.Count(tmpP => tmpP == exit) > 0)
                throw new ArgumentException($"Exit point has the same corrdinate as a mine.");

            if (Mines.Count(tmpP => tmpP == start) > 0)
                throw new ArgumentException($"Start point has the same corrdinate as a mine.");

            // Mines outside the range
            List<Point> outsideRangeMines = Mines.Where(tmpM => !IsInRange(tmpM)).ToList();
            if (outsideRangeMines.Count > 0)
                throw new ArgumentException($"Mines out of range: " + string.Join(", ", outsideRangeMines));

            // Start point outside the range
            if (!IsInRange(start))
                throw new ArgumentException($"Start point out of range: " + start.ToString());

            // Exit point outside the range
            if (!IsInRange(exit))
                throw new ArgumentException($"Exit point out of range: " + exit.ToString());
        }



        public bool IsInRange(Point p)
        {
            if (p.X < 0 ||
                p.Y < 0 ||
                p.X >= SizeX ||
                p.Y >= SizeY)
                return false;
            return true;
        }


        public void TriggerAction(Point p)
        {
            if (!IsInRange(p))
            {
                TriggerOutsideMap();
                return;
            }

            if (Mines.Count(m => m == p) > 0)
            {
                TriggerHitMine();
                return;
            }

            if (p == Exit)
            {
                TriggerExit();
                return;
            }
            
            ForwardSuccess();
        }
    }
}
