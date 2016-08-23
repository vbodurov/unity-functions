### Visualization of different unity projection and collistion detection algorithms

[![Unity 3D manual collisions](https://img.youtube.com/vi/1xbVyvCTEwU/0.jpg)](https://www.youtube.com/watch?v=1xbVyvCTEwU)

How to:

1. Open scene from unity-functions/UnityFunctions/Assets/Scenes/ folder.

2. Play the scene

3. Open "Scene View" then select object and move it.

When screen object is selected you can use shortcuts to switch between views:

W - move
E - rotate

Collisions Visualized:

* Capsule - Capsule
* Capsule - Sphere
* Capsule - Disk
* Triangle - Ray
* Triangle - Line
* Triangle - Sphere
* Triangle - Triangle
* Triangle - Disk
* Disk - Disk

Main behaviours for each collision are in **unity-functions\UnityFunctions\Assets\Scripts\Main**:

* [CapsuleCapsuleCollision.cs](UnityFunctions/Assets/Scripts/Main/CapsuleCapsuleCollision.cs)
* [CapsuleDiskCollision.cs](UnityFunctions/Assets/Scripts/Main/CapsuleDiskCollision.cs)
* [CapsuleSphereCollision.cs](UnityFunctions/Assets/Scripts/Main/CapsuleSphereCollision.cs)
* [DiskDiskCollision.cs](UnityFunctions/Assets/Scripts/Main/DiskDiskCollision.cs)
* [ProjectPointOnPlane.cs](UnityFunctions/Assets/Scripts/Main/ProjectPointOnPlane.cs)
* [RaySphereCollision.cs](UnityFunctions/Assets/Scripts/Main/RaySphereCollision.cs)
* [RayTriangleCollision.cs](UnityFunctions/Assets/Scripts/Main/RayTriangleCollision.cs)
* [TriangleDiskCollision.cs](UnityFunctions/Assets/Scripts/Main/TriangleDiskCollision.cs)
* [TriangleLineCollision.cs](UnityFunctions/Assets/Scripts/Main/TriangleLineCollision.cs)
* [TriangleSphereCollision.cs](UnityFunctions/Assets/Scripts/Main/TriangleSphereCollision.cs)
* [TriangleTriangleCollision.cs](UnityFunctions/Assets/Scripts/Main/TriangleTriangleCollision.cs)