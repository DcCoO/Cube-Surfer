using UnityEngine;

public class ObstacleLine : MonoBehaviour
{
    [SerializeField] Transform[] tracks;
    [SerializeField] int[] height;

    private void OnTriggerEnter(Collider other) => HitPlayer();
    private void OnTriggerExit(Collider other) => Player.Instance.EndHit();
    
    void HitPlayer()
    {
        Vector3 playerPos = Player.Instance.position.Ground();

        float[] dist = new float[5] { float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue } ;
        for (int i = 0; i < 5; ++i) if(tracks[i] != null) dist[i] = Vector3.Distance(tracks[i].position.Ground(), playerPos);
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
