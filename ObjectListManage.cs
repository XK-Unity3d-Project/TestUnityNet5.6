using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 对象池管理.
/// </summary>
public class ObjectListManage
{
    class ObjectManageDt
    {
        public bool IsUsed;
        public GameObject Obj;
    }
    List<ObjectManageDt> ObjList = new List<ObjectManageDt>();
    /// <summary>
    /// 添加Object到list.
    /// </summary>
    public void AddObjectToList(GameObject obj)
    {
        ObjectManageDt objManageDt = new ObjectManageDt();
        objManageDt.Obj = obj;
        objManageDt.IsUsed = false;
        ObjList.Add(objManageDt);
    }
    /// <summary>
    /// 在list中查找可用的Object.
    /// </summary>
    public GameObject FindObjectFromList()
    {
        try
        {
            ObjectManageDt objManageDt =  ObjList.Find(
                delegate (ObjectManageDt objTmp)
                {
                    return objTmp.IsUsed == false;
                });
            if (objManageDt != null)
            {
                objManageDt.IsUsed = true;
                return objManageDt.Obj;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Unity: -> " + ex);
            throw;
        }
    }
    public void CloseObjectInfoFromList(GameObject obj)
    {
        try
        {
            ObjectManageDt objManageDt = ObjList.Find(
                delegate (ObjectManageDt objTmp)
                {
                    return objTmp.Obj == obj;
                });
            if (objManageDt != null)
            {
                objManageDt.IsUsed = false;
            }
            return;
        }
        catch (Exception ex)
        {
            Debug.LogError("Unity: -> " + ex);
            throw;
        }
    }
}