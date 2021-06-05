using PathCreation;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] protected PathCreator pathCreator;
    [SerializeField] protected float length;
    [SerializeField] protected int angle;
    [SerializeField] protected Direction direction;

    public Vector3 begin { get; private set; }
    public Vector3 end { get; private set; }
    
    private void Start()
    {
        begin = pathCreator.path.GetPoint(0);
        end = pathCreator.path.GetPoint(1);
        length = pathCreator.path.length;
    }

    public void Place(Vector3 position, Direction direction)
    {
        if (direction == Direction.NORTH) transform.eulerAngles = Vector3.zero;
        else if (direction == Direction.EAST) transform.eulerAngles = Vector3.up * 90;
        else if (direction == Direction.SOUTH) transform.eulerAngles = Vector3.up * 180;
        else transform.eulerAngles = Vector3.up * 270;
        transform.position = position;
    }


    public Vector3 GetPosition(ref float distanceTravelled) => pathCreator.path.GetPointAtDistance(distanceTravelled);
    public Quaternion GetRotation(ref float distanceTravelled) => pathCreator.path.GetRotationAtDistance(distanceTravelled);
    public Vector3 GetDirection(ref float distanceTravelled) => pathCreator.path.GetDirectionAtDistance(distanceTravelled);
    public bool Finished(ref float distanceTravelled) => Mathf.Abs(distanceTravelled - length) < 0.01f;

}