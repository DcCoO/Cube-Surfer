using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] GameObject[] parts;
    [SerializeField] Level[] levels;
    int currentPart;
    Transform tf;
    // Start is called before the first frame update
    void Start()
    {
        tf = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) LoadLevel();
    }

    public void LoadLevel(int levelIndex = 0)
    {
        currentPart = 0;
        Path[] paths = new Path[levels[levelIndex].parts.Length];
        Level level = levels[levelIndex];
        Vector3 position = Vector3.zero;
        Direction direction = Direction.NORTH;
        for(int i = 0, len = level.parts.Length; i < len; ++i)
        {
            paths[i] = Instantiate(parts[level.parts[i]], tf).GetComponent<Path>();
            paths[i].Place(position, direction);
            direction = paths[i].GetNextDirection();
            position = paths[i].GetEnd();
        }
        PathController.Instance.SetPath(paths);
    }
}
