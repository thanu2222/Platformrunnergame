using UnityEngine;

public class EnemyFireballHolder : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    // Update is called once per frame
    private void Update()
    {
        // transforming enemy scale 
        transform.localScale = enemy.localScale;
    }
}