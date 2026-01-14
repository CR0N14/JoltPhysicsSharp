// Copyright (c) Amer Koleci and Contributors.
// Licensed under the MIT License (MIT). See LICENSE in the repository root for more information.

namespace JoltPhysicsSharp;

public enum EStateRecorderState
{
    None = 0,
    Global = 1,
    Bodies = 2,
    Contacts = 4,
    Constraints = 8,
    All = Global | Bodies | Contacts | Constraints,
}
