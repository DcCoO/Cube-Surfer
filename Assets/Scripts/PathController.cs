namespace CubeSurferClone
{
    public class PathController : SingletonMonoBehaviour<PathController>, IReset
    {
        public Path currentPath;
        public int currentPathIndex;
        public Path[] paths;

        public void Reset()
        {
            currentPathIndex = 0;
            currentPath = paths[0];
        }

        public void SetPath(Path[] paths)
        {
            this.paths = paths;
            currentPath = paths[0];
            currentPathIndex = 0;
        }

        public void NextPath()
        {
            currentPathIndex = (currentPathIndex + 1) % paths.Length;
            currentPath = paths[currentPathIndex];
        }
    }
}
