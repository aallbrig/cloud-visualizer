using System;
using Core.Clouds.AWS;
using SOs;
using TMPro;
using UnityEngine;

namespace Clouds.AWS
{
    public class EC2 : MonoBehaviour
    {
        public EC2_SO data;
        public TextMeshPro text;
        private void Start()
        {
            data ??= ScriptableObject.CreateInstance<EC2_SO>();
            SetDataValues();
        }
        private void SetDataValues()
        {
            var ec2Output = $"{data.instanceID}\n{data.instanceState}";
            if (data.instanceState == InstanceState.RUNNING)
                ec2Output += $"\n{data.publicIP}";
            text.text = ec2Output;
        }
    }
}