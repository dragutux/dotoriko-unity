# .Oriko - a Unity3D Framework

Despite Unity is a massive game engine that contains a lot of stuff to make life of game developer easy, there are no some useful stuff that we use almost in every game.

**.Oriko Framework** contains useful utilities to reduce developer's pain of implementing some functionality. Furthermore this framework allows you to structure your game using MVC, create sequances with Finite State Machine, use own Event System and many more. It's time to improve your indie projects!

### Why not asset store?

Usually you can see stuff like this on Unity Asset Store for $60 and more. I think that this code can be improved and optimized a lot. Also I would like other developers to add new component that they think are used frequently.

### Installing

> Note, that **.Oriko** works with **Unity 4.6+**  because of new UI system

In **Assets** folder of your Unity project
```sh 
    git clone https://github.com/NoxCaos/dotoriko-unity.git
    git clone https://github.com/NoxCaos/fullserializer.git
```
Download and import [Ionic Zip] from **zip-v1.9**

>Note, that fullserializer will be integrated to framework soon

That's it! You are ready to start. 

### Usage

Check out README files in every folder. You can find out about namespace, classes, interfaces and how to use them.

### Version **0.7.0**
#

[Ionic Zip]: <https://dotnetzip.codeplex.com/downloads/get/258014>

---------------------------------------

# DotOriko namespace

### DotOrikoComponent

Use this component like a new base for all your scripts. It contains lots of useful stuff that MonoBehavior doesn't. 

##### Public Properties
- `Vector3` **Position**:  the position of transform
- `Vector3` **LocalPosition**:  position in local space
- `Quaternion` **Rotation**:  the rotation of transform
- `Quaternion` **LocalRotation**:  rotation in local space
- `Vector3` **ERotation**:  the euler angles of transform
- `Vector3` **ELocalRotation**:  euler angles in local space
- `Transform` **CachedTransform**: cached transform of gameobject for optimizations

##### Virtual Methods
Basically, these methods replace standart Unity's Start() Update() Awake() to make it more 'Object oriented'
- `void` **OnInitialize**:  called when script is initialized
- `void` **OnStart**:  called the next frame after initilialization
- `void` **OnUpdate**:  called every frame after 'OnStart()'
- `void` **OnScheduledUpdate**:  called when 'StartScheduledUpdate()' is launched. Use 'StopScheduledUpdate()' to stop
- `void` **OnReleaseResources**:  called when object is destroyed. Don't forget to unsubscribe from events here

##### Protected Methods
- `void` **StartScheduledUpdate** (float):  starts calling 'OnScheduledUpdate()' every seconds
- `void` **StopScheduledUpdate**:  position in local space
- `void` **RemoveAllChildren**:  removes all children of current transform
- `void` **RemoveAllChildren** (Transform):  removes all children of specified transform
- `void` **LocalMirrorByX**:  reflects current transform on X axis
- `void` **LocalMirrorByY**:  reflects current transform on Y axis
- `void` **LocalMirrorByXY**:  reflects current transform on X and Y axis

### DotOrikoUI : DotOrikoComponent

Base for all UI components. If you plan to use gameobject in Unity UI it is better to attach this instead of DotOrikoComponent


