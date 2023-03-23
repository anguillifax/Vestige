Tutorial Prototype Feature
--------------------------

One of the big goals of team development is to be able to work on tasks in
parallel without creating conflicts.

Vestige addresses this goal through the use of Feature packages.

Each time a new feature is taken from the Trello, the feature initially starts
off as a Prototype Feature placed in the Prototype/ folder. Once the prototype
works as expected and is ready for integration, the Integration Manager will
tidy up the feature and place it into the main Content/ folder.

A Feature Package is structured as follows

|  FeatureName (Owners)/
|  - Output.prefab
|  - OutputVariant.prefab
|  - Readme.txt
|  - Private/
|    - Obj1.asset
|    - Helper3.mat
|    - TempScript.csharp

All assets intended to be edited by other members of the team are placed in the
root folder. These include Output.prefab, OutputVariant.prefab, and Readme.txt
in the above layout.

Any resources needed to build the asset are placed in the Private/ folder to
clearly communicate they are for the developer only. These include Obj1.asset,
Helper3.mat, and TempScript.csharp in the above layout.

In the case of this example, the directory appears as follows

|  Tutorial Feature (Bryan)
|  - Tutorial Logger.prefab
|  - Readme.txt
|  - Private/
|    - TutorialLoggerScript.csharp

Scripts may be created inside of the Prototype/ folder, but such scripts are
not visible to production code inside ~/Scripts/.

A Readme.txt is optional, but encouraged. Keep in mind these files must be
created using an external text editor.

That's all! Contact your team lead for questions regarding Feature Packages and
prototyping/integration.
