// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

using System;
using System.Numerics;
using System.Runtime.InteropServices;
using static JoltPhysicsSharp.JoltApi;

namespace JoltPhysicsSharp;

public class StateRecorderFilter : NativeObject, IStateRecorderFilter
{
    private static readonly JPH_StateRecorderFilter_Procs s_procs;
    private readonly nint _stateRecorderFilterUserData;

    public struct JPH_StateRecorderFilter_Procs
    {
        public delegate* unmanaged<nint, nint, Bool8> ShouldSaveBody;
        public delegate* unmanaged<nint, nint, Bool8> ShouldSaveConstraint;
        public delegate* unmanaged<nint, BodyID, BodyID, Bool8> ShouldSaveContact;
        public delegate* unmanaged<nint, BodyID, BodyID, Bool8> ShouldRestoreContact;
    }

    static unsafe StateRecorderFilter()
    {
        s_procs = new JPH_StateRecorderFilter_Procs
        {
            ShouldSaveBody = &ShouldSaveBodyCallback,
            ShouldSaveConstraint = &ShouldSaveConstraintCallback,
            ShouldSaveContact = &ShouldSaveContactCallback,
            ShouldRestoreContact = &ShouldRestoreContactCallback,
        };
        JPH_StateRecorderFilter_SetProcs(in s_procs);
    }

    public StateRecorderFilter()
    {
        _stateRecorderFilterUserData = DelegateProxies.CreateUserData(this, true);
        Handle = JPH_StateRecorderFilter_Create(_stateRecorderFilterUserData);
    }

    public StateRecorderFilter(nint handle, bool ownsHandle)
        : base(handle, ownsHandle)
    {
    }

    protected override void DisposeNative()
    {
        DelegateProxies.GetUserData<StateRecorderFilter>(_stateRecorderFilterUserData, out GCHandle gch);

        JPH_StateRecorderFilter_Destroy(Handle);
        gch.Free();
    }

    nint IStateRecorderFilter.Handle => Handle;

    protected virtual bool ShouldSaveBody(in Body body)
    {
    }

    protected virtual bool ShouldSaveConstraint(in Constraint constraint)
    {
    }

    protected virtual bool ShouldSaveContact(in BodyID body1, in BodyID body2)
    {
    }

    protected virtual bool ShouldRestoreContact(in BodyID body1, in BodyID body2)
    {
    }

    [UnmanagedCallersOnly]
    private static unsafe Bool8 ShouldSaveBodyCallback(nint userData, nint body)
    {
        StateRecorderFilter stateRecorderFilter = DelegateProxies.GetUserData<StateRecorderFilter>(userData, out _);

        return stateRecorderFilter.ShouldSaveBody(in Body.GetObject(body)!);
    }

    [UnmanagedCallersOnly]
    private static unsafe Bool8 ShouldSaveConstraintCallback(nint userData, nint constraint)
    {
        StateRecorderFilter stateRecorderFilter = DelegateProxies.GetUserData<StateRecorderFilter>(userData, out _);

        return stateRecorderFilter.ShouldSaveConstraint(in Constraint.GetObject(constraint)!);
    }

    [UnmanagedCallersOnly]
    private static unsafe Bool8 ShouldSaveContactCallback(nint userData, BodyID body1, BodyID body2)
    {
        StateRecorderFilter stateRecorderFilter = DelegateProxies.GetUserData<StateRecorderFilter>(userData, out _);

        return stateRecorderFilter.ShouldSaveContact(in body1, in body2);
    }

    [UnmanagedCallersOnly]
    private static unsafe Bool8 ShouldRestoreContactCallback(nint userData, BodyID body1, BodyID body2)
    {
        StateRecorderFilter stateRecorderFilter = DelegateProxies.GetUserData<StateRecorderFilter>(userData, out _);

        return stateRecorderFilter.ShouldRestoreContact(in body1, in body2);
    }
}

public interface IStateRecorderFilter
{
    public nint Handle { get; }
}
