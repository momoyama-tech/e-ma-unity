using UnityEngine;

public class thunder : MonoBehaviour
{
    public ParticleSystem lightningParticleSystem;

    public float positionYValue = 0f; // �o���ʒuY
    public float targetYValue = 0f; // �ړI�nY
    public float targetZValue = 0f; // �ړI�nZ
    public float randomRange = 10f; // ���o���X���̃����_���͈�
    public float constantYValue = 0f;

    

    void Start()
    {
        // Particle System���J�n
        lightningParticleSystem.Play();

        // �����_���ȕ��o�_��ݒ�
        Vector3 randomPosition = new Vector3(Random.Range(-10,10), constantYValue, transform.position.z);
        lightningParticleSystem.transform.position = randomPosition;

        // ���o��̐ݒ�
        Vector3 targetPosition;
        // ���o�_��X�l����ɕ��o���X�l�������_���ɐݒ�
        float randomXForTarget = randomPosition.x + Random.Range(-randomRange, randomRange);
        targetPosition = new Vector3(randomXForTarget, targetYValue, targetZValue); 

        // ���o��̕�����ݒ�
        Vector3 direction = (targetPosition - randomPosition).normalized;
        lightningParticleSystem.transform.rotation = Quaternion.LookRotation(direction);
    }
}