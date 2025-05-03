using UnityEngine;

public class Ema : MonoBehaviour
{
    private int _id;// EmaのID
    private bool _isRight; // Emaがどちら側で回転しているか
    private Sprite _flowerSprite;// Emaの花のスプライト
    private Sprite _nameSprite;// Emaの名前のスプライト
    private Sprite _wishSprite;// Emaの願いのスプライト
    public void Initialize(int id, Sprite flowerSprite, Sprite nameSprite, Sprite wishSprite)
    {
        _id = id;
        // EmaのIDが偶数なら右側、奇数なら左側
        if (id % 2 == 0)
        {
            _isRight = true;
        }
        else
        {
            _isRight = false;
        }
        _flowerSprite = flowerSprite;
        _nameSprite = nameSprite;
        _wishSprite = wishSprite;
    }

    public int GetId()
    {
        return _id;
    }
    public bool GetIsRight()
    {
        return _isRight;
    }
    public Sprite GetFlowerSprite()
    {
        return _flowerSprite;
    }
    public Sprite GetNameSprite()
    {
        return _nameSprite;
    }
    public Sprite GetWishSprite()
    {
        return _wishSprite;
    }
    public void SetIsRight(bool isRight)
    {
        _isRight = isRight;
    }
}