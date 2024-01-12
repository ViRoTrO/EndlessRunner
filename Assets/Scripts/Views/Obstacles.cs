using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : BaseView, IPoolItems
{
    [SerializeField] private ObstacleTypesEnums id;

    public ObstacleTypesEnums Id => id;

}
