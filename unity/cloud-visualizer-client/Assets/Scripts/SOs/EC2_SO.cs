using Core.Clouds.AWS;
using UnityEngine;

namespace SOs
{
    [CreateAssetMenu(fileName = "EC2_Info", menuName = "SOs/new EC2", order = 0)]
    public class EC2_SO : ScriptableObject
    {
        public string instanceID = "i-xxxxx";
        public string publicIP = "1.2.3.4";
        public InstanceState instanceState = InstanceState.STOPPED;
    }
}