using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MyPipeline : RenderPipeline
{
    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        //base.Render(context, cameras);
        context.DrawSkybox(cameras[0]);
        context.Submit();
    }
}
