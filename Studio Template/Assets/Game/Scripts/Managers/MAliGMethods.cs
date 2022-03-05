

#region Documentation
// Sep-2020 By Muhammad Ali Safdar - princlia@yahoo.com

#region Keys

// *Ex. = Extension Method

#endregion

// Return Type    Methods                                                                         Descriptions

// void           DestroyAllGameObjectsWithTag(string tag)                           -  Destroy All Gameobjects with given string Tag in current scene
// string         GetRendomStringWithSpliter(string stringArr,char spliter)          -  Get a Rendom string in a Long String with Split Character
// Rigidbody[]    GetRigidboidies(Transform transform)                               -  Get All Child Rigidbodies in given transform
// Ex. viod       KinematicOnOff(this Transform transform,bool isColliderOff=true)   -  Turn Kinenmatic On/Off Fromm All Childern's Rigidbody
// Ex. void       SetActiveAll(this GameObject[] objs, bool value)                   -  Gameobjects Array Active / Deactive
// Ex. Vector3    Random(this Vector3 value, Vector3 min, Vector3 max)               -  Randomr Between two Vectors3

#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MAliGMethods : MonoBehaviour
{
    #region Get Component

    public static Rigidbody[] GetRigidboidies(Transform transform)
    {
        return transform.GetComponentsInChildren<Rigidbody>();
    }

    #endregion

    #region Random

    public static string GetRendomStringWithSpliter(string stringArr,char spliter)
    {
       // UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
        var animArray = stringArr.Split(spliter);
        var randomString = animArray[UnityEngine.Random.Range(0, animArray.Length)];
        return randomString;
    }

    #endregion

    #region GameObject
    /// <summary>
    ///  Destroy All Gameobjects with given string Tag in current scene
    /// </summary>
    /// <param name="tag">Gameobject Tag</param>
    public static void DestroyAllGameObjectsWithTag(string tag)
    {
        var objs = GameObject.FindGameObjectsWithTag(tag);
        foreach (var item in objs)
        {
            Destroy(item);
        }
    }



    #endregion


    public static void Wait(Action action , float time,MonoBehaviour instance)
    {
        //if (delayCor != null) instance.StopCoroutine(delayCor);
        delayCor =  instance.StartCoroutine(AfterTime(action,time));
    }
    static Coroutine delayCor;
  static IEnumerator AfterTime(Action action ,float time)
    {
        yield return new WaitForSeconds(time);
        action.Invoke();
    }
}

public static class ExtensionMethods
{
    #region Math
    public static Vector3 Random(this Vector3 value, Vector3 min, Vector3 max)
    {
        return new Vector3(UnityEngine.Random.Range(min.x, max.x), UnityEngine.Random.Range(min.y, max.y), UnityEngine.Random.Range(min.z, max.z));
    }

    #endregion

    #region Rigidbody
    public static void ZeroVelocity(this Rigidbody rigid)
    {
       rigid.velocity = Vector3.zero;
    }
    public static void ZeroAngularVelocity(this Rigidbody rigid)
    {
        rigid.angularVelocity = Vector3.zero;
    }

    /// <summary>
    /// Turn Kinenmatic On/Off Fromm All Childern's Rigidbody
    /// </summary>
    /// <param name="transform"></param>
    public static void KinematicOnOff(this Transform transform,bool isColliderOff=true)
    {
        Rigidbody[] bodies = MAliGMethods.GetRigidboidies(transform);
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = !rb.isKinematic;
        }

        if (isColliderOff)
        {
            var c = transform.GetComponentsInChildren<Collider>();
            foreach (var item in c)
            {
                if (!item.IsWithTag("Player"))
                {
                    item.enabled = !item.enabled;
                }
               
            }
        }
      

    }

    public static void RagDollForce(this Transform transform, Vector3 minForce, Vector3 maxForce)
    {
        Rigidbody[] bodies = MAliGMethods.GetRigidboidies(transform);
        Vector3 force = Vector3.zero;
        foreach (Rigidbody rb in bodies)
        {
            var f1 = force.Random(minForce, maxForce);
            var f2 = force.Random(minForce, maxForce);
             rb.AddForce(f1, ForceMode.Impulse);

            //rb.AddTorque(f2 * -50f,ForceMode.VelocityChange);
            //  rb.velocity = f1;
            //  rb.AddRelativeForce(f2 * 5,ForceMode.Impulse);
            if (rb.name == "Hips" || rb.name == "Spine" )
            {
                rb.maxAngularVelocity = float.MaxValue;
                 rb.angularVelocity = new Vector3(UnityEngine.Random.Range(-2f,3f), UnityEngine.Random.Range(.5f,1f), 0) * UnityEngine.Random.Range(30f,50f);
               // rb.AddRelativeTorque(new Vector3(-3f,0,0), ForceMode.Impulse);
            }
            if (rb.name == "Handgun_01" || rb.name.StartsWith("jmy_hat"))
            {
                rb.maxAngularVelocity = float.MaxValue;
                rb.angularVelocity = new Vector3(UnityEngine.Random.Range(-2f, 3f), UnityEngine.Random.Range(1f, 4f), 0) * UnityEngine.Random.Range(3f, 5f);
            }
            
           

        }
    }
    public static void RagDollZeroVelocity(this Transform transform)
    {
        Rigidbody[] bodies = MAliGMethods.GetRigidboidies(transform);
        Vector3 force = Vector3.zero;
        foreach (Rigidbody rb in bodies)
        {
            rb.ZeroVelocity();
            //rb.ZeroAngularVelocity();
        }
    }



    #endregion

    #region Colliders / Triggers
    public static string GetName(this UnityEngine.Collision collision)
    {
        return collision.gameObject.name;
    }
    public static string GetName(this UnityEngine.Collider collider)
    {
        return collider.gameObject.name;
    }

    public static string GetTag(this UnityEngine.Collision collision)
    {
        return collision.gameObject.tag;
    }
    public static string GetTag(this UnityEngine.Collider collider)
    {
        return collider.gameObject.tag;
    }
    public static bool IsWithName(this UnityEngine.Collision collision,string name)
    {
        return collision.GetName().Equals(name);      
    }
    public static bool IsWithTag(this UnityEngine.Collision collision, string tag)
    {
        return collision.GetTag().Equals(tag);
    }
    public static bool IsWithName(this UnityEngine.Collider collision, string name)
    {
        return collision.GetName().Equals(name);
    }
    public static bool IsWithTag(this UnityEngine.Collider collision, string tag)
    {
        return collision.GetTag().Equals(tag);
    }
    public static Vector3 FirstContact(this UnityEngine.Collision collision)
    {
        return collision.contacts[0].normal;
    }



    #endregion

    #region Animation


    public static string SetBoolRandom(this Animator anim,string animations, bool value)
    {
        var randomAnim = MAliGMethods.GetRendomStringWithSpliter(animations, ',');
        anim.SetBool(randomAnim, value);
        return randomAnim;
    }





    #endregion

    #region GameObject

    public static void SetActiveAll(this GameObject[] objs, bool value)
    {
        foreach (var item in objs)
        {
            item.SetActive(value);
        }
    }



    #endregion

    #region Arrays
    public static T[] AddtoArray<T>(this T[] Org, T New_Value)
    {
        T[] New = new T[Org.Length + 1];
        Org.CopyTo(New, 0);
        New[Org.Length] = New_Value;
        return New;
    }

    #endregion

    #region Transform

    public static Transform[] ToTransformArray(this MonoBehaviour[] array)
    {
        Transform[] arr = new Transform[array.Length];

        int index = 0;
        foreach (var item in array)
        {
            arr[index] = item.transform;
            index++;
        }

        return arr;

    }

    #endregion

}


