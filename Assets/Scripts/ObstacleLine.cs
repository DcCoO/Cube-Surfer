using UnityEngine;

public class ObstacleLine : MonoBehaviour
{
    [SerializeField] int[] height;
    [SerializeField] Transform left, right;

    private void OnTriggerEnter(Collider other) => HitPlayer();
    private void OnTriggerExit(Collider other) => Player.Instance.EndHit();
    
    void HitPlayer()
    {
        Vector3 playerPos = Player.Instance.position.Ground();
        Vector3 diff = right.position - left.position;
        float[] dist = new float[5];
        float rate = 0;
        for (int i = 0; i < 5; ++i) {
            dist[i] = Vector3.Distance((left.position + diff * rate).Ground(), playerPos);
            rate += 0.2f;
        }
        int min1 = (dist[0] < dist[1] ? 0 : 1);
        int min2 = 1 - min1;
        for (int i = 2; i < 5; ++i)
        {
            if (dist[i] < dist[min2]) min2 = i;
            if(dist[min2] < dist[min1])
            {
                int aux = min2;
                min2 = min1;
                min1 = aux;
            }
        }
        Player.Instance.StartHit(Mathf.Max(height[min1], height[min2]));
    }
}