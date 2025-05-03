using UnityEngine;

public class Animal : MonoBehaviour
{
    private float _goalPosX;
    [SerializeField] private GameObject _destroyEffectPrefab;
    [SerializeField] private float _speed = 5.0f; // 移動速度

    public void Initialize(float goalPos)
    {
        _goalPosX = goalPos;
        _destroyEffectPrefab.SetActive(false);
        gameObject.SetActive(false);
    }

    public void Alive(Vector3 pos)
    {
        _destroyEffectPrefab.GetComponent<Disappearance>().Initialize(new Vector3(pos.x, pos.y + 15.0f, pos.z));
    }

    private void Died()
    {
        // ここに死亡時の処理を追加
        // Debug.Log("Animal Died");
        // _destroyEffectPrefab にアタッチされているスクリプトを取得して Initialize を呼び出す
        _destroyEffectPrefab.GetComponent<Disappearance>().Initialize(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 15.0f, gameObject.transform.position.z));
        gameObject.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Destroy"))
        {
            Debugger.Log("Animal collided with Destroy");
            Died();
        }
    }

    public float GetSpeed()
    {
        return _speed;
    }
}