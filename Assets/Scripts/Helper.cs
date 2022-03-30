using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static class Tags
    {
        public static string player = "Player";
        public static string ball = "Ball";        
        public static string mainPlatform = "MainPlatform";
        public static string platformSide = "PlatformSide";
        public static string holePlatform = "HolePlatform";
        public static string checkPointDoor = "CheckPointDoor";
        public static string checkPoint1 = "CheckPoint1";
        public static string checkPoint2 = "CheckPoint2";
        public static string checkPoint3 = "CheckPoint3";
        public static string checkPoint = "CheckPoint";
        public static string holePlatform1 = "HolePlatform1";
        public static string holePlatform2 = "HolePlatform2";
        public static string holePlatform3 = "HolePlatform3";
        public static string sceneManagement = "SceneManagement";
        public static string environment1 = "Environment1";
        public static string environment2 = "Environment2";
        public static string environment3 = "Environment3";

    }// Tags

    public static class AnimationParameters
    {
        public static string checkPoint1Reached = "CheckPoint1Reached";
        public static string checkPoint2Reached = "CheckPoint2Reached";
        public static string checkPoint3Reached = "CheckPoint3Reached";
        public static string riseThePlatform = "RiseThePlatform";
    }// AnimationParameters

    public static class PlayerPrefs
    {
        public static readonly string currentLevel = "CurrentLevel";
        public static readonly string ballPercentageToWin = "BallPercentageToWin";
        public static readonly string ballSpawnerCount = "BallSpawnerCount";
        public static readonly string playerSpeed = "PlayerSpeed";
        public static readonly string swipeSpeed = "SwipeSpeed";
    }


} // Class
