using UnityEngine;

public class SpawnerOfItems : MonoBehaviour
{

    public GameObject[] PrefabsOfItem;
    public GameObject[] SpawnPlacesOfItems;

    [Space]
    [Range(3, 15)]
    [SerializeField] private int minimumItemsInMap = 3;
    [Range(3, 15)]
    [SerializeField] private int maximumItemsInMap = 5;


    void Start()
    {
        SpawnItems();
    }


    private void SpawnItems()
    {
        int rndAmountOfItems = RandomItemsAmount();


        for (int i = 0; i <= rndAmountOfItems; i++)
        {
            int rndItemOfTheArrayItems = Random.Range(0, PrefabsOfItem.Length - 1);

            var item = Instantiate(PrefabsOfItem[rndItemOfTheArrayItems], SpawnPlacesOfItems[i].transform.position, Quaternion.identity);
            item.transform.SetParent(transform);
        }
    }


    private int RandomItemsAmount()
    {
        int RND_AMOUNT_OF_ITEMS;


        if (maximumItemsInMap < SpawnPlacesOfItems.Length)
            RND_AMOUNT_OF_ITEMS = Random.Range(minimumItemsInMap, maximumItemsInMap);
        else
            RND_AMOUNT_OF_ITEMS = Random.Range(minimumItemsInMap, SpawnPlacesOfItems.Length);


        return RND_AMOUNT_OF_ITEMS;
    }
}
