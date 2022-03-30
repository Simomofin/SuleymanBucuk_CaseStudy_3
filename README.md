# SuleymanBucuk_CaseStudy
Case Study

Dev Log:
1) Run LoadingScene_RunThisFirst scene on editor ar first, GameManager.cs instance is working and managing game from this scene. Game logic is designed as this level runs first. If you don't run this scene first, you may get some errors.
2) Levels getting harder by increasing total ball spawner count, increasing player's speed and player's swipe speed and increasing collected ball's percentage to win level. These values are controlled by GameManager.cs->IncreaseDifficulty function
3) There are 10 scenes and 10 different colour patterns. For each scene, these colour patterns are being applied by SceneManagement.cs->SetGameObjectTexture function. So players don't feel like they play on same level.
4) For each scene, there are 3 check points. SceneManagement.cs->CheckLevelFinish() function checks if 3th check point is reached and calculates if enough ball is collected to pass current level. If enough ball collected, GameWonUI is shown, if next level button is clicked by player, GameManager.cs->IncreaseLevelNumber() function is called and difficulty is being increased for the next level. If enough ball is not collected Replay button appears and if clicked, reloads the same level.
5) There are 10 ball spawner at the very beginning of game, the ball spawners spawns 10 balls at a random coordinate just around it's spawner. A little power on Y coordinate is applied to each ball to make the game more attractable. These ball spawners are controlled by BallRespawnManagement.cs->GenerateSpawnerPrefabs() function. This function gets the spawner count from PlayerPrefs according to level difficulty and divide this number by 3 and spawns 1/3 of spawners between start point and 1th checkpoint, 1/3 of them between 1th and 2th checkpoint and rest of spawners between 2th and 3th checkpoints. Even spawners count is increased by passing every level, this function ensures that spawners are distributed where they should be.
6) Level numbers are shown under Canvas->UpsidePanel in the Editor. These values controlled by SceneManagement.cs->SetLevelNumberText() function. Also checkpoints are indicated between current level and mext level as white before player reaches checkpoint, when player reaches related checkpoint, related UI gets red colour. This is controlled in SceneManagement.cs->CheckpointUIColorChange(int cpNumber) function.

7) Explanations about LevelEditor scene: In LevelEditorScene, if you press Update Scene button, it gets all scenes names included in Build Settings and set them to dropdown menu. Select any scene you want to get, and press Get Selected Scene button to get all level objects in related scene. When you click Update Scene button again LevelEditorManagement.cs->OnUpdateSceneButtonClicked() function clears all objects in scene to prevent call over all gameobjects. Data about all objects in any scene is store in a scriptable object called SceneData. Inside this scriptable objects, there is a serialized class named as DataContainer which holds object' s tag, position and rotation info. This serves connection between scenes and handle operations with ease.

***Uncompleted Parts***
1) On level editor scene, updating objects position and rotations
2) Adding new scene on level editor scene
3) Adding objects from editor scene
4) Updating level complete percentage from level editor
