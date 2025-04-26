using UnityEngine;

public class Animal : MonoBehaviour
{
    private float _goalPosX;
    public void Initialize(float goalPos)
    {
        _goalPosX = goalPos;
        gameObject.SetActive(false);
    }

    public void ManualUpdate()
    {
        if (Mathf.Abs(gameObject.transform.position.x) <= Mathf.Abs(_goalPosX) - 10f)
        {
            Died();
        }
    }

    private void Died()
    {
        gameObject.SetActive(false);
        // ここに死亡時の処理を追加
        Debug.Log("Animal Died");
    }
}