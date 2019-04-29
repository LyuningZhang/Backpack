using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLua;
public class GridClick : MonoBehaviour
{
    LuaSvr luaSvr;
    LuaTable self;
    LuaFunction init;
    LuaFunction updateClick;

    public bool isClick;

    public void UpdateClick()
    {
        updateClick.call(self);
    }

    void Start()
    {
        luaSvr = new LuaSvr();
        luaSvr.init(null, () =>
        {
            self = luaSvr.start("lua/GridClick") as LuaTable;
            init = self["init"] as LuaFunction;
            updateClick = self["UpdateClick"] as LuaFunction;
        });
        if(init != null)
        {
            init.call(self, transform);
        }
    }
}
