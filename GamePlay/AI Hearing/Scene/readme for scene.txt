I forgot to mention this in the tutorial and I'm really sorry:

First thing is probably setting up a ground plane (Create 3D Object/Plane -> Scale it up)

Put the plane on the nav mesh (Window/AI/Navigation -> Select Plane, tick it as NavigationStatic from within the Object sub-tab of the Navigation tab -> Bake the navmesh)

It's probably best to create a new layer and call it "Ground"

Put the ground Plane on the Ground layer

Make sure you have Unity's Standard Assets pack -> https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-for-unity-2018-4-32351

Make sure to have AT LEAST Characters, Particle Effects, and Utilities.
