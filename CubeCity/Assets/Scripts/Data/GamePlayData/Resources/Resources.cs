using System;
using UnityEngine;

[Serializable]
public struct ResourceItem
{
    public string name;
    public int amount;

    public ResourceItem(string name, int amount)
    {
        this.name = name;
        this.amount = amount;
    }

    public static ResourceItem operator +(ResourceItem a, ResourceItem b)
    {
        if (a.name != b.name)
        {
            Debug.LogError("Trying to sum two different resources");
        }
        ResourceItem result = new ResourceItem(a.name, a.amount + b.amount);
        return result;
    }

    public static ResourceItem operator -(ResourceItem a)
    {
        return new ResourceItem(a.name, -a.amount);
    }
}

[System.Serializable]
public class Resources
{
    [HideInInspector] public string name;
    [Header("Stats")]
    [SerializeField] private ResourceItem[] resources;
    public Resources()
    {
        ResourceTypes[] resourceTypes = (ResourceTypes[]) Enum.GetValues(typeof(ResourceTypes));
        resources = new ResourceItem[resourceTypes.Length];
        for (int i = 0; i < resourceTypes.Length; i++)
        {
            resources[i].name = ((ResourceTypes)i).ToString();
            resources[i].amount = 0;
        }
    }

    public Resources(Resources other) : this()
    {
        this.name = other.name;
        for (int i = 0; i < other.resources.Length; i++)
        {
            try
            {
                int field = (int)Enum.Parse(typeof(ResourceTypes), other.resources[i].name);
                resources[field].amount = other.resources[i].amount;
            } catch (ArgumentException e)
            {
                // The resource type no longer exists
                continue;
            }
        }
    }

    public static Resources operator +(Resources a, Resources b)
    {
        Resources result = new Resources();
        for (int i = 0; i < result.resources.Length; i++) {
            result.resources[i] = a.resources[i] + b.resources[i];
        }

        return result;
    }

    public static Resources operator -(Resources a)
    {
        Resources result = new Resources();
        for (int i = 0; i < result.resources.Length; i++)
        {
            result.resources[i] = -a.resources[i];
        }

        return result;
    }

    public int GetResourceType(ResourceTypes type)
    {
        return resources[(int)type].amount;
    }
}