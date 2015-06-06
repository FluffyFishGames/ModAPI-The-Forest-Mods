using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CheatMenu
{
    class TOffLeaves : TurnOffLeaves
    {
        public override void Awake()
        {

            Rigidbody[] rigidbodies = this.transform.GetComponentsInChildren<Rigidbody>();
            Collider[] colliders = this.transform.GetComponentsInChildren<Collider>();
            PhysicMaterial newPhysicMaterial = new PhysicMaterial("Blub");
            newPhysicMaterial.dynamicFriction = 20f;
            newPhysicMaterial.dynamicFriction2 = 40f;
            newPhysicMaterial.staticFriction = 50f;
            newPhysicMaterial.staticFriction2 = 50f;
            newPhysicMaterial.frictionCombine = PhysicMaterialCombine.Multiply;
            foreach (Rigidbody body in rigidbodies)
            {
                body.angularDrag = 5f;
            }
            foreach (Collider coll in colliders)
            {
                coll.sharedMaterial = newPhysicMaterial;
            }
            base.Awake();
        }
    }
}
