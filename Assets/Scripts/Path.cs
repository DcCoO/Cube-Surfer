using PathCreation;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] protected PathCreator pathCreator;
    public Vector3 begin { get; private set; }
    public Vector3 end { get; private set; }
    [SerializeField] protected float length;
    [SerializeField] protected Direction direction;

    private void Start()
    {
        begin = pathCreator.path.GetPoint(0);
        end = pathCreator.path.GetPoint(1);
        length = pathCreator.path.length;
    }

    public Vector3 GetPosition(ref float distanceTravelled) => pathCreator.path.GetPointAtDistance(distanceTravelled);
    public Quaternion GetRotation(ref float distanceTravelled) => pathCreator.path.GetRotationAtDistance(distanceTravelled);
    public Vector3 GetDirection(ref float distanceTravelled) => pathCreator.path.GetDirectionAtDistance(distanceTravelled);
    public bool Finished(ref float distanceTravelled) => Mathf.Abs(distanceTravelled - length) < 0.01f;

}