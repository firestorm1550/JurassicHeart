using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkeletonPlacement
{
    public class DinoPart : MonoBehaviour
    {
        public enum PartEnum
        {
            Part1,
            Part2,
            Part3,
            Part4
        }
        public PartEnum PartType;

        public void OnPickUp()
        {
            PlayerInventory.Instance.QueryPart(this);
        }
    }

}
