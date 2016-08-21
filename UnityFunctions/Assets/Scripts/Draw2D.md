

```
var t1in2d = t1.As2d(ref x2d, ref y2d) + Vector2.right;
var t2in2d = t2.As2d(ref x2d, ref y2d) + Vector2.right;
var t3in2d = t3.As2d(ref x2d, ref y2d) + Vector2.right;
var spin2d = spherePos.As2d(ref x2d, ref y2d) + Vector2.right;
var radius2d = (float) Math.Sqrt(1.0 - ratio*ratio)*_sphereRadius;

var hasCollision = HasCircleTriangleCollision2D(ref spin2d, radius2d, ref t1in2d, ref t2in2d, ref t3in2d);


float i, j;
for (i = -1f, j = -1+0.05f; j <= 1f; i += 0.05f, j += 0.05f)
{
    var x1 = i;
    var y1 = (float)Mathf.Sqrt(1f - x1*x1);

    var x2 = j;
    var y2 = (float)Mathf.Sqrt(1f - x2*x2);
    Debug.DrawLine(new Vector3(spin2d.x+x1*radius2d, spin2d.y+y1*radius2d), new Vector3(spin2d.x+x2*radius2d, spin2d.y+y2*radius2d), hasCollision ? Color.yellow : Color.gray, 0, false);
    Debug.DrawLine(new Vector3(spin2d.x+x1*radius2d, spin2d.y-y1*radius2d), new Vector3(spin2d.x+x2*radius2d, spin2d.y-y2*radius2d), hasCollision ? Color.yellow : Color.gray, 0, false);
}

Debug.DrawLine(t1in2d, t2in2d, hasCollision ? Color.cyan : Color.gray, 0, false);
Debug.DrawLine(t2in2d, t3in2d, hasCollision ? Color.cyan : Color.gray, 0, false);
Debug.DrawLine(t3in2d, t1in2d, hasCollision ? Color.cyan : Color.gray, 0, false);

```
