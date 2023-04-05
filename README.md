# UnityRaytracer
A raytracing shader interfaced with unity

The goal here is to eventually have a standalone ray tracing engine. Unity is being used for scene setup, and much of the C# is to link the unity scene and the raytracing scene.

## Current Progress
![Current Progress Image](/Pictures/screenshot_2023-4-5-10-57-19.png)
- Collision
  - [x] ~~Spheres~~
  - [x] ~~Planes~~
  - [x] ~~Cuboids~~
  - [x] Triangles
  - [x] Mesh
- Lighting
  - [x] Reflection
  - [ ] Rough Reflection
  - [x] Diffuse Scattering
  - [ ] Refraction
  - [ ] Partial Refraction with outer diffuse color
  - [x] Dispersion
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
  - [ ] ~~Time Averaging~~
  - [ ] Dynamic Exposure
  - [ ] Bloom
