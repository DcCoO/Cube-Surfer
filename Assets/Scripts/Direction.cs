namespace CubeSurferClone
{
    public enum Direction
    {
        NORTH = 0, EAST = 1, SOUTH = 2, WEST = 3
    }

    public static class DirectionMethods
    {
        public static Direction Rotate(this Direction direction, int angle)
        {
            if (angle == 0) return direction;
            int directionValue = (int)direction + (angle > 0 ? 1 : -1);
            if (directionValue < 0) directionValue = 3;
            else if (directionValue > 3) directionValue = 0;
            return (Direction)directionValue;
        }
    }
}