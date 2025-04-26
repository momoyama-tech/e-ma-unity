using UnityEngine;

public class Animal : MonoBehaviour
{
    private float _goalPosX;
    [SerializeField] private GameObject _destroyEffectPrefab;

    public void Initialize(float goalPos)
    {
        _goalPosX = goalPos;
        _destroyEffectPrefab.SetActive(false);
        gameObject.SetActive(false);
    }

    private void Died()
    {
        // ここに死亡時の処理を追加
        Debug.Log("Animal Died");
        // _destroyEffectPrefab にアタッチされているスクリプトを取得して Initialize を呼び出す
        _destroyEffectPrefab.GetComponent<Disappearance>().Initialize(gameObject.transform.position);
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
}