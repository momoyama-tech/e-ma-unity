using UnityEngine;

public class thunder : MonoBehaviour
{
    public ParticleSystem lightningParticleSystem;

    public float positionYValue = 0f; // 出現位置Y
    public float targetYValue = 0f; // 目的地Y
    public float targetZValue = 0f; // 目的地Z
    public float randomRange = 10f; // 放出先のX軸のランダム範囲
    public float constantYValue = 0f;

    

    void Start()
    {
        // Particle Systemを開始
        lightningParticleSystem.Play();

        // ランダムな放出点を設定
        Vector3 randomPosition = new Vector3(Random.Range(-10,10), constantYValue, transform.position.z);
        lightningParticleSystem.transform.position = randomPosition;

        // 放出先の設定
        Vector3 targetPosition;
        // 放出点のX値を基に放出先のX値をランダムに設定
        float randomXForTarget = randomPosition.x + Random.Range(-randomRange, randomRange);
        targetPosition = new Vector3(randomXForTarget, targetYValue, targetZValue); 

        // 放出先の方向を設定
        Vector3 direction = (targetPosition - randomPosition).normalized;
        lightningParticleSystem.transform.rotation = Quaternion.LookRotation(direction);
    }
}