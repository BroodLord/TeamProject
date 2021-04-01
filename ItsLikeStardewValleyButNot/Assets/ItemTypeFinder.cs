using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemTypeFinder : MonoBehaviour
{
    public ItemBase TyepFinder(int i, GameObject childObject)
    {
        XMLParser XML = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
        ItemBase BasicItem = new ItemBase();
        if (XML.items.ElementAt(i).Value.bName == "Hoe") { BasicItem = childObject.AddComponent<HoeScript>() as HoeScript; }
        else if (XML.items.ElementAt(i).Value.bName == "Axe") { BasicItem = childObject.AddComponent<AxeScript>() as AxeScript; }
        else if (XML.items.ElementAt(i).Value.bName == "Pickaxe") { BasicItem = childObject.AddComponent<PickaxeScript>() as PickaxeScript; }
        else if (XML.items.ElementAt(i).Value.bName == "Water Bucket") { BasicItem = childObject.gameObject.AddComponent<WateringCanScript>() as WateringCanScript; }
        else if (XML.items.ElementAt(i).Value.bName == "Scythe") { BasicItem = childObject.gameObject.AddComponent<ScytheTool>() as ScytheTool; }
        else if (XML.items.ElementAt(i).Value.bName == "Fishing Rod") { BasicItem = childObject.gameObject.AddComponent<FishingRodScript>() as FishingRodScript; }
        else if (XML.items.ElementAt(i).Value.bItemType == DefaultItemBase.ItemTypes.Seed) { BasicItem = childObject.gameObject.AddComponent<PlantSeed>() as PlantSeed; }
        else if (XML.items.ElementAt(i).Value.bItemType == DefaultItemBase.ItemTypes.Ore) { BasicItem = childObject.gameObject.AddComponent<Ore>() as Ore; }
        else { BasicItem = childObject.gameObject.AddComponent<ItemBase>() as ItemBase; }

        return BasicItem;

    }

    public int FindItemIndex(string Name)
    {
        XMLParser XML = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
        for (int i = 0; i < XML.items.Count; i++)
        {
            if (Name == XML.items.ElementAt(i).Value.bName)
            {
                return i;
            }
        }
        return 0;

    }

    public ItemBase ItemByTyepFinder(string Name, string TypeName, GameObject childObject)
    {
        XMLParser XML = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
        ItemBase BasicItem = new ItemBase();
        if (Name == "Hoe") { BasicItem = childObject.AddComponent<HoeScript>() as HoeScript; }
        else if (Name == "Axe") { BasicItem = childObject.AddComponent<AxeScript>() as AxeScript; }
        else if (Name == "Pickaxe") { BasicItem = childObject.AddComponent<PickaxeScript>() as PickaxeScript; }
        else if (Name == "Water Bucket") { BasicItem = childObject.gameObject.AddComponent<WateringCanScript>() as WateringCanScript; }
        else if (Name == "Scythe") { BasicItem = childObject.gameObject.AddComponent<ScytheTool>() as ScytheTool; }
        else if (Name == "Fishing Rod") { BasicItem = childObject.gameObject.AddComponent<FishingRodScript>() as FishingRodScript; }
        else if (TypeName == "Seed") { BasicItem = childObject.gameObject.AddComponent<PlantSeed>() as PlantSeed; }
        else if (TypeName == "Ore") { BasicItem = childObject.gameObject.AddComponent<Ore>() as Ore; }
        else { BasicItem = childObject.gameObject.AddComponent<ItemBase>() as ItemBase; }

        return BasicItem;

    }

    public ItemBase ItemTyepFinder(ItemBase Item, GameObject childObject)
    {
        XMLParser XML = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<XMLParser>();
        ItemBase BasicItem = new ItemBase();
        if (Item.bName == "Hoe") { BasicItem = childObject.AddComponent<HoeScript>() as HoeScript; }
        else if (Item.bName == "Axe") { BasicItem = childObject.AddComponent<AxeScript>() as AxeScript; }
        else if (Item.bName == "Pickaxe") { BasicItem = childObject.AddComponent<PickaxeScript>() as PickaxeScript; }
        else if (Item.bName == "Water Bucket") { BasicItem = childObject.gameObject.AddComponent<WateringCanScript>() as WateringCanScript; }
        else if (Item.bName == "Scythe") { BasicItem = childObject.gameObject.AddComponent<ScytheTool>() as ScytheTool; }
        else if (Item.bName == "Fishing Rod") { BasicItem = childObject.gameObject.AddComponent<FishingRodScript>() as FishingRodScript; }
        else if (Item.bItemType == DefaultItemBase.ItemTypes.Seed) { BasicItem = childObject.gameObject.AddComponent<PlantSeed>() as PlantSeed; }
        else if (Item.bItemType == DefaultItemBase.ItemTypes.Ore) { BasicItem = childObject.gameObject.AddComponent<Ore>() as Ore; }
        else { BasicItem = childObject.gameObject.AddComponent<ItemBase>() as ItemBase; }

        return BasicItem;

    }
}
