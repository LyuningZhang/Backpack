using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLua;

public class Ctrl : MonoBehaviour
{
    LuaSvr luaSvr;
    LuaTable self;
    LuaFunction init;

    void Start()
    {
        luaSvr = new LuaSvr();
        luaSvr.init(null, () =>
         {
             self = luaSvr.start("lua/Ctrl") as LuaTable;
             init = self["init"] as LuaFunction;
         });
        if(init != null)
            init.call(self, transform);
    }
}
