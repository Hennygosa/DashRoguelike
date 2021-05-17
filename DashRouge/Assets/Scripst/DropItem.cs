using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public GameObject item;
    public int chance;

    public DropItem(GameObject _item, int _chance)
    {
        item = _item;
        chance = _chance;
    }

    public void CreateDropItem(Vector3 position)
    {
        //var drop_item = Instantiate(item) as GameObject;
        Instantiate(item, position, Quaternion.identity);
        //item. transform.position = position;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
