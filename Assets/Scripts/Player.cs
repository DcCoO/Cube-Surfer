using UnityEngine;

namespace CubeSurferClone
{
    public class Player : SingletonMonoBehaviour<Player>, IReset
    {
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
        bool isStopped = true;
        PathController pathController;

        public int numCollectibles { get; private set; }
        public Vector3 position => tf.position;
        public Vector3 bodyPosition => playerBody.position;

        void Start() => Reset();

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
            Quaternion pathRotation = pathController.currentPath.GetRotation(ref distanceTravelled);
            followedBody.position = pathPosition.SetY(yOrigin);
            followedBody.rotation = pathRotation;
            tf.position = (pathPosition + right * sidePosition).SetY(height);
            tf.rotation = pathRotation;
            colliderBody.position = tf.position.SetY(yOrigin);

            if (pathController.currentPath.Finished(ref distanceTravelled)) ChangePathPart();

            distanceTravelled += speed * Time.deltaTime;
        }

        public void SetStopped(bool state) => isStopped = state;

        public void Reset()
        {
            distanceTravelled = 0;
            sidePosition = 0;
            pathController = PathController.Instance;
            collectibles[0].gameObject.SetActive(true);
            tf = transform;
            tf.position = Vector3.up * yOrigin;
            tf.rotation = Quaternion.identity;
            followedBody.position = Vector3.up * yOrigin;
            followedBody.rotation = Quaternion.identity;
            screenWidth = Screen.width;
            height = yOrigin;
            numCollectibles = 1;
            for (int i = 1; i < collectibles.Length; ++i) collectibles[i].gameObject.SetActive(false);
            UpdateBody();
        }

        void UpdateBody() => playerBody.localPosition = (numCollectibles * ySize) * Vector3.up;

        public void Collect(int numParts)
        {
            if (numCollectibles == collectibles.Length) return;
            for (int i = 0; i < numParts; ++i)
            {
                collectibles[numCollectibles++].gameObject.SetActive(true);
                if (numCollectibles == collectibles.Length) break;
            }
            UpdateBody();
        }

        void ChangePathPart()
        {
            distanceTravelled = 0;
            pathController.NextPath();
        }

        public void FallInLava()
        {
            if (numCollectibles == 1) EventController.Instance.OnGameOver();
            numCollectibles = Mathf.Max(numCollectibles - 1, 0);
            if (numCollectibles == 0) return;

            AudioController.Instance.PlayLava();
            GameObject explosion = PoolController.Instance.GetExplosion();
            explosion.transform.position = collectibles[0].position;
            explosion.GetComponent<ParticleSystem>().Play();

            for (int i = numCollectibles; i < collectibles.Length; ++i)
            {
                collectibles[i].gameObject.SetActive(false);
            }
            height = (yOrigin + 1 * ySize);
            UpdateBody();
            isFalling = true;
        }

        public void BreakAtLevel(int level)
        {
            collectibles[level].gameObject.SetActive(false);
            AudioController.Instance.PlayHit();
            GameObject explosion = PoolController.Instance.GetExplosion();
            explosion.transform.position = collectibles[level].position;
            explosion.GetComponent<ParticleSystem>().Play();
        }

        public void StartHit(int obstacleHeight)
        {
            AudioController.Instance.PlayHit();

            if (obstacleHeight >= numCollectibles)
            {
                EventController.Instance.OnGameOver();
                return;
            }
            numCollectibles -= obstacleHeight;

            for (int i = 0; i < obstacleHeight; ++i)
            {
                GameObject explosion = PoolController.Instance.GetExplosion();
                explosion.transform.position = collectibles[i].position;
                explosion.GetComponent<ParticleSystem>().Play();
            }

            for (int i = numCollectibles; i < collectibles.Length; ++i)
            {
                collectibles[i].gameObject.SetActive(false);
            }
            height = (yOrigin + obstacleHeight * ySize);
            UpdateBody();
        }

        public void EndHit() => isFalling = true;
    }
}
