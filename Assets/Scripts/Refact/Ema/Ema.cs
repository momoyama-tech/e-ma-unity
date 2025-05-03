using UnityEngine;

public class Ema : MonoBehaviour
{
    private int _id;
    private bool _isRight;
    private Sprite _flowerSprite;
    private Sprite _nameSprite;
    private Sprite _wishSprite;

    public void Initialize(int id, Sprite flowerSprite, Sprite nameSprite, Sprite wishSprite)
    {
        _id = id;
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