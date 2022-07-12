using System;
using System.Linq.Expressions;
using System.Reflection;
using UnityEngine;

namespace ProjectAssets.Project.Runtime.Test
{
    public class TestingScript : MonoBehaviour
    {
        private void Awake()
        {
            print("Awake");
        }

        private void TestMethod ([System.Runtime.CompilerServices.CallerMemberName] string memberName = "")
        {
            //Debug.Log(type.Name);
        }
    }
}
