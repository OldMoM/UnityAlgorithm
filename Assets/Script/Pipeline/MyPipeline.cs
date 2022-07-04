using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MyPipeline : RenderPipeline
{
    private void Buffer(ScriptableRenderContext context, Camera[] cameras)
    {
        CommandBuffer buffer = new CommandBuffer
        {
            name = "FirstBuffer"
        };
        CameraClearFlags clearFlag = cameras[0].clearFlags;
        buffer.ClearRenderTarget(
            (clearFlag & CameraClearFlags.Depth) != 0,
            (clearFlag & CameraClearFlags.Color) != 0,
            cameras[0].backgroundColor);
        //buffer.ClearRenderTarget(true, false, Color.clear);
        context.ExecuteCommandBuffer(buffer);
        buffer.Release();
    }

    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        ScriptableCullingParameters cullingParameters = new ScriptableCullingParameters();

        CullingResults cull = context.Cull(ref cullingParameters);

        context.DrawSkybox(cameras[0]);
        context.Submit();
    }
}
