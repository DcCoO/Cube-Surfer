using UnityEngine;

public class Player : SingletonMonoBehaviour<Player>
{
    [SerializeField] protected int numCollectibles;

    [Header("References")]
    [SerializeField] protected Transform playerBody;
    [SerializeField] protected Transform colliderBody;
    [SerializeField] protected Transform followedBody;
    [SerializeField] protected Transform[] collectibles;

    [Header("Physics")]
    [SerializeField] protected float speed;
    [SerializeField] float height;
    [SerializeField] float ySize;
    [SerializeField] float yOrigin;
    [SerializeField] float mouseInfluence;
    [SerializeField] float gravity;

    float distanceTravelled;
    Transform tf;
    float sidePosition;
    float screenWidth;
    float mousePos;
    bool isFalling;
    bool isStopped;
    PathController pathController;

    public Vector3 position => tf.position;

    void Start() => Reset();
    

    public void Reset()
    {
        pathController = PathController.Instance;
        tf = transform;
        tf.position = Vector3.up * yOrigin;
        screenWidth = Screen.width;
        height = yOrigin;
        numCollectibles = 1;
        for (int i = 1; i < collectibles.Length; ++i) collectibles[i].gameObject.SetActive(false);
        UpdateBody();
    }

    void Update()
    {
        if (isStopped) return;

        Vector3 right = Vector3.Cross(Vector3.up, pathController.currentPath.GetDirection(ref distanceTravelled));

        if (Input.GetMouseButtonDown(0)) mousePos = Input.mousePosition.x;
        else if (Input.GetMouseButton(0))
        {
            float delta = Input.mousePosition.x - mousePos;
            float percent = delta / screenWidth;
            sidePosition = Mathf.Clamp(sidePosition + percent * mouseInfluence, -0.4f, 0.4f);
            mousePos = Input.mousePosition.x;
        }

        if (isFalling)
        {
            height -= Time.deltaTime * gravity;
            if (height <= yOrigin)
            {
                isFalling = false;
                height = yOrigin;
            }
        }

        Vector3 pathPosition = pathController.currentPath.GetPosition(ref distanceTravelled);
        followedBody.position = pathPosition.SetY(yOrigin);
        tf.position = (pathPosition + right * sidePosition).SetY(height);
        tf.rotation = pathController.currentPath.GetRotation(ref distanceTravelled);
        colliderBody.position = tf.position.SetY(yOrigin);

        if(pathController.currentPath.Finished(ref distanceTravelled)) ChangePart();
        
        distanceTravelled += speed * Time.deltaTime;
    }

    public void StartGame() => isStopped = false;

    void UpdateBody() => playerBody.localPosition = (numCollectibles * ySize) * Vector3.up;

    public void Collect(int numParts)
    {
        for (int i = 0; i < numParts; ++i) collectibles[numCollectibles++].gameObject.SetActive(true);
        UpdateBody();
    }

    void ChangePart()
    {
        distanceTravelled = 0;
        pathController.NextPath();
    }

    public void StartHit(int obstacleHeight)
    {
        if(obstacleHeight >= numCollectibles)
        {
            EventController.Instance.OnGameOver();
            return;
        }
        numCollectibles -= obstacleHeight;
        for (int i = numCollectibles; i < collectibles.Length; ++i) collectibles[i].gameObject.SetActive(false);
        height = (yOrigin + obstacleHeight * ySize);
        UpdateBody();
    }

    public void EndHit() => isFalling = true;    
}
