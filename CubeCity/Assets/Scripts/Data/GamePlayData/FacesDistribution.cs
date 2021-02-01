using UnityEngine;

[System.Serializable]
public class DistributionItem
{
    public string name;
    public int amount;
    public DistributionItem(string name, int amount)
    {
        this.name = name;
        this.amount = amount;
    }

    public DistributionItem(DistributionItem distributionItem)
    {
        this.name = distributionItem.name;
        this.amount = distributionItem.amount;
    }
}

[System.Serializable]
public class FacesDistribution
{
    [SerializeField] DistributionItem[] distribution;

    public FacesDistribution()
    {
        FaceTypes[] faceTypes = (FaceTypes[]) System.Enum.GetValues(typeof(FaceTypes));
        distribution = new DistributionItem[faceTypes.Length];
        for (int i = 0; i < distribution.Length; i++)
        {
            distribution[i] = new DistributionItem(((FaceTypes)i).ToString(), 0);
        }
    }

    public FacesDistribution(FacesDistribution facesDistribution)
    {
        distribution = new DistributionItem[facesDistribution.distribution.Length];
        for (int i = 0; i < facesDistribution.distribution.Length; i++)
        {
            distribution[i] = new DistributionItem(facesDistribution.distribution[i]);
        }
    }

    public int GetTotalRemainingFaces()
    {
        int remainingFaces = 0;
        foreach (DistributionItem item in distribution)
        {
            remainingFaces += item.amount;
        }
        return remainingFaces;
    }

    public int GetNewFaceTypeIndex()
    {
        int remainingFaces = GetTotalRemainingFaces();
        if (remainingFaces == 0)
        {
            Debug.LogError("No more faces in the distribution system");
            return Random.Range(0, 6);
        }

        int result = 0;
        float totalFaces = (float) remainingFaces;
        float acum_prob = distribution[0].amount / totalFaces;
        float random = Random.Range(0f, 1f);
        while (acum_prob < random)
        {
            result++;
            acum_prob += distribution[result].amount / totalFaces;
        }

        distribution[result].amount -= 1;
        return result;
    }
}
