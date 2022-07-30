# UnityRaytracer
A raytracing shader interfaced with unity

The goal here is to eventually have a standalone ray tracing engine. Unity is being used for scene setup, and much of the C# is to link the unity scene and the raytracing scene.

## Current Progress
![Current Progress Image](/Pictures/screenshot_2022-7-30-3-3-49.png)
- Collision
  - [x] Spheres
  - [x] Planes
  - [ ] Cuboids
  - [ ] Triangles
  - [ ] Mesh
- Lighting
  - [x] Reflection
  - [ ] Rough Reflection
  - [x] Diffuse Scattering
  - [x] Refraction
  - [ ] Partial Refraction with outer diffuse color
  - [ ] Dispersion
  - [x] Emission
- Object
  - [x] Translation
  - [x] Rotation
  - [x] Scale
  - [ ] Optimized Collision
- Utility
  - [x] Screenshots
  - [x] Realtime Object Editing
  - [x] Realtime Material Editing
- Camera
  - [x] Time Averaging
  - [ ] Dynamic Exposure
  - [ ] Bloom
