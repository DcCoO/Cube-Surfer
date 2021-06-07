using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class LevelBuilder : SingletonMonoBehaviour<LevelBuilder>, IReset
{
    [SerializeField] GameObject[] parts;
    [SerializeField] Level[] levels;
    [SerializeField] List<IReset> resetables = new List<IReset>();

    public void LoadLevel(int levelIndex = 0)
    {
        resetables.Clear();
        Transform tf = transform;
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

    public void Reset()
    {
        resetables.ForEach(x => x.Reset());
        StartCoroutine(MovePlayer());
    }

    IEnumerator MovePlayer()
    {
        yield return new WaitForSeconds(0.2f);
        Player.Instance.SetStopped(false);
    }

    public void AddResetable(IReset resetable) => resetables.Add(resetable);

}
