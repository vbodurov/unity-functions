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

* CapsuleCapsuleCollision.cs
* CapsuleDiskCollision.cs
* CapsuleSphereCollision.cs
* DiskDiskCollision.cs
* ProjectPointOnPlane.cs
* RaySphereCollision.cs
* RayTriangleCollision.cs
* TriangleDiskCollision.cs
* TriangleLineCollision.cs
* TriangleSphereCollision.cs
* TriangleTriangleCollision.cs