using UnityEngine;

public class PathController : SingletonMonoBehaviour<PathController>
{
    public Path currentPath;
    int currentPathIndex;
    public Path[] paths;
    

    public void NextPath()
    {
        currentPathIndex = (currentPathIndex + 1) % paths.Length;
        currentPath = paths[currentPathIndex];
    }
}
