using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float distanceTravelled;

    PathController pathController;
    void Start()
    {
        pathController = PathController.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 right = Vector3.Cross(Vector3.up, pathController.currentPath.GetDirection(ref distanceTravelled));
        Debug.DrawRay(transform.position, right * 4);
        transform.position = pathController.currentPath.GetPosition(ref distanceTravelled);
        transform.rotation = pathController.currentPath.GetRotation(ref distanceTravelled);
        if(pathController.currentPath.Finished(ref distanceTravelled))
        {
            ChangePart();
        }
        distanceTravelled += speed * Time.deltaTime;
    }

    void ChangePart()
    {
        distanceTravelled = 0;
        pathController.NextPath();
    }
}
