using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapGenerator
{
    public static MapPortionData GenerateMapPortion()
    {
        MapSliceData[] slices = new MapSliceData[20];

        bool isHole = false;
        bool hasFloorCollectable = true;
        bool hasSecondLevel = false;
        bool hasSecondLevelCollectable = false;

        for (int i = 0; i < slices.Length; i++)
        {
            hasFloorCollectable = Random.Range(0f, 1f) > (hasFloorCollectable ? 0.5f : 0.7f);
            hasSecondLevel = Random.Range(0f, 1f) > (hasSecondLevel ? 0.3f : 0.7f);
            hasSecondLevelCollectable = (hasSecondLevel ? Random.Range(0f, 1f) > (hasSecondLevelCollectable ? 0.5f : 0.7f) : Random.Range(0f, 1f) > (!hasSecondLevelCollectable ? 0.6f : 0.8f));
            isHole = (hasSecondLevel ? Random.Range(0f, 1f) > 0.6f : !isHole && Random.Range(0f, 1f) > 0.8f);

            //if (hasFloorCollectable && isHole)
            //{
            //    hasFloorCollectable = false;
            //    hasSecondLevelCollectable = true;
            //}

            slices[i] = new MapSliceData(isHole, (hasFloorCollectable ? CollectableData.GetRandomCollectable() : CollectableData.None), hasSecondLevel, (hasSecondLevelCollectable ? CollectableData.GetRandomCollectable() : CollectableData.None));
        }

        return new MapPortionData() { Slices = slices };
    }
}

public struct MapPortionData
{
    public MapSliceData[] Slices;
}

public struct MapSliceData
{
    public bool IsHole { get; private set; }

    public CollectableData FloorCollectable { get; private set; }

    public bool HasSecondLevel { get; private set; }

    public CollectableData SecondLevelCollectable { get; private set; }

    public MapSliceData(bool isHole, CollectableData floorCollectable, bool hasSecondLevel, CollectableData secondLevelCollectable)
    {
        IsHole = isHole;
        FloorCollectable = floorCollectable;
        HasSecondLevel = hasSecondLevel;
        SecondLevelCollectable = secondLevelCollectable;
    }
}

public struct CollectableData
{
    public static CollectableData None = new CollectableData(0);

    public int Points { get; private set; }

    private CollectableData(int points)
    {
        Points = points;
    }

    public static CollectableData GetRandomCollectable()
    {
        return new CollectableData(Random.Range(1, 4));
    }

    public bool IsNone() => Points <= 0;
}

