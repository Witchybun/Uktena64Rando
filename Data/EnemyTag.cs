using UnityEngine;

namespace Uktena64Randomizer.Data;

public class EnemyTag: MonoBehaviour
{
    public string info;

    public EnemyTag(string thing)
    {
        info = thing;
    }
}