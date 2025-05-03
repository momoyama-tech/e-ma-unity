using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EmaCreator : MonoBehaviour
{
    private SpriteChanger _spriteChanger;
    [SerializeField] private GameObject _emaPrefab;

    public void ManualStart()
    {
        _spriteChanger = GetComponent<SpriteChanger>();
    }
    public async UniTask CreateEma(string flowerUrl, string nameUrl, string wishUrl)
    {
        var ema = Instantiate(_emaPrefab, gameObject.transform);
        await _spriteChanger.Initialize(flowerUrl, nameUrl, wishUrl);
        Debug.Log("フラワースプライトゲット" + _spriteChanger.GetFlowerSprite());
        Debug.Log("ネームスプライトゲット" + _spriteChanger.GetNameSprite());
        Debug.Log("ウィッシュスプライトゲット" + _spriteChanger.GetWishSprite());
        ema.GetComponent<SpriteRenderer>().sprite = _spriteChanger.GetFlowerSprite();
        ema.transform.Find("Name").GetComponent<SpriteRenderer>().sprite = _spriteChanger.GetNameSprite();
        ema.transform.Find("Wish").GetComponent<SpriteRenderer>().sprite = _spriteChanger.GetWishSprite();
    }
}