using UnityEngine;

public class CreatureCreator : MonoBehaviour
{
    [SerializeField] private GameObject _creaturePrefab;
    [SerializeField] private GameObject _creatureParent;

    void Start()
    {
        // CreateCreature();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            CreateCreature();
        }
    }

    private void CreateCreature()
    {
        GameObject creature = Instantiate(_creaturePrefab, _creatureParent.transform);
    }
}