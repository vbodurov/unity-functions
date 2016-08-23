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

* Capsule - Capsule: [CapsuleCapsuleCollision.cs](UnityFunctions/Assets/Scripts/Main/CapsuleCapsuleCollision.cs)
* Capsule - Sphere: [CapsuleSphereCollision.cs](UnityFunctions/Assets/Scripts/Main/CapsuleSphereCollision.cs)
* Capsule - Disk: [CapsuleDiskCollision.cs](UnityFunctions/Assets/Scripts/Main/CapsuleDiskCollision.cs)
* Triangle - Ray:[RayTriangleCollision.cs](UnityFunctions/Assets/Scripts/Main/RayTriangleCollision.cs)
* Triangle - Line: [TriangleLineCollision.cs](UnityFunctions/Assets/Scripts/Main/TriangleLineCollision.cs)
* Triangle - Sphere: [TriangleSphereCollision.cs](UnityFunctions/Assets/Scripts/Main/TriangleSphereCollision.cs)
* Triangle - Triangle: [TriangleTriangleCollision.cs](UnityFunctions/Assets/Scripts/Main/TriangleTriangleCollision.cs)
* Triangle - Disk: [TriangleDiskCollision.cs](UnityFunctions/Assets/Scripts/Main/TriangleDiskCollision.cs)
* Disk - Disk: [DiskDiskCollision.cs](UnityFunctions/Assets/Scripts/Main/DiskDiskCollision.cs)
* Ray - Sphere: [RaySphereCollision.cs](UnityFunctions/Assets/Scripts/Main/RaySphereCollision.cs)

Projecting point on plane:

* [ProjectPointOnPlane.cs](UnityFunctions/Assets/Scripts/Main/ProjectPointOnPlane.cs)

main behaviours for each collision are in **unity-functions\UnityFunctions\Assets\Scripts\Main**:

