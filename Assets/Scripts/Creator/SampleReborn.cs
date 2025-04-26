using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class SampleReborn : MonoBehaviour
{
    GameObject[] _animals;// 動物のPrefab
    [SerializeField] private float _rebirthPosX = -400f; // 再生位置X
    [SerializeField] private float _rebirthPosY = -60f; // 再生位置Y
    [SerializeField] private float _rebirthPosZ = -20f; // 再生位置Z
    [SerializeField] private float _goalPosX = -50f; // 目標位置X
    [SerializeField] private bool _isRightSide = true; // 右向きかどうか
    private Vector3 _rebirthPos;
    private Vector3 _rebirthRotation;

    public void ManualStart()
    {
        // このオブジェクトの子要素を全て_animalsに格納
        _animals = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            _animals[i] = transform.GetChild(i).gameObject;
            _animals[i].GetComponent<Animal>().Initialize(_goalPosX);
            _animals[i].SetActive(false);
        }
        // 初期化処理
        if (_isRightSide)
        {
            _rebirthPos = new Vector3(-_rebirthPosX, _rebirthPosY, _rebirthPosZ);
            _rebirthRotation = new Vector3(0, -90, 0);
            _goalPosX *= -1;
        }
        else
        {
            _rebirthPos = new Vector3(_rebirthPosX, _rebirthPosY, _rebirthPosZ);
            _rebirthRotation = new Vector3(0, 90, 0);
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
    private void Reborn()
    {

        // ランダムに動物を選択
        int randNum = Random.Range(0, _animals.Length - 1);

        // _animalsのrandNum番目の動物を表示
        _animals[randNum].SetActive(true);
        _animals[randNum].GetComponent<Animal>().Alive(_rebirthPos);
        _animals[randNum].transform.position = _rebirthPos;
        _animals[randNum].transform.rotation = Quaternion.Euler(_rebirthRotation);
        _animals[randNum].transform.DOMoveX(_goalPosX, 5f);
    }
}