## Visualization of different unity projection and collistion detection algorithms

[![Unity 3D manual collisions](https://img.youtube.com/vi/1xbVyvCTEwU/0.jpg)](https://www.youtube.com/watch?v=1xbVyvCTEwU)

How to:

1. Open scene from unity-functions/UnityFunctions/Assets/Scenes/ folder.

2. Play the scene

3. Open "Scene View" then select object and move it.

When screen object is selected you can use shortcuts to switch between views:

* W - move
* E - rotate

main behaviours for each collision are in **unity-functions\UnityFunctions\Assets\Scripts\Main**

## Collisions Visualized:

* Capsule - Capsule: [CapsuleCapsuleCollision.cs](https://github.com/vbodurov/unity-functions/blob/master/UnityFunctions/Assets/Scripts/Main/CapsuleCapsuleCollision.cs)
* Capsule - Disk: [CapsuleDiskCollision.cs](https://github.com/vbodurov/unity-functions/blob/master/UnityFunctions/Assets/Scripts/Main/CapsuleDiskCollision.cs)
* Capsule - Sphere: [CapsuleSphereCollision.cs](https://github.com/vbodurov/unity-functions/blob/master/UnityFunctions/Assets/Scripts/Main/CapsuleSphereCollision.cs)
* Disk - Disk: [DiskDiskCollision.cs](https://github.com/vbodurov/unity-functions/blob/master/UnityFunctions/Assets/Scripts/Main/DiskDiskCollision.cs)
* Ray - Sphere: [RaySphereCollision.cs](https://github.com/vbodurov/unity-functions/blob/master/UnityFunctions/Assets/Scripts/Main/RaySphereCollision.cs)
* Ray - Triangle:[RayTriangleCollision.cs](https://github.com/vbodurov/unity-functions/blob/master/UnityFunctions/Assets/Scripts/Main/RayTriangleCollision.cs)
* Triangle - Disk: [TriangleDiskCollision.cs](https://github.com/vbodurov/unity-functions/blob/master/UnityFunctions/Assets/Scripts/Main/TriangleDiskCollision.cs)
* Triangle - Line: [TriangleLineCollision.cs](https://github.com/vbodurov/unity-functions/blob/master/UnityFunctions/Assets/Scripts/Main/TriangleLineCollision.cs)
* Triangle - Sphere: [TriangleSphereCollision.cs](https://github.com/vbodurov/unity-functions/blob/master/UnityFunctions/Assets/Scripts/Main/TriangleSphereCollision.cs)
* Triangle - Triangle: [TriangleTriangleCollision.cs](https://github.com/vbodurov/unity-functions/blob/master/UnityFunctions/Assets/Scripts/Main/TriangleTriangleCollision.cs)

## Projecting on a Plane:

* Project point on a plane [ProjectPointOnPlane.cs](https://github.com/vbodurov/unity-functions/blob/master/UnityFunctions/Assets/Scripts/Main/ProjectPointOnPlane.cs)
* Project vector on a plane [ProjectVectorOnPlane.cs](https://github.com/vbodurov/unity-functions/blob/master/UnityFunctions/Assets/Scripts/Main/ProjectVectorOnPlane.cs)

## Transforming Spaces:

* Between Local and World without Transform [TestComputeRelative.cs](https://github.com/vbodurov/unity-functions/blob/master/UnityFunctions/Assets/Scripts/Main/MiscTests/TestComputeRelative.cs)
* Between 2D and 3D [Test2D3DConversion.cs](https://github.com/vbodurov/unity-functions/blob/master/UnityFunctions/Assets/Scripts/Main/MiscTests/Test2D3DConversion.cs)

