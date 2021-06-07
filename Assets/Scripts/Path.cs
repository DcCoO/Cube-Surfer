using PathCreation;
using UnityEngine;

public class Path : MonoBehaviour, IReset
{
    [SerializeField] protected PathCreator pathCreator;
    [SerializeField] protected float length;
    [SerializeField] protected int angle;
    [SerializeField] protected Direction direction;

    [SerializeField] protected Transform begin;
    [SerializeField] protected Transform end;

    [SerializeField] IReset[] resetables;
    
    private void Start()
    {
        length = pathCreator.path.length;
    }

    public Vector3 GetBegin() => begin.position;
    public Vector3 GetEnd() => end.position;

    public void Place(Vector3 position, Direction direction)
    {
        this.direction = direction;
        if (direction == Direction.NORTH) transform.eulerAngles = Vector3.zero;
        else if (direction == Direction.EAST) transform.eulerAngles = Vector3.up * 90;
        else if (direction == Direction.SOUTH) transform.eulerAngles = Vector3.up * 180;
        else transform.eulerAngles = Vector3.up * 270;
        transform.position = position;
        length = pathCreator.path.length;
    }

    public Direction GetNextDirection() => direction.Rotate(angle);


    public Vector3 GetPosition(ref float distanceTravelled) => pathCreator.path.GetPointAtDistance(distanceTravelled);
    public Quaternion GetRotation(ref float distanceTravelled) => pathCreator.path.GetRotationAtDistance(distanceTravelled);
    public Vector3 GetDirection(ref float distanceTravelled) => pathCreator.path.GetDirectionAtDistance(distanceTravelled);
    public bool Finished(ref float distanceTravelled) => Mathf.Abs(distanceTravelled - length) < 0.01f;

    public void Reset()
    {
        gameObject.SetActive(true);
        for (int i = 0; i < resetables.Length; ++i) resetables[i].Reset();
    }
}