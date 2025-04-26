using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class SampleReborn : MonoBehaviour
{
    [SerializeField] GameObject[] _animals;// 動物のPrefab
    [SerializeField] private float _rebirthPosX = -400f; // 再生位置X
    [SerializeField] private float _rebirthPosY = -60f; // 再生位置Y
    [SerializeField] private float _rebirthPosZ = -20f; // 再生位置Z
    [SerializeField] private float _goalPosX = -50f; // 目標位置X
    [SerializeField] private bool _isRightSide = true; // 右向きかどうか
    private Vector3 _rebirthPos;
    private Vector3 _rebirthRotation;

    public void ManualStart()
    {
        foreach(var animal in _animals)
        {
            Instantiate(animal);
            animal.SetActive(false);
        }
        // 初期化処理
        if(_isRightSide)
        {
            _rebirthPos = new Vector3(_rebirthPosX, _rebirthPosY, _rebirthPosZ);
            _rebirthRotation = new Vector3(0, 180, 0);
        }
        else
        {
            _rebirthPos = new Vector3(-_rebirthPosX, _rebirthPosY, _rebirthPosZ);
            _rebirthRotation = new Vector3(0, 0, 0);
        }
    }

    public void ManualUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reborn();
            Debug.Log("Reborn");
        }
    }

    private GameObject SelectAnimal(int randNum)
    {
        if(randNum < 0 || randNum >= _animals.Length)
        {
            Debug.LogError("Error: randNum is out of range.");
            return null;
        }
        return _animals[randNum];
    }

    private void Reborn()
    {
        // ランダムに動物を選択
        int randNum = Random.Range(0, _animals.Length);
        GameObject selectedAnimal = SelectAnimal(randNum);

        if (selectedAnimal != null)
        {
            selectedAnimal.SetActive(true);
            selectedAnimal.transform.position = _rebirthPos;
            selectedAnimal.transform.rotation = Quaternion.Euler(_rebirthRotation);
            selectedAnimal.transform.DOMove(new Vector3(_goalPosX, _rebirthPosY, _rebirthPosZ), 1f)
                .SetEase(Ease.Linear);
        }
    }
}