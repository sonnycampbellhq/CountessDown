using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shopItemController : MonoBehaviour
{
    [SerializeField]
    int itemType=0;
    [SerializeField]
    int price=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getItemType()
    {
        return itemType;
    }

    public int getItemPrice()
    {
        return price;
    }

    public (int,int) getItemDetails()
    {
        return (itemType, price);
    }
}
