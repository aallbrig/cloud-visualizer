using System;
using System.Linq;
using UnityEngine;

namespace Clouds.AWS
{
    [RequireComponent(typeof(BoxCollider))]
    public class Region : MonoBehaviour
    {
        [Range(0f, 100f)] public float padding = 5f;
        [Range(0f, 10f)] public float height = 2f;
        private BoxCollider _boxCollider;
        private void Start()
        {
            _boxCollider ??= GetComponent<BoxCollider>();
            CalculateBoxColliderSize();
        }
        private void CalculateBoxColliderSize()
        {
            // The box collider size is defined by the children inside (so I can draw border around it later)
            // Get all children positions
            var children = GetComponentsInChildren<Transform>().ToList();
            // Average position for box collider center = sum of all children positions / number of children
            var averagePosition =
                children.Aggregate(Vector3.zero, (positionSum, child) => positionSum + child.position)
                / children.Count;
            // hack: 0 the Y coordinate
            averagePosition.y = 0;
            _boxCollider.center = averagePosition;
            // TODO: calculate
            // Size of box = 0.5 * distance between
            // Add padding
            // Recursively check? I hope not >.<
            // TODO: calculate
            _boxCollider.size = new Vector3(10, height, 10);
        }
    }
}