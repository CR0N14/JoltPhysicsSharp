// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System;
using System.Numerics;
using System.Runtime.InteropServices;
using static JoltPhysicsSharp.JoltApi;

namespace JoltPhysicsSharp;

public class StateRecorder : NativeObject, IStateRecorder
{
    public StateRecorder(nint handle, bool ownsHandle)
        : base(handle, ownsHandle)
    {
    }

    nint IPhysicsStepListener.Handle => Handle;

    public void SetValidating(bool inValidating)
    {
        JPH_StateRecorder_SetValidating(Handle, inValidating);
    }

    public bool IsValidating()
    {
        return JPH_StateRecorder_IsValidating(Handle);
    }

    public void SetIsLastPart(bool inIsLastPart)
    {
        JPH_StateRecorder_SetIsLastPart(Handle, inIsLastPart);
    }

    public bool IsLastPart()
    {
        return JPH_StateRecorder_IsLastPart(Handle);
    }
}

public interface IStateRecorder
{
    public nint Handle { get; } 
}
